using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float x = 0;
    float y = 0;
    float speed = 5;    
    Rigidbody2D rigid;
    int jumpCount = 0;
    bool dirRight = true;
    Vector3 vec = Vector3.zero; //vec == 0,0,0
    Vector3 scaleVec = Vector3.one;
    SpriteRenderer sprend;
    Animator anim;

    void Start()
    {
        rigid = transform.GetComponent<Rigidbody2D>();        
        sprend = transform.GetComponent<SpriteRenderer>();
        anim = transform.GetComponent<Animator>();
    }

    void Update() 
    {
        //Input.GetAxis //뭔가 인풋을 세밀하게 받아옴 //-1~1 받아옴 //실수를 받음. 
        //알아둘점은.. 0.1.... 
        //Input.GetAxisRaw //뭔가 인풋을 -1,0,1 이렇게 받아옴//일반적인 키보드 인풋

        //Horizontal == 가로값
        //Vertical == 세로값

        x = Input.GetAxisRaw("Horizontal"); //가로값
        //y = Input.GetAxisRaw("Vertical"); //가로값        

        vec.x = x;
        //vec.y = y;

        //transform.position += vec * Time.deltaTime * speed; //Time.deltatime == 한프레임당 걸리는 시간... 
        //변수를 직접 수정하는 작업.

        transform.Translate(vec.normalized * Time.deltaTime * speed); //position += 누적더하기 기능과 동일함.
        //벡터.normalized == 정규화한 벡터. 크기가 1짜리인 기본 벡터로 줌.

        //좋은 컴퓨터라서 1초에 60프레임이 돈다고 칩시다.  =>   1/60 => 0.016
        //안좋은 컴퓨터가 있어요 1초에 30프레임이 돈다고 칩시다. => 1/30 => 0.032

        //Debug.Log($"x = {x}, y = {y}"); //-1,0,1

        //1번 스케일 반대로 하는법 //스케일 반대로 하는 게 베스트...
        if (vec.x != 0)//뭔가 내가 움직이고 있는 상태
        {
            scaleVec.x = vec.x;
            anim.SetBool("IsMove", true);
        }
        else //vex.x == x == 0 아무 입력이 없는 상태.
        {
            anim.SetBool("IsMove", false);
        }
        //anim.SetFloat("해당 float형 변수이름", 실수);
        //anim.SetInteger("해당 int형 변수이름", 정수);
        transform.localScale = scaleVec; //1,1,1 // -1,1,1


        ////2번은 스프라이트렌더러를 뒤집는 법. //이방법을 쓰면, 콜라이더는 안뒤집히고 그자리에 가만히 있기때문.
        //if (vec.x < 0)
        //{
        //    sprend.flipX = true;
        //}
        //else if(vec.x > 0)
        //{
        //    sprend.flipX = false;
        //}

        ////애니메이션 스피드 조절이 가능
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    anim.speed += 1; 
        //}
        //else if (Input.GetKeyDown(KeyCode.DownArrow))
        //{            
        //    if (anim.speed > 0.1f)
        //    {
        //        anim.speed -= 0.1f; //speed == 0 아예 안움직임
        //    }
        //}

        //좋은 방법은 아니지만 요것도 가능...
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("스킬1"))
        //{
        //    if (Input.GetKeyDown(KeyCode.스킬2번키))
        //    {
        //        일종의 콤보..? 특수기술...
        //    }
        //}

        ////내가 현재 Hit 애니메이션 플레이 중이고, 해당 애니메이션이
        ////80퍼 이상 플레이 되었다면...
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")
        //    && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f
        //    )
        //{            
        //        Debug.Log("이 애니메이션이 다 끝나갈때쯤 해야하는 일");         
        //}        

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (jumpCount<2)
            {
                jumpCount++;
            }
            else
            {
                return;
            }
            rigid.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
            //anim.SetTrigger("Jump2");            
        }
    }
    //코루틴 배움

    //IEnumerator 코루틴이름()
    //{
    //    //일단 실행
    //    yield return new WaitForSeconds(n초후);
    //    //n초후 실행
    //}

    //2단점프 구현


    //안쓰면 구현하지 말 것..

    ////어느 한쪽이라도 트리거 체크되어있으면 무조건 트리거 함수가 불림.
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //Debug.Log("트리거의 접촉 시작");
    // // 아래 아이템 체크는, Food스크립트로 옮겨졌음.
    //    if (collision.CompareTag("Item"))  //나와 부딪힌 대상의 태그가 Item이라면...
    //    {
    //        GameManager.Instance.AddScore();
    //        Destroy(collision.gameObject);//나랑 부딪힌 친구 객체를 삭제 
    //    }
    //}
    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("트리거의 접촉 해제");
    //}
    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("트리거의 접촉 중");
    //}
    //충돌함수가 안불려요=>
    //1 둘다 rigidbody가 없음
    //2 어느한쪽이 Trigger체크가 되어있음

    ////콜리젼 라인은 둘다 콜리젼이어야, 안겹치고 콜리젼 함수가 불림
    void OnCollisionEnter2D(Collision2D collision) 
    {
        jumpCount = 0;//땅에 닿았을때 점프가 다시 가능하도록 점프카운트를 초기화시켜줌...
        if (collision.gameObject.CompareTag("Trap"))
        {
            anim.SetTrigger("Hit");
        }
    }

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.Log("그냥 콜리젼 접촉 해제");
    //}
    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log("그냥 콜리젼 접촉 중");
    //}

    public void Test() //hit 도중에 불릴 테스트함수
    {
        Debug.Log("아야");
    }

    public int Test1()
    {
        Debug.Log("아야1");
        return 0;
    }
    public void Test2(int val)
    {
        Debug.Log("아야2");        
    }
    public int Test3(int val)
    {
        Debug.Log("아야3");
        return 0;
    }
    public int Test4(int val, string str) //매개변수가 2개이상이라서 애니메이션이벤트 추가 x
    {
        Debug.Log("아야4");
        return 0;
    }

    public void Test5(int val, string str)//매개변수가 2개이상이라서 애니메이션이벤트 추가 x
    {
        Debug.Log("아야5");        
    }

    public string Test6() 
    {
        Debug.Log("test6");
        return "";
    }
    //그 외에 string,int, float.... 이런 타입의 반환, 혹은 반환없음 + 매개변수 1개이하 면
    //애니메이션 이벤트로 추가 가능.
}
