using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;

    private int direction = 0; //오른쪽
    private float shootSpeed = 20.0f;

    private float delayTime = 0.5f;
    public bool isDelay;

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
            Debug.Log("화살발사");
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
        //프리팹 생성
        GameObject Arrow = Instantiate(arrowPrefab) as GameObject;
        Arrow.transform.position = newPos;
        Debug.Log("direction = " + direction);
        Rigidbody2D rbody = Arrow.GetComponent<Rigidbody2D>();
        if (direction == 0) //오른쪽
        {
            Arrow.transform.Rotate(0, 0, 90);
            rbody.AddForce(Vector2.right * shootSpeed, ForceMode2D.Impulse);
        }
        else if (direction == 1) //왼쪽
        {
            Arrow.transform.Rotate(0, 0, 270);
            rbody.AddForce(Vector2.left * shootSpeed, ForceMode2D.Impulse);
        }
        else if (direction == 2) //위
        {
            Arrow.transform.Rotate(0, 0, 180);
            rbody.AddForce(Vector2.up * shootSpeed, ForceMode2D.Impulse);
        }
        else if (direction == 3) //아래
        {
            Arrow.transform.Rotate(0, 0, 0);
            rbody.AddForce(Vector2.down * shootSpeed, ForceMode2D.Impulse);
        }
        else if (direction == 4) //오른쪽 대각선 위
        {
            Arrow.transform.Rotate(0, 0, 135);
            rbody.AddForce(new Vector2(shootSpeed, shootSpeed), ForceMode2D.Impulse);
        }
        else if (direction == 5) //왼쪽 대각선 위
        {
            Arrow.transform.Rotate(0, 0, 215);
            rbody.AddForce(new Vector2(-shootSpeed, shootSpeed), ForceMode2D.Impulse);
        }
        else if (direction == 6) //오른쪽 밑
        {
            Arrow.transform.Rotate(0, 0, 45);
            rbody.AddForce(new Vector2(shootSpeed, -shootSpeed), ForceMode2D.Impulse);
        }
        else if (direction == 7) //왼쪽 밑
        {
            Arrow.transform.Rotate(0, 0, -45);
            rbody.AddForce(new Vector2(-shootSpeed, -shootSpeed), ForceMode2D.Impulse);
        }
    }


}
