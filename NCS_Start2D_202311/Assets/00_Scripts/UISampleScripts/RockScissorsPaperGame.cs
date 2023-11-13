using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockScissorsPaperGame : MonoBehaviour
{
    string[] rspString = new string[] { "��", "����", "����"};
    
    public Image comImg; //��ǻ������ ���������� �׸��� �����.. ���ڰ���... ��..
    public Sprite[] RSPSprites; //���� ���������� �׸� ���ҽ�
    
    public Text text;
    int comnum = 0; //��ǻ�Ͱ� ������ ����������..����ǥ��
    bool isrepeat = false; 

    public GameObject Button; //����� ��ư

    Coroutine cor = null; 
    void Start()
    {        
        GameStart();
    }
    public void GameStart() //����۹�ư���� �޷��ִ� ���.
    {
        text.text = "������������ �������ּ���";
        isrepeat = true;
        if (cor == null)
        {
            cor = StartCoroutine(ImgShuffle());
        }

        Button.SetActive(false);//����� ��ư�� �����ٸ� ����� ��ư ��Ȱ��ȭ
    }
    public void PlayerChoose(int playernum)
    {
        isrepeat = false;
        StopCoroutine(cor);
        cor = null;
        comImg.sprite = RSPSprites[comnum];
        
        if (playernum== comnum)
        {
            text.text = rspString[playernum].ToString()+"�� �����Ͽ� �����ϴ�";
        }
        else
        {
            if (playernum > comnum)
            {
                if (playernum == 2 && comnum == 0)
                {
                    text.text = rspString[playernum]+ "�� �����Ͽ� �����ϴ�";
                }
                else
                {
                    text.text = rspString[playernum] + "�� �����Ͽ� �̰���ϴ�";
                }
            }
            else
            {
                if (playernum == 0 && comnum == 2)
                {
                    text.text = rspString[playernum]+ "�� �����Ͽ� �̰���ϴ�";
                }
                else
                {
                    text.text = rspString[playernum] + "�� �����Ͽ� �����ϴ�";                    
                }
            }            
        }

        Button.SetActive(true);//����� ��ư Ȱ��ȭ.
    }

    #region �ڷ�ƾ while ��� ������
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
        while (isrepeat/*true*/) //��Ȥ �ڷ�ƾ�� �������ѵ� while���� ���ӵǴ� ��찡 �־ bool�� �����ִ°��� ���� ������
        {
            comImg.sprite = RSPSprites[comnum];//0
            yield return new WaitForSeconds(0.08f); //1�ʶ�� ����
            comnum++;//1
            if (comnum > 2)
            {
                comnum = 0;
            }
        }        
    }
}
