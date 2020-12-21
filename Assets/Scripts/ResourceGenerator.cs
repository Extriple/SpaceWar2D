using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private BuildingTypeSO _buildingTypeSo;
    
    
    private float timer;
    private float timerMax;

    private void Awake()
    {
        _buildingTypeSo = GetComponent<BuildingTypeHolder>().buildingTypeSo; 
        //Get active resource generator
        timerMax = _buildingTypeSo.resourceGeneratorData.timerMax;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            timer += timerMax;
            Debug.Log("Ding" + _buildingTypeSo.resourceGeneratorData.resourceType.nameString);
            ResourceManager.Instance.AddResource(_buildingTypeSo.resourceGeneratorData.resourceType, 1);

        }
    }
}
