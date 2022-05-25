using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject NetworkManager;

    public Vector3 postion;
    [SerializeField] float Speed = 10;

    void Start()
    {
        //��Ʈ��ũ �Ŵ����� ���� �������� ���� �޴� ���
        //this.NetworkManager = GameObject.Find("NetworkManager");
        postion = transform.position;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
            postion.x -= Speed*Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
            postion.x += Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            postion.y += Speed*Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            postion.y -= Speed * Time.deltaTime;
        }
        transform.position = postion;





    }





    ////��Ʈ��ũ�� ���� �żҵ�
    //public void transformCat(string txt)
    //{
    //    if (txt == "-1")
    //    {
    //        transform.Translate(-3, 0, 0);
    //    }
    //    else if (txt == "1")
    //    {
    //        transform.Translate(3, 0, 0);
    //    }

    //    if (txt == "-2")
    //    {
    //        transform.Translate(0, 3, 0);
    //    }
    //    else if (txt == "2")
    //    {
    //        transform.Translate(0, -3, 0);
    //    }
    //}


}
