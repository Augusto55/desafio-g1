using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombie;
    [SerializeField] private float zombieInterval = 3.5f;
    public GameObject gun;

    void Start()
    {
        StartCoroutine(SpawnEnemiesWhenReady());
    }

    private IEnumerator SpawnEnemiesWhenReady()
    {
        while (!gun.GetComponent<Gun>().boxInteraction)
        {
            yield return null;
        }
        StartCoroutine(SpawnEnemy(zombieInterval, zombie));
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        while (gun.GetComponent<Gun>().boxInteraction)
        {
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6f), 0), Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }
}