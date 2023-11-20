using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EditSplineSample : MonoBehaviour
{
    SpriteShapeController spritecontroller;
    Spline spline;
    int num = 0;
    int pointCount = 0; //스프라이트셰이프가 가진 점의 총 개수
    void Start()
    {
        spritecontroller = GetComponent<SpriteShapeController>();
        spline = spritecontroller.spline;        
        pointCount = spline.GetPointCount();

        Vector3 vec = Vector3.zero;
        for (int i = 0; i < spline.GetPointCount(); i++)//이 셰이프가 가진 모든 점의 개수를 가져옴
        {
            vec = spline.GetPosition(i); //스플라인의 i에 해당되는 점의 위치 가져오기
            Debug.Log(i + "점의 위치 : " + vec);

            //중앙 pivot이 0,0,0에 있고,
            //그 양옆으로 점들이 있는 구조였는데
            //코드에서 강제로 0번째점(즉 가장 왼쪽의 점)의 위치를
            //0,0,0으로 만들었고,
            //왼쪽부터 오른쪽으로 차례대로 한칸씩 옆으로 이동시켰기 때문에
            spline.SetPosition(i, Vector3.right * i);//i에 해당되는 점의 위치 세팅
        }
    }    
}
