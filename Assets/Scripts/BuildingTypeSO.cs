using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypes")]
public class BuildingTypeSO : ScriptableObject
{
   public string name;
   public Transform prefab;
   public ResourceGeneratorData resourceGeneratorData;
   public Sprite sprite;
   public float minConstractionRadius;
   public ResourceAmount[] conctructionResourceCost;
   public int healthAmountMax;
 
   public string GetConstructionResourceCostString()
   {
      string str = "";
      foreach (ResourceAmount resourceAmount in conctructionResourceCost)
      {
         str += " <color=#" + resourceAmount.resourceTypeSo.colorHex  + ">" + resourceAmount.resourceTypeSo.nameShort + resourceAmount.amount + "</color> ";
      }

      return str;
   }
}
