using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    public GameObject target;

    [SerializeField] float enemyMoveSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        target = FindNearestObjectByTag("Player");
        movement();
    }

    private GameObject FindNearestObjectByTag(string tag)
    {
        // 탐색할 오브젝트 목록을 List 로 저장
        var objects = GameObject.FindGameObjectsWithTag(tag).ToList();

        // LINQ 메소드를 이용해 가장 가까운 적을 탐색
        var neareastObject = objects
            .OrderBy(obj =>
            {
                return Vector3.Distance(transform.position, obj.transform.position);
            })
        .FirstOrDefault();

        return neareastObject;
    }

    void movement()
    {
        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            transform.Translate(dir.normalized * enemyMoveSpeed * Time.deltaTime);
        }
        if (target.transform.position.x > this.transform.position.x)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
