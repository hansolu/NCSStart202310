using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{
    public WheelJoint2D[] wheel;
    JointMotor2D[] motor; //구조체 선언.. 구조체는 비어있을 수 없어서..
    int val = 0;
    void Start()
    {        
        motor = new JointMotor2D[2];
        motor[0] = wheel[0].motor;//당시의 모터 정보
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
                motor[i].motorSpeed = 100 * (ison? 1: -1);//원하는 값을 먼저 세팅해두고
                wheel[i].motor = motor[i];//그 세팅한 값의 덩어리 == 구조체를
                //실제 클래스에 값을 넣어줌...
            }
        }        
    }
}
