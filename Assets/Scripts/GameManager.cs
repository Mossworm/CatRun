using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {

            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    public Text TimeText;
    float _timeCnt = 0;

    void Start()
    {
        
    }

    void Update()
    {
        _timeCnt += Time.deltaTime;
        OnGUI();
    }

    void OnGUI() 
    { 
        string SecStr;
        SecStr = "" + ((int)_timeCnt % 60).ToString("00");

        string MinStr;
        MinStr = "" + ((int)_timeCnt /60 % 60).ToString("00");
        TimeText.text = MinStr + ":" + SecStr; 
    }
}
