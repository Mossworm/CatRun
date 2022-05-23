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
            //transform.Translate(-3, 0, 0);
            NetworkManager.GetComponent<NetworkManager>().SendData(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //transform.Translate(3, 0, 0);
            NetworkManager.GetComponent<NetworkManager>().SendData(1);
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
    }
}
