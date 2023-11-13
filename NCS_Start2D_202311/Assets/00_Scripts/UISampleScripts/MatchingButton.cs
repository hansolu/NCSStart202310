using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingButton : MonoBehaviour
{
    //눌렸을때
    //자기 그림 보여주고, 일정시간후에 보이지 않기
    //기본게임에게 정보 전달..

    //MatchingGame gameScript; //싱글톤으로 바뀌었으므로
                                //변수로 굳이 들고있을 필요가 없음
    int num = 0; //각자 자기 번호/ index
    Image img; //MatchingGame에 있던
               //ButtonImgs의 배열의 요소였던 것들 중 하나

    public void SetInfo(/*MatchingGame sc,*/ int num, Sprite spr) //내 번호 가지고 있음
    {
        //gameScript = sc;
        this.num = num;
        img = transform.GetChild(0).GetComponent<Image>();
        img.sprite = spr;
        img.gameObject.SetActive(false); //기본꺼두기

        this.transform.GetComponent<Button>().onClick.AddListener(ClickEvent);
    }

    void ClickEvent()
    {
        if (MatchingGame.Instance.CheckClick(num/*게임매니저에게 나의 인덱스 번호를 넘겨줫음*/))
        {
            SetImageFalse(true);
        }        
    }

    public void SetImageFalse(bool active)
    {
        img.gameObject.SetActive(active);
    }
}
