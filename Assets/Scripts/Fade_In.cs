using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade_In : MonoBehaviour

{
    GameObject SplashObj;               //�ǳڿ�����Ʈ
    Image image;                            //�ǳ� �̹���
    private bool checkbool = false;     //������ ���� ������ ����


    void Awake()

    {
        SplashObj = this.gameObject;                         //��ũ��Ʈ ������ ������Ʈ
        image = SplashObj.GetComponent<Image>();    //�ǳڿ�����Ʈ�� �̹��� ����

    }



    void Update()

    {
        StartCoroutine("MainSplash");                        //�ڷ�ƾ    //�ǳ� ������ ����
        if (checkbool)                                            //���� checkbool �� ���̸�
        {                     
            SceneManager.LoadScene(1); //�� ����
        }
    }



    IEnumerator MainSplash()
    {
        Color color = image.color;                            //color �� �ǳ� �̹��� ����
        for (int i = 0; i <= 100; i++)                            //for�� 100�� �ݺ� 100����Ŭ �� ����
        {
            color.a += Time.deltaTime * 0.01f;               //�̹��� ���� ���� Ÿ�� ��Ÿ �� * 0.01 ���Ѵ�
            image.color = color;                                //�ǳ� �̹��� �÷��� �ٲ� ���İ� ����
            if (color.a >= 1)                        //���� �ǳ� �̹��� ���� ���� 1���� ũ��
            {
                checkbool = true;                              //checkbool �� 
            }
        }
        yield return null;                                        //�ڷ�ƾ ����
    }
}