using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewAcousticConfig", menuName = "Acoustics/Material Config")]
public class AcousticMaterialConfig : ScriptableObject
{
    [System.Serializable]
    public struct MaterialData
    {
        public string nameKeyword; 
        [Range(0, 1)] public float absorption;
        public Color debugColor;
    }

    public List<MaterialData> materials = new List<MaterialData>();
    public float defaultAbsorption = 0.15f;
    public float skyAbsorption = 0.5f;

    public float GetAbsorption(string tag, out Color debugColor)
    {
        debugColor = Color.white; 

        if (string.IsNullOrEmpty(tag) || tag == "Untagged")
        {
            debugColor = Color.gray;
            return defaultAbsorption;
        }

        string cleanTag = tag.Trim();

        foreach (var mat in materials)
        {
            if (string.IsNullOrEmpty(mat.nameKeyword)) continue;

            if (cleanTag.Equals(mat.nameKeyword.Trim(), System.StringComparison.OrdinalIgnoreCase))
            {
                debugColor = mat.debugColor;
                return mat.absorption;
            }
        }

        return defaultAbsorption;
    }
}