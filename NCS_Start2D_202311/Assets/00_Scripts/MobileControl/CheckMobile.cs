using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMobile : MonoBehaviour
{
    public static bool IsAndroid = true;
    // Start is called before the first frame update
    void Start()
    {
        //��ǲ�Ŵ������ٰ� bool�����ϰ�

        //awake�ܰ迡�� 

        //���� ���� �ȵ���̵�����, �׿����� �Ǻ��ϴ�
        //���1. �ٵ� ��ó���� ���� ����� ���� ����.
        //�������� ��� UNITY_EDITOR
        #if UNITY_ANDROID
                IsAndroid = true;
        #else
            IsAndroid = false;
        #endif

        //���2 �̰� ���� ��Ȯ�ؼ� �̹�� ��õ
        if (Application.platform == RuntimePlatform.Android)
        {
            IsAndroid = true;
        }
        else
        {
            IsAndroid = false;
        }

        Debug.Log("�ȵ���̵� ���� :" +IsAndroid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
