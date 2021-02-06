using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingTypeSO buildingTypeSo;
    private HealthSystem healthSystem;
    private void Start()
    {
        buildingTypeSo = GetComponent<BuildingTypeHolder>().buildingTypeSo;
        
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.SetHealthAmountMax(buildingTypeSo.healthAmountMax, true);
        
        healthSystem.onDie += HealthSystem_OnDie;
    }

    private void HealthSystem_OnDie(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }
}
