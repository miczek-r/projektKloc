using UnityEngine;
using System.Collections;

[CreateAssetMenu()]
public class BiomeSettings : UpdatableData {

	public int biomeGrid;
    public float noiseMult;
    public float noiseDist;

    public BiomeType[] biomes;
    public int seed;

#if UNITY_EDITOR
    void OnValidate()
    {
        
        if (biomeGrid < 1)
            biomeGrid = 1;

        if (noiseMult < 1)
            noiseMult = 1;

        if (noiseDist < 0.0001f)
            noiseDist = 0.0001f;

        if (seed < 1)
            seed = 1;

        
    }
#endif
}