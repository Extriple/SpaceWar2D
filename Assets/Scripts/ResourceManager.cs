using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
   //Key and value
   private Dictionary<ResourceTypeSO, int> resourceAmmountDictionary;

   private void Awake()
   {
      resourceAmmountDictionary = new Dictionary<ResourceTypeSO, int>();
      //Load resources with ScriptableObjects
      ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

      foreach (ResourceTypeSO resourceType in resourceTypeList.list)
      {
         resourceAmmountDictionary[resourceType] = 0;
      }
      TestLog();
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.A))
      {
         ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
         //Add 2 wood 
         AddResource(resourceTypeList.list[0], 2);
         TestLog();

      }
   }

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
   }
}
