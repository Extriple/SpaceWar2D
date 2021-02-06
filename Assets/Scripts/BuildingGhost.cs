using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObject;
    private ResourceNearbyOverlay resourceNearbyOverlay;

    private void Awake()
    {
        spriteGameObject = transform.Find("sprite").gameObject;
        resourceNearbyOverlay = transform.Find("pfResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
        Hide();
        
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingChange += BuildingManager_OnActiveBuildingTypeChange;
    }

    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e)
    {
        if (e.buildingTypeSo == null)
        {
            Hide();
            resourceNearbyOverlay.Hide();
        }
        else
        {
            Show(e.buildingTypeSo.sprite);
            if (e.buildingTypeSo.hasResourceGeneratorData)
            {


                resourceNearbyOverlay.Show(e.buildingTypeSo.resourceGeneratorData);
            }
            else
            { 
                resourceNearbyOverlay.Hide();
            }
        } 
    }
    
    
    
    private void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPosition();
    }

    private void Show(Sprite gohstSprite) 
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = gohstSprite;
    }

    private void Hide()
    {
        spriteGameObject.SetActive(false);
    }

    
}
