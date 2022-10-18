using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    public Light s�o�ce;
    public Light moon;
    [Range(1,3600)] public float sekundyCyklu;
    [Range(0, 1)] public float aktualnyCzas;
    private float mno�nikCzasu = 1f;
    private float intensywno��S�o�ca;
    private float intensywno��Cieni;
    private Quaternion rotacjaS�o�ca;
    // Start is called before the first frame update
    void Start()
    {
        intensywno��S�o�ca = s�o�ce.intensity;
        intensywno��Cieni = s�o�ce.shadowStrength;

    }
    public void UpdateSun()
    {
        rotacjaS�o�ca = Quaternion.Euler((aktualnyCzas * 360f) - 90, 0, 0);
        s�o�ce.transform.localRotation = rotacjaS�o�ca;
        float aktualnaIntensywno��S�o�ca = 1;
        float aktualnaIntensywno��Cieni = 1;
        if (aktualnyCzas >= 0.23f || aktualnyCzas <= 0.75f)
        {
            aktualnaIntensywno��Cieni = 0.8f;
        }
        if (aktualnyCzas <= 0.23f || aktualnyCzas >= 0.75f)
        {
            aktualnaIntensywno��S�o�ca = 0;
            aktualnaIntensywno��Cieni = 1;

        }
        else if (aktualnyCzas < 0.25f)
        {
            aktualnaIntensywno��S�o�ca = Mathf.Clamp01((aktualnyCzas - 0.23f) * (1 / 0.02f));
            aktualnaIntensywno��Cieni = Mathf.Clamp((aktualnyCzas - 0.23f) * (1 / 0.02f), 1, 0.6f);

        }
        else if (aktualnyCzas >= 0.73f)
        {
            aktualnaIntensywno��S�o�ca = Mathf.Clamp01((aktualnyCzas - 0.73f) * (1 / 0.02f));
            aktualnaIntensywno��Cieni = Mathf.Clamp((aktualnyCzas - 0.73f) * (1 / 0.02f), 1, 0.6f);

        }
        s�o�ce.intensity = intensywno��S�o�ca * aktualnaIntensywno��S�o�ca;
        s�o�ce.shadowStrength = intensywno��Cieni * aktualnaIntensywno��Cieni;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSun();

        aktualnyCzas += (Time.deltaTime / sekundyCyklu) * mno�nikCzasu;

        if (aktualnyCzas >= 1)
        {
            aktualnyCzas = 0;
        }
    }
}
