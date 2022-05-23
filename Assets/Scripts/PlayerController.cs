using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject NetworkManager;
    // Start is called before the first frame update
    void Start()
    {
        this.NetworkManager = GameObject.Find("NetworkManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            NetworkManager.GetComponent<NetworkManager>().SendData(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NetworkManager.GetComponent<NetworkManager>().SendData(1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            NetworkManager.GetComponent<NetworkManager>().SendData(-2);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            NetworkManager.GetComponent<NetworkManager>().SendData(2);
        }
    }

    //네트워크를 위한 매소드
    public void transformCat(string txt)
    {
        if (txt == "-1")
        {
            transform.Translate(-3, 0, 0);
        }
        else if (txt == "1")
        {
            transform.Translate(3, 0, 0);
        }

        if (txt == "-2")
        {
            transform.Translate(0, 3, 0);
        }
        else if (txt == "2")
        {
            transform.Translate(0, -3, 0);
        }
    }
}
