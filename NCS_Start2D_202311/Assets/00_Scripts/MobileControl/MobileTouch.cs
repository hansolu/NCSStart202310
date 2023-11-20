using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileTouch : MonoBehaviour, IPointerClickHandler,
    //IpointerDownHandler / IpointerUpHandler 도 존재. 클릭의 눌림과 떼어짐 상태도 가져올 수 있음.
    
    // IpointerDownHandler / IpointerUpHandler 이 포인터 두개 쌍으로 구현해야 무난하게 사용가능.

    IBeginDragHandler,/*드래그의 시작*/ IEndDragHandler,/*드래그의 끝*/ IDragHandler/*드래그 중*/

    //핸들러 사용은 클릭을 받을 준비가 되어있어야 가능.
        //=> 대상이 캔버스에 있는 애들이다 == EventSystem이 존재하고, 내가 Raycast target 활성화되어있어야 함.
        // 대상이 만약 필드에 있는 애들이다. ex object들.. spriterender나, 3d object..큐브나.. 구체나..뭐그런것들..
        //=> 반드시 콜라이더가 있어야 함... 

{
    //단순 클릭은 마우스 클릭과 동일하게 사용가능.
    //Input.mousePosition == 
    //터치를 받아서 내가 판별해서 하는방법

    void Update()
    {
        //Debug.LogWarning("내가 원하는 내용"); //노란 세모 로 내가 지정한 메세지가 뜸
        //Debug.LogError("원하는 내용2"); //빨간 원으로 내가 지정한 메세지가 뜸

        //종료버튼 만들어놓고 
        //종료 함수에 이 Quit하기전에 
        //내 프로그램에서 돌리던것들 다 정지하고, 없앨거 다 없애고
        //Application.Quit(); //어플리케이션의 완전한 종료.
        //Application.targetFrameRate = 60; //프레임속도 동적으로 조정할 수 있습니다...


        //if (Input.GetMouseButton(0))//모바일에서 되긴됨. 가끔 안될수있음.. 안되는 기기가 있거나..
        //{
        //}
        if (Input.touchCount > 0) //뭔가 터치가 들어왔다는 뜻이니까...
        {
            //구조체라서 그냥 비어있을 수 없기떄문에...
            //일단 터치가 들어왔는지 판별부터 필요함...
            Touch touch1 = Input.GetTouch(0); //첫번째 터치

            Touch touch2 = Input.GetTouch(1); //다른손가락 터치        
        
                
        //핸드폰 일반적으로 들듯 x축은 오른쪽을 따라서 양의 값
        //y축은 위쪽을 따라 양의 값
        //z축은 사용자쪽을 따라서 양의 값
        //Input.acceleration.x
        
            if (touch1.phase == TouchPhase.Moved) //터치의 시작
        {
            //touch1.deltaPosition //전 프레임과의 위치 차이..            
            //즉 방향성을 가지고 있음.
            //캐릭터.transform.Translate(touch1.deltaPosition/*한프레임당 내 손가락 움직임 방향성을 지님*/ 
            //    * 속력 ); //내 드래그와 동일하게 캐릭터의 위치를 이동시킬수 있음.
        }

            //Began, //터치를 시작함   //==IBeginDragHandler      
            //Moved 와 Stationary상태가 양립할수 없음. //IDragHandler
            //Moved // 손가락이 스크린에서 움직임                
            //Stationary  //손가락이 움직임없이 그냥 눌리고 있는 상태        
            //Ended,  //손가락이 들어올려짐. //== IEndDragHandler           
            //Canceled// 터치가 너무많이 갑자기 들어와서 터치 추적을 취소함.
            //=> 터치가 5개 이상들어오면 

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
