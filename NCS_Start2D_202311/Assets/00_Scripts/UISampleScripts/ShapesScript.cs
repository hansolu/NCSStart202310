using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapesScript : MonoBehaviour
{
    Transform RadioGroupTr;
    
    [SerializeField]
    Toggle[] shapesArr; //모양 선택 하는 라디오 토글 배열


    Transform ToggleGroupTr;

    [SerializeField]
    Toggle[] colorArr; //색 선택 하는 토글 배열

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

    public void ClickToggle_Shape() //도형만 설정함...
    {
        for (int i = 0; i < shapesArr.Length; i++)
        {
            spriteRendrers[i].gameObject.SetActive(shapesArr[i].isOn);
        }
    }

    public void ClickToggle_Color() //색상 값만 설정함
    {
        //색 세팅
        color.r = colorArr[0].isOn ? 1f : 0f;
        color.g = colorArr[1].isOn ? 1f : 0f;
        color.b = colorArr[2].isOn ? 1f : 0f;        
                
        for (int i = 0; i < spriteRendrers.Length; i++)
        {
            spriteRendrers[i].color = color; //실제 색 반영
        }        
    }

    public void Slider_Alpha() //알파값만 수정함
    {
        color.a = slider.value;
        
        for (int i = 0; i < spriteRendrers.Length; i++)
        {
            spriteRendrers[i].color = color;
        }                
    }
}
