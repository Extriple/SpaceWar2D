using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;
    
    private Dictionary<BuildingTypeSO, Transform> btnTransformDir;
    private Transform arrowButton;
    
    private void Awake()
    {
        Transform btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);

        BuildingListTypeSO buildingListTypeSo = Resources.Load<BuildingListTypeSO>(typeof(BuildingListTypeSO).Name);

        btnTransformDir = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;
        
        
        arrowButton = Instantiate(btnTemplate, transform);
        arrowButton.gameObject.SetActive(true);
         

        float offSetAmount = +130f;
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offSetAmount * index, 0);

        btnTemplate.Find("image").GetComponent<Image>().sprite = arrowSprite;
        arrowButton.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -40);

         
        //Event system 
        arrowButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });
        
        MouseEnterExitEvent mouseEnterExitEvent =  arrowButton.GetComponent<MouseEnterExitEvent>();
        mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) =>
        {
            ToolTipUI.Instance.Show("Arrow");
        };
        mouseEnterExitEvent.onMouseExit += (object sender, EventArgs e) =>
        {
            ToolTipUI.Instance.Hide();
        };
        
        index++;
        
        foreach (BuildingTypeSO buildingTypeSo in buildingListTypeSo.list)
        { 
            if (ignoreBuildingTypeList.Contains(buildingTypeSo)) continue;
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);
         

            offSetAmount = +130f;
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offSetAmount * index, 0);

            btnTemplate.Find("image").GetComponent<Image>().sprite = buildingTypeSo.sprite;
         
         //Event system  
                btnTransform.GetComponent<Button>().onClick.AddListener(() =>
                {
                BuildingManager.Instance.SetActiveBuildingType(buildingTypeSo);
                });

                mouseEnterExitEvent = btnTransform.GetComponent<MouseEnterExitEvent>();
                mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) =>
                {
                    ToolTipUI.Instance.Show(buildingTypeSo.name + "\n" + buildingTypeSo.GetConstructionResourceCostString());
                };
                mouseEnterExitEvent.onMouseExit += (object sender, EventArgs e) =>
                {
                    ToolTipUI.Instance.Hide();
                };
                

         btnTransformDir[buildingTypeSo] = btnTransform;
         
         index++; 
        }
    }

    private void Start() {
        BuildingManager.Instance.OnActiveBuildingChange += BuildingManager_OnActiveBuildingTypeChanged;
        UpdateActiveBuildingTypeButton();
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e) {
        UpdateActiveBuildingTypeButton();
    }
    

    //selected building
    private void UpdateActiveBuildingTypeButton()
    {
        arrowButton.Find("selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingTypeSo in btnTransformDir.Keys)
        {
            Transform btnTransform = btnTransformDir[buildingTypeSo];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildType = BuildingManager.Instance.GetBuildingType();
        if (activeBuildType == null)
        {
            arrowButton.Find("selected").gameObject.SetActive(true);
        }
        else
        {
            btnTransformDir[activeBuildType].Find("selected").gameObject.SetActive(true);   
        }
    }
}
