using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMobile : MonoBehaviour
{
    public static bool IsAndroid = true;
    // Start is called before the first frame update
    void Start()
    {
        //인풋매니저에다가 bool선언하고

        //awake단계에서 

        //내가 지금 안드로이드인지, 그외인지 판별하는
        //방법1. 근데 전처리기 쓰는 방식이 제일 빠름.
        //에디터일 경우 UNITY_EDITOR
        #if UNITY_ANDROID
                IsAndroid = true;
        #else
            IsAndroid = false;
        #endif

        //방법2 이게 좀더 명확해서 이방법 추천
        if (Application.platform == RuntimePlatform.Android)
        {
            IsAndroid = true;
        }
        else
        {
            IsAndroid = false;
        }

        Debug.Log("안드로이드 여부 :" +IsAndroid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
