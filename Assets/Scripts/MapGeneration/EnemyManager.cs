using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour 
{
    public Transform[] m_SpawnPoints;
    public GameObject m_EnemyPrefarb;
    Vector3 heigh;
    Vector3 People;
    int licznik;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void HeightCheck(float a = 10,float y=100, float c =10)
    {
        this.heigh.x=a;
        this.heigh.y=y;
        this.heigh.z=c;
    }
    void Update()
    {
        if (licznik == 1) 
        {

            People = GameObject.Find("Player").transform.position;
        }
        
        licznik++;
        if (licznik == 1000)
        {
            SpawnNewEnemy();
            licznik=0;
            
        }
        
    }
    
    void SpawnNewEnemy() 
    {    
        Instantiate(m_EnemyPrefarb, new Vector3(heigh.x,heigh.y+1.0f,heigh.z), Quaternion.identity);   
    }

}
