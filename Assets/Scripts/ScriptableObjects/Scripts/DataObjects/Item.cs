using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataObject/Item")]
public class Item : DataObject
{
    public float itemValue;
    public bool isUnique = false;
}
