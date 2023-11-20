using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileTouch : MonoBehaviour, IPointerClickHandler,
    //IpointerDownHandler / IpointerUpHandler �� ����. Ŭ���� ������ ������ ���µ� ������ �� ����.
    
    // IpointerDownHandler / IpointerUpHandler �� ������ �ΰ� ������ �����ؾ� �����ϰ� ��밡��.

    IBeginDragHandler,/*�巡���� ����*/ IEndDragHandler,/*�巡���� ��*/ IDragHandler/*�巡�� ��*/

    //�ڵ鷯 ����� Ŭ���� ���� �غ� �Ǿ��־�� ����.
        //=> ����� ĵ������ �ִ� �ֵ��̴� == EventSystem�� �����ϰ�, ���� Raycast target Ȱ��ȭ�Ǿ��־�� ��.
        // ����� ���� �ʵ忡 �ִ� �ֵ��̴�. ex object��.. spriterender��, 3d object..ť�곪.. ��ü��..���׷��͵�..
        //=> �ݵ�� �ݶ��̴��� �־�� ��... 

{
    //�ܼ� Ŭ���� ���콺 Ŭ���� �����ϰ� ��밡��.
    //Input.mousePosition == 
    //��ġ�� �޾Ƽ� ���� �Ǻ��ؼ� �ϴ¹��

    void Update()
    {
        //Debug.LogWarning("���� ���ϴ� ����"); //��� ���� �� ���� ������ �޼����� ��
        //Debug.LogError("���ϴ� ����2"); //���� ������ ���� ������ �޼����� ��

        //�����ư �������� 
        //���� �Լ��� �� Quit�ϱ����� 
        //�� ���α׷����� �������͵� �� �����ϰ�, ���ٰ� �� ���ְ�
        //Application.Quit(); //���ø����̼��� ������ ����.
        //Application.targetFrameRate = 60; //�����Ӽӵ� �������� ������ �� �ֽ��ϴ�...


        //if (Input.GetMouseButton(0))//����Ͽ��� �Ǳ��. ���� �ȵɼ�����.. �ȵǴ� ��Ⱑ �ְų�..
        //{
        //}
        if (Input.touchCount > 0) //���� ��ġ�� ���Դٴ� ���̴ϱ�...
        {
            //����ü�� �׳� ������� �� ���⋚����...
            //�ϴ� ��ġ�� ���Դ��� �Ǻ����� �ʿ���...
            Touch touch1 = Input.GetTouch(0); //ù��° ��ġ

            Touch touch2 = Input.GetTouch(1); //�ٸ��հ��� ��ġ        
        
                
        //�ڵ��� �Ϲ������� ��� x���� �������� ���� ���� ��
        //y���� ������ ���� ���� ��
        //z���� ��������� ���� ���� ��
        //Input.acceleration.x
        
            if (touch1.phase == TouchPhase.Moved) //��ġ�� ����
        {
            //touch1.deltaPosition //�� �����Ӱ��� ��ġ ����..            
            //�� ���⼺�� ������ ����.
            //ĳ����.transform.Translate(touch1.deltaPosition/*�������Ӵ� �� �հ��� ������ ���⼺�� ����*/ 
            //    * �ӷ� ); //�� �巡�׿� �����ϰ� ĳ������ ��ġ�� �̵���ų�� ����.
        }

            //Began, //��ġ�� ������   //==IBeginDragHandler      
            //Moved �� Stationary���°� �縳�Ҽ� ����. //IDragHandler
            //Moved // �հ����� ��ũ������ ������                
            //Stationary  //�հ����� �����Ӿ��� �׳� ������ �ִ� ����        
            //Ended,  //�հ����� ���÷���. //== IEndDragHandler           
            //Canceled// ��ġ�� �ʹ����� ���ڱ� ���ͼ� ��ġ ������ �����.
            //=> ��ġ�� 5�� �̻������ 

            //foreach (Touch touch in Input.touches)
            //{
            //    if (touch.phase == TouchPhase.Began)
            //    {
            //        // Construct a ray from the current touch coordinates
            //        Ray ray = Camera.main.ScreenPointToRay(touch.position);
            //        if (Physics.Raycast(ray))
            //        {
            //            // Create a particle if hit                    
            //        }
            //    }
            //}
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {        
    }

    public void OnDrag(PointerEventData eventData)
    {     
    }

    public void OnEndDrag(PointerEventData eventData)
    {        
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {        
    }
}
