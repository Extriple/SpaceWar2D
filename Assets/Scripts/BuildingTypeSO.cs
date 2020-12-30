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
}
