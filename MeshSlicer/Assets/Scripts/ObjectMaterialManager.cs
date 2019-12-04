using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Material manager */
public class ObjectMaterialManager : MonoBehaviour
{
    MaterialDefinitions matTypes;

    /* Pull material data from json on startup */
    private void Start()
    {
        matTypes = (MaterialDefinitions)JsonUtility.FromJson(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/material_types.json"), typeof(MaterialDefinitions));
    }
}

/* Material json setup */
[System.Serializable]
public class MaterialDefinitions
{
    public List<MaterialDefinition> materials;
}
[System.Serializable]
public class MaterialDefinition
{
    public string type;      //The name of the material
    public bool can_shatter; //If this material is breakable or not
    public int strength;     //The strength of this material (how hard does it need to be hit/dropped to break)
    public int density;      //The density of this material (how much it should shatter when broken)
}

/* This enum is auto-populated... do not edit anything below this line! */
public enum MateralTypes
{
    /*START*/
    BEDROCK,
    GLASS,
    CONCRETE,
    /*END*/
}
