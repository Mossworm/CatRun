using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;

    private int direction = 0; //������
    private float shootSpeed = 20.0f;

    [SerializeField] float delayTime = 0.3f;
    private bool isDelay;

    void Start()
    {
        isDelay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = 1;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            direction = 2;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            direction = 3;
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            direction = 4;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            direction = 5;
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            direction = 6;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            direction = 7;
        }

        if (isDelay == false)
        {
            isDelay = true;
            ShootingArrow();
            StartCoroutine(ShootDelay());
        }


    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }

    void ShootingArrow()
    {
        Vector3 newPos = this.transform.position;
        //������ ����
        GameObject Arrow = Instantiate(arrowPrefab) as GameObject;
        Arrow.transform.position = newPos;
        Debug.Log("direction = " + direction);
        Rigidbody2D rbody = Arrow.GetComponent<Rigidbody2D>();
        if (direction == 0) //������
        {
            Arrow.transform.Rotate(0, 0, 90);
            rbody.AddForce(Vector2.right * shootSpeed, ForceMode2D.Impulse);
        }
        else if (direction == 1) //����
        {
            Arrow.transform.Rotate(0, 0, 270);
            rbody.AddForce(Vector2.left * shootSpeed, ForceMode2D.Impulse);
        }
        else if (direction == 2) //��
        {
            Arrow.transform.Rotate(0, 0, 180);
            rbody.AddForce(Vector2.up * shootSpeed, ForceMode2D.Impulse);
        }
        else if (direction == 3) //�Ʒ�
        {
            Arrow.transform.Rotate(0, 0, 0);
            rbody.AddForce(Vector2.down * shootSpeed, ForceMode2D.Impulse);
        }
        else if (direction == 4) //������ �밢�� ��
        {
            Arrow.transform.Rotate(0, 0, 135);
            rbody.AddForce(new Vector2(shootSpeed, shootSpeed), ForceMode2D.Impulse);
        }
        else if (direction == 5) //���� �밢�� ��
        {
            Arrow.transform.Rotate(0, 0, 215);
            rbody.AddForce(new Vector2(-shootSpeed, shootSpeed), ForceMode2D.Impulse);
        }
        else if (direction == 6) //������ ��
        {
            Arrow.transform.Rotate(0, 0, 45);
            rbody.AddForce(new Vector2(shootSpeed, -shootSpeed), ForceMode2D.Impulse);
        }
        else if (direction == 7) //���� ��
        {
            Arrow.transform.Rotate(0, 0, -45);
            rbody.AddForce(new Vector2(-shootSpeed, -shootSpeed), ForceMode2D.Impulse);
        }
    }


}
