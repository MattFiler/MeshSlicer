using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Material manager */
public class ObjectMaterialManager : MonoSingleton<ObjectMaterialManager>
{
    MaterialDefinitions materialData;

    /* Pull material data from json on startup */
    private void Start()
    {
        materialData = (MaterialDefinitions)JsonUtility.FromJson(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/material_types.json"), typeof(MaterialDefinitions));
    }

    /* Can the requested material break? */
    public bool CanMaterialBreak(MaterialTypes type)
    {
        foreach (MaterialDefinition material in materialData.materials)
        {
            if (material.type == type.ToString())
            {
                return material.can_shatter;
            }
        }
        Debug.LogError("Tried to access data for non-existant material (" + type + ")! DO NOT MANUALLY EDIT THE FILES!");
        return false;
    }

    /* What is the strength of the requested material? */
    public int GetMaterialStrength(MaterialTypes type)
    {
        foreach (MaterialDefinition material in materialData.materials)
        {
            if (material.type == type.ToString())
            {
                if (!material.can_shatter)
                {
                    Debug.LogError("Tried to access strength value of non-breakable material (" + type + "). Always call CanMaterialBreak first!");
                    return 1;
                }
                return material.strength;
            }
        }
        Debug.LogError("Tried to access data for non-existant material (" + type + ")! DO NOT MANUALLY EDIT THE FILES!");
        return 1;
    }

    /* What is the density of the requested material? */
    public int GetMaterialDensity(MaterialTypes type)
    {
        foreach (MaterialDefinition material in materialData.materials)
        {
            if (material.type == type.ToString())
            {
                if (!material.can_shatter)
                {
                    Debug.LogError("Tried to access density value of non-breakable material (" + type + "). Always call CanMaterialBreak first!");
                    return 1;
                }
                return material.density;
            }
        }
        Debug.LogError("Tried to access data for non-existant material (" + type + ")! DO NOT MANUALLY EDIT THE FILES!");
        return 1;
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
public enum MaterialTypes
{
    /*START*/
    BEDROCK,
    GLASS,
    CONCRETE,
    /*END*/
}
