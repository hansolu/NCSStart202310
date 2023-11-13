using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //이게 있어야 캔버스에 올라가는 객체들을 선언 및 접근 가능.

public class ClickerGame : MonoBehaviour
{
    public GameObject obj1; //녹색 슬라임 친구 째로 넣을 것임..
    public GameObject obj2; //다른 슬라임 친구 째로 넣을 것임.

    public Text countTxt;
    int count = 0; //누른 횟수 기록용..

    void Start()
    {
        obj1.SetActive(true);
        obj2.SetActive(false);
    }

    public void AddCount()
    {
        //  <b> 문자열 굵게 표시 </b> 
        //  <i> 이탤릭체로 표시 </i> 
        // <color=#000000> 글씨색바꿈 </color>
        count++;
        Debug.Log("카운트 확인 : "+count);
        //countTxt.text = "<color=#00ffff>"+ count +"</color>";
        AddColorOnText(count.ToString(), Color.magenta);

        if (count > 10)
        {
            obj1.SetActive(false);
            obj2.SetActive(true);
        }
    }

    void AddColorOnText(string val, Color color)
    {
        countTxt.color = color;
        countTxt.text = val;
    }
}
