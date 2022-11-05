using UnityEngine;
using System.Collections;

public class MapPreview : MonoBehaviour
{

    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;


    public enum DrawMode { NoiseMap, Mesh, FalloffMap, Biomes };
    public DrawMode drawMode;

    public NoiseSettings noiseSettings;
    public MeshSettings meshSettings;
    public HeightMapSettings heightMapSettings;
    public TextureData textureData;

    public Material terrainMaterial;



    [Range(0, MeshSettings.numSupportedLODs - 1)]
    public int editorPreviewLOD;
    public bool autoUpdate;

    public int biomeGrid;
    public float noiseMult;
    public float noiseDist;

    public BiomeType[] biomes;
    public int seed;

    


    public void DrawMapInEditor()
    {
        int[,] biomeMap = Noise.GenerateBiomeMap(meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, seed, biomeGrid, biomes.Length, noiseMult, noiseDist);
        Color[] biomesColorMap = new Color[meshSettings.numVertsPerLine * meshSettings.numVertsPerLine];
        textureData.ApplyToMaterial(terrainMaterial);
        textureData.UpdateMeshHeights(terrainMaterial, heightMapSettings.minHeight, heightMapSettings.maxHeight);
        HeightMap heightMap = HeightMapGenerator.GenerateHeightMap(meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, heightMapSettings, Vector2.zero);
        for (int y = 0; y < meshSettings.numVertsPerLine; y++)
        {
            for (int x = 0; x < meshSettings.numVertsPerLine; x++)
            {
                biomesColorMap[y * meshSettings.numVertsPerLine + x] = biomes[biomeMap[x, y]].color;
            }
        }
        if (drawMode == DrawMode.NoiseMap)
        {
            DrawTexture(TextureGenerator.TextureFromHeightMap(heightMap));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            DrawMesh(MeshGenerator.GenerateTerrainMesh(heightMap.values, meshSettings, editorPreviewLOD));
        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            DrawTexture(TextureGenerator.TextureFromHeightMap(new HeightMap(FalloffGenerator.GenerateFalloffMap(meshSettings.numVertsPerLine), 0, 1)));
        }
         else if (drawMode == DrawMode.Biomes)
            DrawTexture(TextureGenerator.TextureFromColourMap(biomesColorMap,meshSettings.numVertsPerLine, meshSettings.numVertsPerLine));
    }





    public void DrawTexture(Texture2D texture)
    {
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height) / 10f;

        textureRender.gameObject.SetActive(true);
        meshFilter.gameObject.SetActive(false);
    }

    public void DrawMesh(MeshData meshData)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();

        textureRender.gameObject.SetActive(false);
        meshFilter.gameObject.SetActive(true);
    }



    void OnValuesUpdated()
    {
        if (!Application.isPlaying)
        {
            DrawMapInEditor();
        }
    }

    void OnTextureValuesUpdated()
    {
        textureData.ApplyToMaterial(terrainMaterial);
    }

    void OnValidate()
    {

        if (meshSettings != null)
        {
            meshSettings.OnValuesUpdated -= OnValuesUpdated;
            meshSettings.OnValuesUpdated += OnValuesUpdated;
        }
        if (heightMapSettings != null)
        {
            heightMapSettings.OnValuesUpdated -= OnValuesUpdated;
            heightMapSettings.OnValuesUpdated += OnValuesUpdated;
        }
        if (textureData != null)
        {
            textureData.OnValuesUpdated -= OnTextureValuesUpdated;
            textureData.OnValuesUpdated += OnTextureValuesUpdated;
        }

    }
    [System.Serializable]
    public struct BiomeType
    {
        public string name;
        public float noiseHeight;
        public Color color;
    }

}
