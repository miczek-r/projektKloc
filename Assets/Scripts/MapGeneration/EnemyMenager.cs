using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMenager : MonoBehaviour
{
    public int maxEnemies = 35;
    public GameObject[] mobsToSpawn;
    public GameObject enemyParent;
    public Transform playerPosition;

    void Start()
    {
        StartCoroutine(SpawnNewEnemy());
    }

    IEnumerator SpawnNewEnemy()
    {
        while (true)
        {
            foreach (Transform enemy in enemyParent.transform)
            {
                if (Vector3.Distance(enemy.position, playerPosition.position) > 200)
                {
                    Destroy(enemy.gameObject);
                }
            }
            var enemiesCount = enemyParent.transform.childCount;
            if (maxEnemies > enemiesCount)
            {
                Vector2 spawnPlace = Random.insideUnitCircle;
                var newEnemy = Instantiate(
                    mobsToSpawn[Random.Range(0, mobsToSpawn.Length)],
                    new Vector3(
                        playerPosition.position.x
                            + spawnPlace.x * 100
                            + (spawnPlace.x > 0 ? 20 : -20),
                        playerPosition.position.y + 5,
                        playerPosition.position.z
                            + spawnPlace.y * 100
                            + (spawnPlace.y > 0 ? 20 : -20)
                    ),
                    Quaternion.identity
                );
                newEnemy.transform.SetParent(enemyParent.transform);
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
    }
}
