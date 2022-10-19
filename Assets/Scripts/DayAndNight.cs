using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    public Light lightSource;
    [Range(1, 3600)] public int lengthOfCycle = 3600;
    [Range(0, 1)] public float currentTime;
    private float timeMultiplier = 1f;
    private float lightIntensity;
    private float shadowIntensity;
    private Quaternion lightSourceRotation;
    // Start is called before the first frame update
    void Start()
    {
        lightIntensity = lightSource.intensity;
        shadowIntensity = lightSource.shadowStrength;

    }
    public void UpdateSun()
    {
        lightSourceRotation = Quaternion.Euler((currentTime * 360f) - 90, 0, 0);
        lightSource.transform.localRotation = lightSourceRotation;
        float currentlightIntensity = 1;
        float currentshadowIntensity = 1;
        if (currentTime >= 0.23f || currentTime <= 0.75f)
        {
            currentshadowIntensity = 0.8f;
        }
        if (currentTime <= 0.23f || currentTime >= 0.75f)
        {
            currentlightIntensity = 0;
            currentshadowIntensity = 1;

        }
        else if (currentTime < 0.25f)
        {
            currentlightIntensity = Mathf.Clamp01((currentTime - 0.23f) * (1 / 0.02f));
            currentshadowIntensity = Mathf.Clamp((currentTime - 0.23f) * (1 / 0.02f), 1, 0.6f);

        }
        else if (currentTime >= 0.73f)
        {
            currentlightIntensity = Mathf.Clamp01((currentTime - 0.73f) * (1 / 0.02f));
            currentshadowIntensity = Mathf.Clamp((currentTime - 0.73f) * (1 / 0.02f), 1, 0.6f);

        }
        lightSource.intensity = lightIntensity * currentlightIntensity;
        lightSource.shadowStrength = shadowIntensity * currentshadowIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSun();

        currentTime += (Time.deltaTime / lengthOfCycle) * timeMultiplier;

        if (currentTime >= 1)
        {
            currentTime = 0;
        }
    }
}
