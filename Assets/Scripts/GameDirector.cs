using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YCCSNET;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System;
using UnityEngine.SceneManagement;

public class movement_sync_t {
    public char id;
    public char dir;
    public int time;
    public bool is_updated;
}

public class GameDirector : MonoBehaviour
{
    public static List<movement_sync_t> sync = new List<movement_sync_t>();

    public static bool game_start_trigger = false;
    static int Timestamp => (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        packet_mgr.packet_load();
        net_event<p_start>.subscribe((start) => {
            NetworkManager.seed = start.timestamp;
            NetworkManager.id = start.my_id;
            game_start_trigger = true;
            OnGameStart();
        });

        net_event<p_input>.subscribe((Input) => {
            if (sync.Select(x=>x.is_updated ? 1 : 0).Sum() == sync.Count) {
                sync.Clear();
            }
            var s = new movement_sync_t();
            s.is_updated = false;
            s.time = Input.timestamp;
            s.dir = Input.input;
            s.id = Input.id;
            sync.Add(s);
        });
    }

    public List<float> random_nums;
    int idx = 0;
    public float get_rn_range(int min, int max) {
        return random_nums[idx++] % max + min;
    }

    void OnGameStart() {
        System.Random rn = new System.Random(NetworkManager.seed);

        for (int i = 0; i < 10000; i++) {
            random_nums.Add(rn.Next() + (float)rn.NextDouble());
        }
    }

    IEnumerator wait_for_time_and(float w, Action act) {
        yield return new WaitForSeconds(w);
        act();
    }

    // Update is called once per frame
    void Update()
    {
        if (game_start_trigger) 
        {
            game_start_trigger = false;
            StartCoroutine(wait_for_time_and(1f - (float)((NetworkManager.seed - Timestamp) / 1000f), () => {
                SceneManager.LoadScene(3);
            }));
        }
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
