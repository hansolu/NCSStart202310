using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapesScript : MonoBehaviour
{
    Transform RadioGroupTr;
    
    [SerializeField]
    Toggle[] shapesArr; //��� ���� �ϴ� ���� ��� �迭


    Transform ToggleGroupTr;

    [SerializeField]
    Toggle[] colorArr; //�� ���� �ϴ� ��� �迭

    Transform SpriteRenderersTr;

    [SerializeField]
    SpriteRenderer[] spriteRendrers;

    public Slider slider;

    Color color;

    void Start()
    {        
        RadioGroupTr = GameObject.Find("RadioGroup").transform;
        shapesArr = RadioGroupTr.GetComponentsInChildren<Toggle>();

        ToggleGroupTr = GameObject.Find("ToggleGroup").transform;
        colorArr = ToggleGroupTr.GetComponentsInChildren<Toggle>();

        SpriteRenderersTr = GameObject.Find("SpriteRenderers").transform;
        spriteRendrers = SpriteRenderersTr.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < colorArr.Length; i++)
        {
            colorArr[i].isOn = false;
        }        
        slider.value = 1;
        color = new Color(0, 0, 0, 1);

        ClickToggle_Shape();
        ClickToggle_Color();
        Slider_Alpha();
    }

    public void ClickToggle_Shape() //������ ������...
    {
        for (int i = 0; i < shapesArr.Length; i++)
        {
            spriteRendrers[i].gameObject.SetActive(shapesArr[i].isOn);
        }
    }

    public void ClickToggle_Color() //���� ���� ������
    {
        //�� ����
        color.r = colorArr[0].isOn ? 1f : 0f;
        color.g = colorArr[1].isOn ? 1f : 0f;
        color.b = colorArr[2].isOn ? 1f : 0f;        
                
        for (int i = 0; i < spriteRendrers.Length; i++)
        {
            spriteRendrers[i].color = color; //���� �� �ݿ�
        }        
    }

    public void Slider_Alpha() //���İ��� ������
    {
        color.a = slider.value;
        
        for (int i = 0; i < spriteRendrers.Length; i++)
        {
            spriteRendrers[i].color = color;
        }                
    }
}
