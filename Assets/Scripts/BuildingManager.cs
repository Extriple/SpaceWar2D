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
            if (activeBuildingTypeSo != null)
            {
                //Clone the WoodHarvester
                Instantiate(activeBuildingTypeSo.prefab,  UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
            }
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
}
