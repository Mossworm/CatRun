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

        newPos.x = Random.Range(-35f, 35f);
        newPos.y = Random.Range(-35f, 35f);

        while (-30<newPos.x && newPos.x<30 && -30 < newPos.y && newPos.y < 30)
        {
            newPos.x = Random.Range(-35f, 35f);
            newPos.y = Random.Range(-35f, 35f);
        }

        Enemy.transform.position = newPos;
    }
}
