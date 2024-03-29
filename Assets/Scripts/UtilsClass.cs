﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class UtilsClass
{
   private static Camera mainCamera;
   
   public static Vector3 GetMouseWorldPosition()
   {
      if (mainCamera == null) mainCamera = Camera.main;
      
      Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
      mousePosition.z = 0f;
      return mousePosition;
   }

   public static Vector3 GetRandomDir()
   {
      return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
   }


   public static float GetAngleFromVector(Vector3 vector3)
   {
      float radians = Mathf.Atan2(vector3.y, vector3.x);
      float degrees = radians * Mathf.Rad2Deg;
      return degrees;
   }
}
