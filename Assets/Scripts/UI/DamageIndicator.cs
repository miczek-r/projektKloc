using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public Color color_i,
        color_f;
    public Vector3 initialOffset,
        finalOffset; //position to drift to, relative to the gameObject's local origin
    public float fadeDuration;
    private float fadeStartTime;
    public TMP_Text DamageText;

    // Start is called before the first frame update
    void Start()
    {
        fadeStartTime = Time.time;
        DamageText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float progress = (Time.time - fadeStartTime) / fadeDuration;
        if (progress <= 1)
        {
            //lerp factor is from 0 to 1, so we use (FadeExitTime-Time.time)/fadeDuration
            transform.localPosition = Vector3.Lerp(initialOffset, finalOffset, progress);
            DamageText.color = Color.Lerp(color_i, color_f, progress);
        }
        else
            Destroy(gameObject);
    }
}
