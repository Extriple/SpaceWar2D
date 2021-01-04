using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    public ResourceGenerator resourceGenerator;

    private Transform barTransform;

    private void Start()
    {
        ResourceGeneratorData resourceGeneratorData =  resourceGenerator.getResourceGenerator();

        barTransform = transform.Find("bar");
        
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmounrGeneratorPerSecond().ToString("F1"));
    }

    private void Update()
    {
        barTransform.localScale = new Vector3(resourceGenerator.GetTimer(), 1, 1);

    }
}
