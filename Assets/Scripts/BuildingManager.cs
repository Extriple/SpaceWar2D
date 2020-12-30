using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    public event EventHandler<OnActiveBuildingTypeChangeEventArgs> OnActiveBuildingChange;

    public class OnActiveBuildingTypeChangeEventArgs : EventArgs
    {
        public BuildingTypeSO buildingTypeSo;
    }
    
    private BuildingTypeSO activeBuildingTypeSo;
    private BuildingListTypeSO buildingListTypeSo;
    private Camera mainCamera;


    private void Awake()
    {
        Instance = this;
        
        //Load the resources
        buildingListTypeSo = Resources.Load<BuildingListTypeSO>(typeof(BuildingListTypeSO).Name);
        
        //Load the scriptable objects with resources and set list on 0 // default
    }

    private void Start()
    {
        mainCamera = Camera.main;
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingTypeSo != null && CanSpawnBuilding(activeBuildingTypeSo, UtilsClass.GetMouseWorldPosition()))
            {
                //Clone the WoodHarvester
                Instantiate(activeBuildingTypeSo.prefab,  UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
            }

            Debug.Log(CanSpawnBuilding(buildingListTypeSo.list[0], UtilsClass.GetMouseWorldPosition()));
        }
    }
    
   

    private Vector3 mouseWorldPos()
    {
        //We return mouse position on the screen
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingTypeSo)
    {
        activeBuildingTypeSo = buildingTypeSo;
        
        OnActiveBuildingChange?.Invoke(
            this,
            new OnActiveBuildingTypeChangeEventArgs {buildingTypeSo = buildingTypeSo}
            );
    }

    public BuildingTypeSO GetBuildingType()
    {
        return activeBuildingTypeSo;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingTypeSo, Vector3 position)
    {
        BoxCollider2D boxCollider2D = buildingTypeSo.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isArenaCleaer = collider2DArray.Length == 0;
        if (!isArenaCleaer) return false;

       collider2DArray = Physics2D.OverlapCircleAll(position, buildingTypeSo.minConstractionRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                //Has buildingtype holder
                if (buildingTypeHolder.buildingTypeSo == buildingTypeSo)
                {
                    //bulding of this with the contruction radius 
                    return false;
                }
            }
        }

        float maxContractionRadius = 25;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxContractionRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                return true;
            }
        }

        return false;
    }
}

