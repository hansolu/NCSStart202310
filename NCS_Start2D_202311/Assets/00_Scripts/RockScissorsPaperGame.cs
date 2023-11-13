using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockScissorsPaperGame : MonoBehaviour
{
    string[] rspString = new string[] { "보", "가위", "바위"};
    
    public Image comImg; //컴퓨터측의 가위바위보 그림을 출력할.. 액자같은... 것..
    public Sprite[] RSPSprites; //실제 가위바위보 그림 리소스
    
    public Text text;
    int comnum = 0; //컴퓨터가 선택한 가위바위보..숫자표현
    bool isrepeat = false; 

    public GameObject Button; //재시작 버튼

    Coroutine cor = null; 
    void Start()
    {        
        GameStart();
    }
    public void GameStart() //재시작버튼에도 달려있는 기능.
    {
        text.text = "가위바위보를 선택해주세요";
        isrepeat = true;
        if (cor == null)
        {
            cor = StartCoroutine(ImgShuffle());
        }

        Button.SetActive(false);//재시작 버튼을 눌렀다면 재시작 버튼 비활성화
    }
    public void PlayerChoose(int playernum)
    {
        isrepeat = false;
        StopCoroutine(cor);
        cor = null;
        comImg.sprite = RSPSprites[comnum];
        
        if (playernum== comnum)
        {
            text.text = rspString[playernum].ToString()+"를 선택하여 비겼습니다";
        }
        else
        {
            if (playernum > comnum)
            {
                if (playernum == 2 && comnum == 0)
                {
                    text.text = rspString[playernum]+ "를 선택하여 졌습니다";
                }
                else
                {
                    text.text = rspString[playernum] + "를 선택하여 이겼습니다";
                }
            }
            else
            {
                if (playernum == 0 && comnum == 2)
                {
                    text.text = rspString[playernum]+ "를 선택하여 이겼습니다";
                }
                else
                {
                    text.text = rspString[playernum] + "를 선택하여 졌습니다";                    
                }
            }            
        }

        Button.SetActive(true);//재시작 버튼 활성화.
    }

    #region 코루틴 while 사용 위험사례
    //void AAA()
    //{
    //    while (true)
    //    {
    //        if (cor == null)
    //        {
    //            cor = StartCoroutine(Sample());
    //        }
    //        comnum++;

    //        if (comnum > 2)
    //        {
    //            comnum = 0;
    //        }
    //        cor = null;
    //    }
    //}

    //IEnumerator Sample()
    //{
    //    yield return new WaitForSeconds(1f);
    //    comImg.sprite = RSPSprites[comnum];
    //}
    #endregion

    IEnumerator ImgShuffle()
    {                
        while (isrepeat/*true*/) //간혹 코루틴을 정지시켜도 while문이 지속되는 경우가 있어서 bool을 따로주는것이 좀더 안전ㅜ
        {
            comImg.sprite = RSPSprites[comnum];//0
            yield return new WaitForSeconds(0.08f); //1초라고 가정
            comnum++;//1
            if (comnum > 2)
            {
                comnum = 0;
            }
        }        
    }
}
