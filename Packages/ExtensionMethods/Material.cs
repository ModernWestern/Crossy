using UnityEngine;

public static class MaterialExtensions
{
    public static bool IsDefaultUIMaterial(this Material material)
    {
        const string Default = "Default UI Material";

        return material.name.Equals(Default);
    }

    public static bool IsMaterial(this Material material, string name)
    {
        return material.shader.name.Equals(name);
    }
}