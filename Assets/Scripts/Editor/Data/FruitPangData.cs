using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "Datas", menuName = "Scriptable Object Asset/Datas")]
public class FruitPangData : ScriptableObject
{
   [SerializeField]
   private string path;   


   public string GetDataPath()
    {
        return path;
    }
}
