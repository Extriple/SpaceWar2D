using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    private ResourceTypeListSO _resourceTypeListSo;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeDictionary;
    private void Awake()
    {
        _resourceTypeListSo = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        resourceTypeDictionary = new Dictionary<ResourceTypeSO, Transform>();
        
        //Transform the prefab with resource
        Transform resourceTemp = transform.Find("ResourceTemp");
        resourceTemp.gameObject.SetActive(false);
        int index = 0;
        foreach (ResourceTypeSO resourceTypeSo in _resourceTypeListSo.list)
        {
            Transform resourceTransform = Instantiate(resourceTemp, transform);
            resourceTransform.gameObject.SetActive(true);


            float offSetAmount = -100f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offSetAmount * index, 0);

            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceTypeSo.sprite;
            resourceTypeDictionary[resourceTypeSo] = resourceTransform;
            index++;
        }
    }

    private void Start()
    {
        ResourceManager.Instance.onResourceAmmountChange += Instance_OnResourceAmountChange;
        UpdateResourceAmmount();
    }

    private void Instance_OnResourceAmountChange(object sender, System.EventArgs e)
    {
        UpdateResourceAmmount();
    }

    //Event 
    

    private void UpdateResourceAmmount()
    {
        foreach (ResourceTypeSO resourceTypeSo in _resourceTypeListSo.list)
        {
            Transform resourceTransform = resourceTypeDictionary[resourceTypeSo];
            
            int resourceAmmount = ResourceManager.Instance.GetResourceAmount(resourceTypeSo);
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmmount.ToString());
        }
    }
}


