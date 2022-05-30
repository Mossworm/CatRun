using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameDirector GD;
    [SerializeField] float SpawnTime = 5.0f;
    public bool isDelay;
    // Start is called before the first frame update
    void Start()
    {
        isDelay = false;
        GD = FindObjectOfType<GameDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkManager.seed == 0) return;
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
        Enemy.transform.position = new Vector2(GD.get_rn_range(-5, 5) * 7, GD.get_rn_range(-5, 5) * 7);
    }
}
