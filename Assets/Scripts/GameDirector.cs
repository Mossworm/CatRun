using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YCCSNET;

public class GameDirector : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        net_event<p_start>.subscribe((start) => {
            NetworkManager.seed = start.timestamp;
            NetworkManager.id = start.my_id;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseHp()
    {

    }
}
