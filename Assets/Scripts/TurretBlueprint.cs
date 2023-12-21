using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Not a MonoBehaviour so it does not need to be attached to a GameObject (=not a Component)
[System.Serializable]
public class TurretBlueprint
{
   public GameObject prefab;
   public int cost;

   public int GetSellValue()
   {
      return cost/2;
   }
}
