using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //�̰� �־�� ĵ������ �ö󰡴� ��ü���� ���� �� ���� ����.

public class ClickerGame : MonoBehaviour
{
    public GameObject obj1; //��� ������ ģ�� °�� ���� ����..
    public GameObject obj2; //�ٸ� ������ ģ�� °�� ���� ����.

    public Text countTxt;
    int count = 0; //���� Ƚ�� ��Ͽ�..

    void Start()
    {
        obj1.SetActive(true);
        obj2.SetActive(false);
    }

    public void AddCount()
    {
        //  <b> ���ڿ� ���� ǥ�� </b> 
        //  <i> ���Ÿ�ü�� ǥ�� </i> 
        // <color=#000000> �۾����ٲ� </color>
        count++;
        Debug.Log("ī��Ʈ Ȯ�� : "+count);
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
