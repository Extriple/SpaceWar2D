using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
   public static ResourceManager Instance { get; private set; }

   public event EventHandler onResourceAmmountChange;

   //Key and value
   private Dictionary<ResourceTypeSO, int> resourceAmmountDictionary;

   private void Awake()
   {
      Instance = this;
      
      resourceAmmountDictionary = new Dictionary<ResourceTypeSO, int>();
      //Load resources with ScriptableObjects
      ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

      foreach (ResourceTypeSO resourceType in resourceTypeList.list)
      {
         resourceAmmountDictionary[resourceType] = 0;
      }
   }

   /*private void Update()
   {
      if (Input.GetKeyDown(KeyCode.A))
      {
         ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
         //Add 2 wood 
         AddResource(resourceTypeList.list[0], 2);
         TestLog();

      }
   }*/

   private void TestLog()
   {
      //Show resource type for example wood, stone, gold in console
      foreach (ResourceTypeSO resourceType in resourceAmmountDictionary.Keys)
      {
         Debug.Log(resourceType.nameString + ": " + resourceAmmountDictionary[resourceType]);
      }
   }

   public void AddResource(ResourceTypeSO resourceType, int amount)
   {
      resourceAmmountDictionary[resourceType] += amount;
      onResourceAmmountChange? .Invoke(this,EventArgs.Empty);
   }

   public int GetResourceAmount(ResourceTypeSO resourceTypeSo)
   {
      return resourceAmmountDictionary[resourceTypeSo];
   }

   public bool CanAfford(ResourceAmount[] resourceAmounts)
   {
      foreach (ResourceAmount resourceAmount in resourceAmounts)
      {
         if (GetResourceAmount(resourceAmount.resourceTypeSo) >= resourceAmount.amount)
         {
            //Can afford
         }
         else
         {
            return false;
         }
      }

      return true;
   }

   public void SpendResource(ResourceAmount[] resourceAmounts)
   {
      foreach (ResourceAmount resourceAmount in resourceAmounts)
      {
         resourceAmmountDictionary[resourceAmount.resourceTypeSo] -= resourceAmount.amount;
      }
   }
}
