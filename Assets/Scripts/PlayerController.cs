using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class PlayerController : MonoBehaviour
{
    public GameObject prfHPBar;
    public GameObject HPGauge;
    public GameObject canvas;

    public bool is_mychr = false;

    RectTransform hpBar;

    private float height = -1.5f;

    
    [SerializeField] float Speed = 10;
    [SerializeField] float HP = 100;
    [SerializeField] float MaxHP = 100;

    public char dir = (char)0;
    public char input_dir = (char)0;
    public char prev_input_dir = (char)0;
    static Dictionary<KeyCode, char> input_map = new Dictionary<KeyCode, char>();

    void Start()
    {
        HP = MaxHP;
        hpBar = Instantiate(prfHPBar, canvas.transform).GetComponent<RectTransform>();
        HPGauge = hpBar.GetComponentsInChildren<RectTransform>()[1].gameObject;
        Debug.Log(HPGauge.name);
        input_map[KeyCode.LeftArrow] = (char)1;
        input_map[KeyCode.RightArrow] = (char)2;
        input_map[KeyCode.UpArrow] = (char)10;
        input_map[KeyCode.DownArrow] = (char)20;
    }

    Vector2 get_vel_of_dir(char dir) {
        var r = dir % 10;
        Vector2 v2 = Vector2.zero;
        if (r == 1) {
            v2.x -= Speed;
        } else if (r == 2) {
            v2.x += Speed;
        }
        if ((dir >= 10) && (dir < 20)) {
            v2.y += Speed;
        } else if (dir >= 20) {
            v2.y -= Speed;
        }
        return v2;
    }

    void movement()
    {
        var r = dir % 10;
        if (r == 1)         GetComponent<SpriteRenderer>().flipX = false;
        else if (r == 2)    GetComponent<SpriteRenderer>().flipX = true;

        transform.position += (Vector3)get_vel_of_dir(dir) * Time.deltaTime;
        
        var pos = transform.position;
        if (pos.x < -25)
        {
            pos.x = -25;
        }
        if (pos.x > 25)
        {
            pos.x = 25;
        }
        if (pos.y < -25)
        {
            pos.y = -25;
        }
        if (pos.y > 25)
        {
            pos.y = 25;
        }
        transform.position = pos;
    }

    void Update()
    {
        if (is_mychr) {
            input_dir = (char)input_map.Select(x => Input.GetKey(x.Key) ? x.Value : 0).Sum();
            if (prev_input_dir != input_dir) {
                prev_input_dir = input_dir;
                YCCSNET.p_input input = new YCCSNET.p_input();
                input.input = input_dir;
                NetworkManager.send(input);
            }
        }
        for (int i = 0; i < GameDirector.sync.Count; i++) {
            var s = GameDirector.sync[i];

            if (is_mychr) {
                if (NetworkManager.id == s.id) {
                    if (!s.is_updated) {
                        transform.position -= (Vector3)get_vel_of_dir(dir) * (float)((Timestamp - s.time) / 1000f);
                        transform.position += (Vector3)get_vel_of_dir(s.dir) * (float)((Timestamp - s.time) / 1000f);


                        Debug.Log((Vector3)get_vel_of_dir(s.dir) * (float)((Timestamp - s.time) / 1000f));
                        dir = s.dir;
                        s.is_updated = true;
                    }
                }
            } else {
                if (NetworkManager.id != s.id) {
                    if (!s.is_updated) {
                        transform.position -= (Vector3)get_vel_of_dir(dir) * (float)((Timestamp - s.time) / 1000f);
                        transform.position += (Vector3)get_vel_of_dir(s.dir) * (float)((Timestamp - s.time) / 1000f);
                        
                        
                        dir = s.dir;
                        s.is_updated = true;
                    }
                }
            }
        }

        movement();

        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        HPGauge.GetComponent<Image>().fillAmount = HP / MaxHP;
    }
    static int Timestamp => (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            HP -= 15.0f * Time.deltaTime;
        }
    }
}