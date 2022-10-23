using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu()]
public class TextureData : UpdatableData
{
    public Layer[] layers;
    const int textureSize = 512;
    const TextureFormat textureFormat = TextureFormat.RGB565;
    float savedMinHeight;
    float savedMaxHeight;
   public void ApplyToMaterial(Material material){
     material.SetInt("layerCount",layers.Length);
     material.SetColorArray("baseColours",layers.Select(x => x.tint).ToArray());
     material.SetFloatArray("baseStartHeights", layers.Select(x => x.StartHeights).ToArray());
     material.SetFloatArray("baseBlends", layers.Select(x => x.blendStrenght).ToArray());
     material.SetFloatArray("baseColourStrength", layers.Select(x => x.tintStrenght).ToArray());
     material.SetFloatArray("baseTextureScales", layers.Select(x => x.textureScale).ToArray());
     Texture2DArray texturesArray = GenerateTextureArray(layers.Select(x => x.texture).ToArray());
     material.SetTexture("baseTextures",texturesArray);
     UpdateMeshHeights(material, savedMinHeight, savedMaxHeight);
   }

   public void UpdateMeshHeights(Material material, float minHeight, float maxHeight){
        savedMaxHeight = maxHeight;
        savedMinHeight = minHeight;

        material.SetFloat("minHeight", minHeight);
        material.SetFloat("maxHeight", maxHeight);
   }
    Texture2DArray GenerateTextureArray(Texture2D[] textures){
        Texture2DArray textureArray = new Texture2DArray(textureSize, textureSize , textures.Length, textureFormat, true);
        for( int i=0 ; i < textures.Length; i++){
            textureArray.SetPixels(textures[i].GetPixels(),i);
        }
        textureArray.Apply();
        return textureArray;
    }

[System.Serializable]
   public class Layer{
    public Texture2D texture;
    public Color tint;
    [Range(0,1)]
    public float tintStrenght;
    [Range(0,1)]
    public float StartHeights;
    [Range(0,1)]
    public float blendStrenght;
    public float textureScale;
   }
}
