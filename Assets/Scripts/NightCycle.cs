using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCycle : MonoBehaviour
{
    public Light Moon;

    [Range(1, 3600)] public float sekundyCyklu;
    [Range(0, 1)] public float aktualnyCzas;
    private float mno�nikCzasu = 1f;
    private float intensywno��Ksie�yca;
    private float intensywno��Cieni;
    private Quaternion rotacjaS�o�ca;
    // Start is called before the first frame update
    void Start()
    {
        intensywno��Ksie�yca = Moon.intensity;
        intensywno��Cieni = Moon.shadowStrength;

    }
    public void UpdateMoon()
    {
        rotacjaS�o�ca = Quaternion.Euler((aktualnyCzas * 360f) - 270, 0, 0);
        Moon.transform.localRotation = rotacjaS�o�ca;
        float aktualnaintensywno��Ksie�yca = 1;
        float aktualnaIntensywno��Cieni = 1;
        if (aktualnyCzas >= 0.23f || aktualnyCzas <= 0.75f)
        {
            aktualnaIntensywno��Cieni = 0.8f;
        }
        if (aktualnyCzas <= 0.23f || aktualnyCzas >= 0.75f)
        {
            aktualnaintensywno��Ksie�yca = 0;
            aktualnaIntensywno��Cieni = 1;

        }
        else if (aktualnyCzas < 0.25f)
        {
            aktualnaintensywno��Ksie�yca = Mathf.Clamp01((aktualnyCzas - 0.23f) * (1 / 0.02f));
            aktualnaIntensywno��Cieni = Mathf.Clamp((aktualnyCzas - 0.23f) * (1 / 0.02f), 1, 0.6f);

        }
        else if (aktualnyCzas >= 0.73f)
        {
            aktualnaintensywno��Ksie�yca = Mathf.Clamp01((aktualnyCzas - 0.73f) * (1 / 0.02f));
            aktualnaIntensywno��Cieni = Mathf.Clamp((aktualnyCzas - 0.73f) * (1 / 0.02f), 1, 0.6f);

        }
        Moon.intensity = intensywno��Ksie�yca * aktualnaintensywno��Ksie�yca;
        Moon.shadowStrength = intensywno��Cieni * aktualnaIntensywno��Cieni;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoon();

        aktualnyCzas += (Time.deltaTime / sekundyCyklu) * mno�nikCzasu;

        if (aktualnyCzas >= 1)
        {
            aktualnyCzas = 0;
        }
    }
}
