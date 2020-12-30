using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{

    private ResourceGeneratorData resourceGeneratorData;
    
    private float timer;
    private float timerMax;

    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingTypeSo.resourceGeneratorData; 
        //Get active resource generator
        timerMax = resourceGeneratorData.timerMax;
    }

    private void Start()
    {
        Collider2D[] collider2Darray =  Physics2D.OverlapCircleAll(transform.position, resourceGeneratorData.resourceDecetionRadius);

        int ResourceAmounmt = 0;
        foreach (Collider2D collider2D in collider2Darray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if (resourceNode.resourceTypeSo == resourceGeneratorData.resourceType)
                {
                    ResourceAmounmt++;
                }
            }
        }

        ResourceAmounmt = Mathf.Clamp(ResourceAmounmt, 0, resourceGeneratorData.maxResourceAmount);

        if (ResourceAmounmt == 0)
        {
            enabled = false;
        }
        else
        {
            timerMax = (resourceGeneratorData.timerMax / 2f) + resourceGeneratorData.timerMax *
                (1 - (float) ResourceAmounmt / resourceGeneratorData.timerMax);
        }
    }
    
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            timer += timerMax;
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);

        }
    }
}
