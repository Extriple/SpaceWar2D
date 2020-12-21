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
      ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));

      foreach (ResourceTypeSO resourceType in resourceTypeList.list)
      {
         resourceAmmountDictionary[resourceType] = 0;
      }
   }

   private void TestLog()
   {
      //Show resource type for example wood, stone, gold in console
      foreach (ResourceTypeSO resourceTypeSo in resourceAmmountDictionary.Keys)
      {
         Debug.Log(resourceTypeSo.nameString + ": " + resourceAmmountDictionary[resourceTypeSo] );
      }
      
   }
}
