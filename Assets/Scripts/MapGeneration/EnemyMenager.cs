
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMenager : MonoBehaviour
{
    public GameObject m_EnemyPrefarb;
    Vector3 heigh;
    int licznik;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void HeightCheck(float a = 10, float y = 100, float c = 10)
    {
        this.heigh.x = a;
        this.heigh.y = y;
        this.heigh.z = c;
    }
    void Update()
    {

        licznik++;
        if (licznik == 1000)
        {
            SpawnNewEnemy();
            licznik = 0;

        }

    }

    void SpawnNewEnemy()
    {
        Instantiate(m_EnemyPrefarb, new Vector3(heigh.x, heigh.y + 1.0f, heigh.z), Quaternion.identity);
    }

}
