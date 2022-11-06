using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BiomeMapGenerator
{
     
    public static Color[] DisplayColorMap(int mapWidth, int mapHeight, BiomeSettings biomeSettings){
        int[,] biomeMap = GenerateBiomeMap(mapWidth, mapHeight, biomeSettings);
        Color[] biomesColorMap = new Color[mapWidth * mapHeight];
         for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                biomesColorMap[y * mapHeight + x] = biomeSettings.biomes[biomeMap[x, y]].color;
            }
        }
        return biomesColorMap;
    }
    public static int[,] GenerateBiomeMap(int mapWidth, int mapHeight, BiomeSettings biomeSettings)
    {
        int[,] biomesMap = new int[mapWidth, mapHeight];
        
        System.Random prng = new System.Random(biomeSettings.seed);
        SeedRandom.SetSeed(biomeSettings.seed);

        int xS = prng.Next(10, 20);
        int yS = prng.Next(10, 20);

        float offsetX = prng.Next(-100000, 100000);
        float offsetY = prng.Next(-100000, 100000);

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                int gridX = (int)Mathf.Floor(x / biomeSettings.biomeGrid);
                int gridY = (int)Mathf.Floor(y / biomeSettings.biomeGrid);

                if (x / biomeSettings.biomeGrid - gridX > 0.5f)
                    gridX -= 2;
                else
                    gridX -= 1;

                if (y / biomeSettings.biomeGrid - gridY > 0.5f)
                    gridY -= 2;
                else
                    gridY -= 1;

                int closest = 0;
                int closestDist = int.MaxValue;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        int curBiome = i * 4 + j;
                        int biomeX = SeedRandom.Get(gridX + i, gridY + j) % biomeSettings.biomeGrid; 
                        int biomeY = SeedRandom.Get(gridX + i, gridY + j) % biomeSettings.biomeGrid;

                        int dist = ((gridX + i) * biomeSettings.biomeGrid + biomeX - x) * ((gridX + i) * biomeSettings.biomeGrid + biomeX - x) +
                                   ((gridY + j) * biomeSettings.biomeGrid + biomeY - y) * ((gridY + j) * biomeSettings.biomeGrid + biomeY - y);

                        dist +=  (int)(Mathf.PerlinNoise(biomeSettings.noiseDist * ((gridX + i) * biomeSettings.biomeGrid + biomeX - x + offsetX) / 100f,
                                                         biomeSettings.noiseDist * ((gridY + j) * biomeSettings.biomeGrid + biomeY - y + offsetY) / 100f) * biomeSettings.noiseMult);

                        if (dist < closestDist)
                        {
                            closestDist = dist;
                            closest = curBiome;
                        }
                    }
                }
                               
                biomesMap[x, y] = SeedRandom.Get(gridX + closest / 4, gridY + closest % 4) % biomeSettings.biomes.Length;  
            }
        }
       
        return biomesMap;
    }   

    
    
}

 [System.Serializable]
    public struct BiomeType
    {
        public string name;
        public float noiseHeight;
        public Color color;
    }

