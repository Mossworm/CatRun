using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YCCSNET;
using System.Linq;

public class GameDirector : MonoBehaviour
{
    public PlayerController my_chr => FindObjectsOfType<PlayerController>().Where(x => x.is_mychr).First();
    public PlayerController other_chr => FindObjectsOfType<PlayerController>().Where(x => !x.is_mychr).First();

    // Start is called before the first frame update
    void Start()
    {
        net_event<p_start>.subscribe((start) => {
            NetworkManager.seed = start.timestamp;
            NetworkManager.id = start.my_id;
        });

        net_event<p_input>.subscribe((Input) => {
            if (Input.id == NetworkManager.id) {
                my_chr.dir = Input.input;
            }else {
                other_chr.dir = Input.input;
            }
            // timestemp로 동기화 처리!
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
