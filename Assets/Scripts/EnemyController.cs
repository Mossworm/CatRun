using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    public GameObject target;

    [SerializeField] float enemyMoveSpeed = 5.0f;
    [SerializeField] int enemyHP = 5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        target = FindNearestObjectByTag("Player");
        movement();
        if (enemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private GameObject FindNearestObjectByTag(string tag)
    {
        // Ž���� ������Ʈ ����� List �� ����
        var objects = GameObject.FindGameObjectsWithTag(tag).ToList();

        // LINQ �޼ҵ带 �̿��� ���� ����� ���� Ž��
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Arrow")
        {
            Debug.Log("����");
            enemyHP -= 1;
            Destroy(other.gameObject);
        }
    }


}
