using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataObject : ScriptableObject
{
    public string dataID;

    [ContextMenu("Generate a guid for id")]
    private void GenerateGuid()
    {
        dataID = System.Guid.NewGuid().ToString();
    }


    public string dataName;
    public string dataDescription;
}
