using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCycle : MonoBehaviour
{
    public Light Moon;

    [Range(1, 3600)] public float sekundyCyklu;
    [Range(0, 1)] public float aktualnyCzas;
    private float mno¿nikCzasu = 1f;
    private float intensywnoœæKsie¿yca;
    private float intensywnoœæCieni;
    private Quaternion rotacjaS³oñca;
    // Start is called before the first frame update
    void Start()
    {
        intensywnoœæKsie¿yca = Moon.intensity;
        intensywnoœæCieni = Moon.shadowStrength;

    }
    public void UpdateMoon()
    {
        rotacjaS³oñca = Quaternion.Euler((aktualnyCzas * 360f) - 270, 0, 0);
        Moon.transform.localRotation = rotacjaS³oñca;
        float aktualnaintensywnoœæKsie¿yca = 1;
        float aktualnaIntensywnoœæCieni = 1;
        if (aktualnyCzas >= 0.23f || aktualnyCzas <= 0.75f)
        {
            aktualnaIntensywnoœæCieni = 0.8f;
        }
        if (aktualnyCzas <= 0.23f || aktualnyCzas >= 0.75f)
        {
            aktualnaintensywnoœæKsie¿yca = 0;
            aktualnaIntensywnoœæCieni = 1;

        }
        else if (aktualnyCzas < 0.25f)
        {
            aktualnaintensywnoœæKsie¿yca = Mathf.Clamp01((aktualnyCzas - 0.23f) * (1 / 0.02f));
            aktualnaIntensywnoœæCieni = Mathf.Clamp((aktualnyCzas - 0.23f) * (1 / 0.02f), 1, 0.6f);

        }
        else if (aktualnyCzas >= 0.73f)
        {
            aktualnaintensywnoœæKsie¿yca = Mathf.Clamp01((aktualnyCzas - 0.73f) * (1 / 0.02f));
            aktualnaIntensywnoœæCieni = Mathf.Clamp((aktualnyCzas - 0.73f) * (1 / 0.02f), 1, 0.6f);

        }
        Moon.intensity = intensywnoœæKsie¿yca * aktualnaintensywnoœæKsie¿yca;
        Moon.shadowStrength = intensywnoœæCieni * aktualnaIntensywnoœæCieni;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoon();

        aktualnyCzas += (Time.deltaTime / sekundyCyklu) * mno¿nikCzasu;

        if (aktualnyCzas >= 1)
        {
            aktualnyCzas = 0;
        }
    }
}
