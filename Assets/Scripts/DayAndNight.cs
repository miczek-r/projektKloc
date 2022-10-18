using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    public Light s³oñce;
    public Light moon;
    [Range(1,3600)] public float sekundyCyklu;
    [Range(0, 1)] public float aktualnyCzas;
    private float mno¿nikCzasu = 1f;
    private float intensywnoœæS³oñca;
    private float intensywnoœæCieni;
    private Quaternion rotacjaS³oñca;
    // Start is called before the first frame update
    void Start()
    {
        intensywnoœæS³oñca = s³oñce.intensity;
        intensywnoœæCieni = s³oñce.shadowStrength;

    }
    public void UpdateSun()
    {
        rotacjaS³oñca = Quaternion.Euler((aktualnyCzas * 360f) - 90, 0, 0);
        s³oñce.transform.localRotation = rotacjaS³oñca;
        float aktualnaIntensywnoœæS³oñca = 1;
        float aktualnaIntensywnoœæCieni = 1;
        if (aktualnyCzas >= 0.23f || aktualnyCzas <= 0.75f)
        {
            aktualnaIntensywnoœæCieni = 0.8f;
        }
        if (aktualnyCzas <= 0.23f || aktualnyCzas >= 0.75f)
        {
            aktualnaIntensywnoœæS³oñca = 0;
            aktualnaIntensywnoœæCieni = 1;

        }
        else if (aktualnyCzas < 0.25f)
        {
            aktualnaIntensywnoœæS³oñca = Mathf.Clamp01((aktualnyCzas - 0.23f) * (1 / 0.02f));
            aktualnaIntensywnoœæCieni = Mathf.Clamp((aktualnyCzas - 0.23f) * (1 / 0.02f), 1, 0.6f);

        }
        else if (aktualnyCzas >= 0.73f)
        {
            aktualnaIntensywnoœæS³oñca = Mathf.Clamp01((aktualnyCzas - 0.73f) * (1 / 0.02f));
            aktualnaIntensywnoœæCieni = Mathf.Clamp((aktualnyCzas - 0.73f) * (1 / 0.02f), 1, 0.6f);

        }
        s³oñce.intensity = intensywnoœæS³oñca * aktualnaIntensywnoœæS³oñca;
        s³oñce.shadowStrength = intensywnoœæCieni * aktualnaIntensywnoœæCieni;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSun();

        aktualnyCzas += (Time.deltaTime / sekundyCyklu) * mno¿nikCzasu;

        if (aktualnyCzas >= 1)
        {
            aktualnyCzas = 0;
        }
    }
}
