using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMaterialDefinition : MonoBehaviour
{
    MaterialDefinitions matTypes;
    private void Start()
    {
        matTypes = (MaterialDefinitions)JsonUtility.FromJson(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/material_types.json"), typeof(MaterialDefinitions));
    }
}

[System.Serializable]
public class MaterialDefinitions
{
    public List<MaterialDefinition> materials;
}
[System.Serializable]
public class MaterialDefinition
{
    public string type;
    public int weight;
    public int density;
}

//This enum is auto-populated... do not edit anything below this line!
public enum MateralTypes
{
    /*START*/
    GLASS,
    CONCRETE,
    /*END*/
}
