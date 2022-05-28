using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    GameObject NetworkManager;

    public GameObject prfHPBar;
    public GameObject HPGauge;
    public GameObject canvas;

    RectTransform hpBar;

    private float height = -1.5f;

    public Vector3 postion;
    [SerializeField] float Speed = 10;
    [SerializeField] float HP = 100;
    [SerializeField] float MaxHP = 100;

    char dir;
    static Dictionary<KeyCode, char> input_map = new Dictionary<KeyCode, char>();

    void Start()
    {
        HP = MaxHP;
        //네트워크 매니저를 통해 서버에서 값을 받는 방식
        //this.NetworkManager = GameObject.Find("NetworkManager");
        postion = transform.position;

        hpBar = Instantiate(prfHPBar, canvas.transform).GetComponent<RectTransform>();
        HPGauge = GameObject.Find("hp_bar");


        input_map[KeyCode.LeftArrow] = (char)1;
        input_map[KeyCode.RightArrow] = (char)2;
        input_map[KeyCode.UpArrow] = (char)10;
        input_map[KeyCode.DownArrow] = (char)20;
    }

    void movement_input(char dir_)
    {
        dir = dir_;
    }

    void movement()
    {
        var r = dir % 10;

        if (r == 1)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
            postion.x -= Speed * Time.deltaTime;
        }
        else if (r == 2)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
            postion.x += Speed * Time.deltaTime;
        }
        if ((dir >= 10) && (dir < 20))
        {
            postion.y += Speed * Time.deltaTime;
        }
        else if (dir >= 20)
        {
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
        HPGauge.GetComponent<Image>().fillAmount = HP / MaxHP;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            HP -= 15.0f * Time.deltaTime;
        }
    }
}