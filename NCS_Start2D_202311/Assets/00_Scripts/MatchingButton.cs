using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingButton : MonoBehaviour
{
    //��������
    //�ڱ� �׸� �����ְ�, �����ð��Ŀ� ������ �ʱ�
    //�⺻���ӿ��� ���� ����..

    //MatchingGame gameScript; //�̱������� �ٲ�����Ƿ�
                                //������ ���� ������� �ʿ䰡 ����
    int num = 0; //���� �ڱ� ��ȣ/ index
    Image img; //MatchingGame�� �ִ�
               //ButtonImgs�� �迭�� ��ҿ��� �͵� �� �ϳ�

    public void SetInfo(/*MatchingGame sc,*/ int num, Sprite spr) //�� ��ȣ ������ ����
    {
        //gameScript = sc;
        this.num = num;
        img = transform.GetChild(0).GetComponent<Image>();
        img.sprite = spr;
        img.gameObject.SetActive(false); //�⺻���α�

        this.transform.GetComponent<Button>().onClick.AddListener(ClickEvent);
    }

    void ClickEvent()
    {
        if (MatchingGame.Instance.CheckClick(num/*���ӸŴ������� ���� �ε��� ��ȣ�� �ѰܢZ��*/))
        {
            SetImageFalse(true);
        }        
    }

    public void SetImageFalse(bool active)
    {
        img.gameObject.SetActive(active);
    }
}
