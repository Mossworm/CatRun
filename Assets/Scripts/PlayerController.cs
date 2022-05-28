using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    GameObject NetworkManager;

    public GameObject prfHPBar;
    public GameObject canvas;

    public bool is_mychr = false;

    RectTransform hpBar;

    private float height = -1.5f;

    public Vector3 postion;
    [SerializeField] float Speed = 10;
    char dir;
    static Dictionary<KeyCode, char> input_map = new Dictionary<KeyCode, char>();

    void Start()
    {
        //네트워크 매니저를 통해 서버에서 값을 받는 방식
        //this.NetworkManager = GameObject.Find("NetworkManager");
        postion = transform.position;

        hpBar = Instantiate(prfHPBar,canvas.transform).GetComponent<RectTransform>();

        input_map[KeyCode.LeftArrow] = (char)1;
        input_map[KeyCode.RightArrow] = (char)2;
        input_map[KeyCode.UpArrow] = (char)10;
        input_map[KeyCode.DownArrow] = (char)20;
    }

    void movement_input(char dir_) {
        dir = dir_;
    }

    void movement() {
        var r = dir % 10;

        if (r == 1) {
            this.GetComponent<SpriteRenderer>().flipX = false;
            postion.x -= Speed * Time.deltaTime;
        }
        else if (r == 2) {
            this.GetComponent<SpriteRenderer>().flipX = true;
            postion.x += Speed * Time.deltaTime;
        }
        if ((dir >= 10) && (dir < 20)) {
            postion.y += Speed * Time.deltaTime;
        }
        else if (dir >= 20) {
            postion.y -= Speed * Time.deltaTime;
        }
        transform.position = postion;
    }

    void Update()
    {
        movement_input((char)input_map.Select(x => Input.GetKey(x.Key) ? x.Value : 0).Sum());

        movement();
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
    }
}
