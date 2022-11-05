using UnityEngine;
using System.Collections;

public static class Noise {

	public enum NormalizeMode {Local, Global};

	public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, NoiseSettings settings, Vector2 sampleCentre) {
		float[,] noiseMap = new float[mapWidth,mapHeight];

		System.Random prng = new System.Random (settings.seed);
		Vector2[] octaveOffsets = new Vector2[settings.octaves];

		float maxPossibleHeight = 0;
		float amplitude = 1;
		float frequency = 1;

		for (int i = 0; i < settings.octaves; i++) {
			float offsetX = prng.Next (-100000, 100000) + settings.offset.x + sampleCentre.x;
			float offsetY = prng.Next (-100000, 100000) - settings.offset.y - sampleCentre.y;
			octaveOffsets [i] = new Vector2 (offsetX, offsetY);

			maxPossibleHeight += amplitude;
			amplitude *= settings.persistance;
		}

		float maxLocalNoiseHeight = float.MinValue;
		float minLocalNoiseHeight = float.MaxValue;

		float halfWidth = mapWidth / 2f;
		float halfHeight = mapHeight / 2f;


		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {

				amplitude = 1;
				frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < settings.octaves; i++) {
					float sampleX = (x-halfWidth + octaveOffsets[i].x) / settings.scale * frequency;
					float sampleY = (y-halfHeight + octaveOffsets[i].y) / settings.scale * frequency;

					float perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
					noiseHeight += perlinValue * amplitude;

					amplitude *= settings.persistance;
					frequency *= settings.lacunarity;
				}

				if (noiseHeight > maxLocalNoiseHeight) {
					maxLocalNoiseHeight = noiseHeight;
				} 
				if (noiseHeight < minLocalNoiseHeight) {
					minLocalNoiseHeight = noiseHeight;
				}
				noiseMap [x, y] = noiseHeight;

				if (settings.normalizeMode == NormalizeMode.Global) {
					float normalizedHeight = (noiseMap [x, y] + 1) / (maxPossibleHeight / 0.9f);
					noiseMap [x, y] = Mathf.Clamp (normalizedHeight, 0, int.MaxValue);
				}
			}
		}

		if (settings.normalizeMode == NormalizeMode.Local) {
			for (int y = 0; y < mapHeight; y++) {
				for (int x = 0; x < mapWidth; x++) {
					noiseMap [x, y] = Mathf.InverseLerp (minLocalNoiseHeight, maxLocalNoiseHeight, noiseMap [x, y]);
				}
			}
	}

		return noiseMap;
	}
	public static int[,] GenerateBiomeMap(int mapWidth, int mapHeight, int seed, int biomesGrid, int biomesNum, float noiseMult, float noiseDist)
    {
        int[,] biomesMap = new int[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        SeedRandom.SetSeed(seed);

        int xS = prng.Next(10, 20);
        int yS = prng.Next(10, 20);

        float offsetX = prng.Next(-100000, 100000);
        float offsetY = prng.Next(-100000, 100000);

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                int gridX = (int)Mathf.Floor(x / biomesGrid);
                int gridY = (int)Mathf.Floor(y / biomesGrid);

                if (x / biomesGrid - gridX > 0.5f)
                    gridX -= 2;
                else
                    gridX -= 1;

                if (y / biomesGrid - gridY > 0.5f)
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
                        int biomeX = SeedRandom.Get(gridX + i, gridY + j) % biomesGrid; 
                        int biomeY = SeedRandom.Get(gridX + i, gridY + j) % biomesGrid;

                        int dist = ((gridX + i) * biomesGrid + biomeX - x) * ((gridX + i) * biomesGrid + biomeX - x) +
                                   ((gridY + j) * biomesGrid + biomeY - y) * ((gridY + j) * biomesGrid + biomeY - y);

                        dist +=  (int)(Mathf.PerlinNoise(noiseDist * ((gridX + i) * biomesGrid + biomeX - x + offsetX) / 100f,
                                                         noiseDist * ((gridY + j) * biomesGrid + biomeY - y + offsetY) / 100f) * noiseMult);

                        if (dist < closestDist)
                        {
                            closestDist = dist;
                            closest = curBiome;
                        }
                    }
                }
                               
                biomesMap[x, y] = SeedRandom.Get(gridX + closest / 4, gridY + closest % 4) % biomesNum;
                      
            }
        }
    
        return biomesMap;
    }   

}

[System.Serializable]
public class NoiseSettings {
	public Noise.NormalizeMode normalizeMode;

	public float scale = 50;

	public int octaves = 6;
	[Range(0,1)]
	public float persistance =.6f;
	public float lacunarity = 2;

	public int seed;
	public Vector2 offset;

	public void ValidateValues() {
		scale = Mathf.Max (scale, 0.01f);
		octaves = Mathf.Max (octaves, 1);
		lacunarity = Mathf.Max (lacunarity, 1);
		persistance = Mathf.Clamp01 (persistance);
	}
}