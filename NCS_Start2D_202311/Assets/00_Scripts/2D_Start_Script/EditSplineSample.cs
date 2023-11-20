using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EditSplineSample : MonoBehaviour
{
    SpriteShapeController spritecontroller;
    Spline spline;
    int num = 0;
    int pointCount = 0; //��������Ʈ�������� ���� ���� �� ����
    void Start()
    {
        spritecontroller = GetComponent<SpriteShapeController>();
        spline = spritecontroller.spline;        
        pointCount = spline.GetPointCount();

        Vector3 vec = Vector3.zero;
        for (int i = 0; i < spline.GetPointCount(); i++)//�� �������� ���� ��� ���� ������ ������
        {
            vec = spline.GetPosition(i); //���ö����� i�� �ش�Ǵ� ���� ��ġ ��������
            Debug.Log(i + "���� ��ġ : " + vec);

            //�߾� pivot�� 0,0,0�� �ְ�,
            //�� �翷���� ������ �ִ� �������µ�
            //�ڵ忡�� ������ 0��°��(�� ���� ������ ��)�� ��ġ��
            //0,0,0���� �������,
            //���ʺ��� ���������� ���ʴ�� ��ĭ�� ������ �̵����ױ� ������
            spline.SetPosition(i, Vector3.right * i);//i�� �ش�Ǵ� ���� ��ġ ����
        }
    }    
}
