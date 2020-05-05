using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class Inventory : MonoBehaviour
{
    [Serializable]
    public class myDictionary : SerializableDictionary<Ingredient, int> {}
    public myDictionary itemsCollected; 
}
