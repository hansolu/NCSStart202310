using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{
    public WheelJoint2D[] wheel;
    JointMotor2D[] motor; //����ü ����.. ����ü�� ������� �� ���..
    int val = 0;
    void Start()
    {        
        motor = new JointMotor2D[2];
        motor[0] = wheel[0].motor;//����� ���� ����
        motor[1] = wheel[1].motor;
    }
    bool ison = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ison = !ison;
            for (int i = 0; i < motor.Length; i++)
            {
                motor[i].motorSpeed = 100 * (ison? 1: -1);//���ϴ� ���� ���� �����صΰ�
                wheel[i].motor = motor[i];//�� ������ ���� ��� == ����ü��
                //���� Ŭ������ ���� �־���...
            }
        }        
    }
}
