using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    [SerializeField] float SpawnTime = 5.0f;
    public bool isDelay;

    // Start is called before the first frame update
    void Start()
    {
        isDelay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDelay == false)
        {
            isDelay = true;
            EnemySpawn();
            StartCoroutine(SpawnDelay());
        }
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(SpawnTime);
        isDelay = false;
    }
    void EnemySpawn()
    {
        GameObject Enemy = Instantiate(EnemyPrefab) as GameObject;
        Vector3 newPos = Vector3.zero;

        newPos.x = Random.Range(-30f, 30f);
        newPos.y = Random.Range(-30f, 30f);

        while (-25<newPos.x && newPos.x<25 && -25 < newPos.y && newPos.y < 25)
        {
            newPos.x = Random.Range(-30f, 30f);
            newPos.y = Random.Range(-30f, 30f);
        }

        Enemy.transform.position = newPos;
    }
}
