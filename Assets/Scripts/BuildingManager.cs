using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private BuildingTypeSO buildingTypeSo;
    private BuildingListTypeSO buildingListTypeSo;
    private Camera mainCamera;


    private void Awake()
    {
        //Load the resources
        buildingListTypeSo = Resources.Load<BuildingListTypeSO>(nameof(BuildingListTypeSO));
        
        //Load the scriptable objects with resources and set list on 0 // default
        buildingTypeSo = buildingListTypeSo.list[0];
    }

    private void Start()
    {
        mainCamera = Camera.main;
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Clone the WoodHarvester
            Instantiate(buildingTypeSo.prefab, mouseWorldPos(), Quaternion.identity);
        }

        if (Input.GetKey(KeyCode.E))
        {
            //Select fromt the list building with number 0;
            buildingTypeSo = buildingListTypeSo.list[0];
        }

        if (Input.GetKey(KeyCode.Q))
        {
            buildingTypeSo = buildingListTypeSo.list[1];
        }
        
    }
    
   

    private Vector3 mouseWorldPos()
    {
        //We return mouse position on the screen
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }
}
