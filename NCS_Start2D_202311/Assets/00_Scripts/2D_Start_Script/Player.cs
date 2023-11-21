using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHit
{
    public Camera cam;
    float x = 0;
    //float y = 0;
    float speed = 5;    
    Rigidbody2D rigid;
    int jumpCount = 0;    
    Vector3 vec = Vector3.zero; //vec == 0,0,0
    Vector3 scaleVec = Vector3.one;
    Vector3 direction = Vector3.zero;
    float knockBackPower = 3;
    bool isHit = false;
    SpriteRenderer sprend;
    Animator anim;

    
    
    Constructure.Stat mystat;

    #region 공격용 추가

    public Camera camera;
    Vector2 attackPos = Vector2.zero;
    Vector2 lookdir = Vector2.zero;
    float angle = 0;
    public Transform weaponTr;
        
    int layermask = 0;
    public LayerMask selectLayer;
    #endregion

    void Start()
    {
        mystat = new Constructure.Stat(100, 10);
        rigid = transform.GetComponent<Rigidbody2D>();        
        sprend = transform.GetComponent<SpriteRenderer>();
        anim = transform.GetComponent<Animator>();
    
        //~selectLayer //선택한 레이어 제외한 모두..
        layermask = 1 << LayerMask.NameToLayer("Enemy") /*| 1 << LayerMask.NameToLayer("Enemy2")*/;
        ////만약 해당 layermask만 제외한 나머지 레이어들
        //~layermask
    }

    void Update() 
    {
        if (isHit)
        {
            return;
        }
        //Input.GetAxis //뭔가 인풋을 세밀하게 받아옴 //-1~1 받아옴 //실수를 받음. 
        //알아둘점은.. 0.1.... 
        //Input.GetAxisRaw //뭔가 인풋을 -1,0,1 이렇게 받아옴//일반적인 키보드 인풋

        //Horizontal == 가로값
        //Vertical == 세로값

        x = Input.GetAxisRaw("Horizontal"); //가로값//왼쪽이 눌리면 -1, 아무것도 안누르면 0, 오른키 누르면 1
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
            scaleVec.x = vec.x; //-1 이랑 1
            anim.SetBool("IsMove", true);
        }
        else //vex.x == x == 0 아무 입력이 없는 상태.
        {
            anim.SetBool("IsMove", false);
        }
        //anim.SetFloat("해당 float형 변수이름", 실수);
        //anim.SetInteger("해당 int형 변수이름", 정수);
        transform.localScale = scaleVec; //1,1,1 오른쪽을 볼때 사람 스케일 // -1,1,1 왼쪽을 볼때의 사람스케일


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

        if (Input.GetKeyDown(KeyCode.Z))// 하여간 z키를 누르면 할 작업...
        {            
            TilemapManager.Instance.SetTile(transform.position + 
                Vector3.right * scaleVec.x,  //왼쪽을 보면 (1,0,0) * -1 == (-1,0,0)
                
                AllEnum.TileKind.Coin_Bronze);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            TilemapManager.Instance.SetTile(transform.position + Vector3.right * scaleVec.x, AllEnum.TileKind.End);
            //TilemapManager.Instance.SwapTile(
            //    TilemapManager.Instance.GetTile( transform.position + Vector3.right * scaleVec.x), null);
        }


        attackPos = camera.ScreenToWorldPoint(Input.mousePosition); //내가 마우스 클릭한 위치를 월드 포지션으로 바꿈
        RotateWeapon();
        if (Input.GetMouseButtonDown(0)) //마우스왼쪽클릭
        {
            //camera.ScreenPointToRay(/*어떤점*/); //카메라의 스크린상에 어떤 점을 레이 (광선)으로 쏘겠다. 
            ////우리는 2d카메라 == orthographic이고 이거는 직영 이라서 z값이 상관없는 상태
            //camera.ScreenToWorldPoint(/*어떤점*/); //어떤점을 월드포인트로 변경하겠음. 월드 포지션...  화면상의 x,y값으로 바꾸겠다.

            //쿨타임이 덜찼으면 돌아가기
            //공격하기
            
            //distance로 비교해도되겠지만.. 
            if (Vector2.Distance(attackPos, transform.position) <= 2) //이건 둥근 경계
            {
                Debug.Log("공격");
            }

            ////이거는 사각경계
            //if (attackPos.x <= transform.position.x + 1 
            //    && attackPos.x >= transform.position.x - 1
            //    && attackPos.y <= transform.position.y + 1
            //    && attackPos.y >= transform.position.y - 1)
            //{                
            //}

            //레이캐스트

            //hits =Physics2D.Raycast(/*레이의 시작점*//*tranform.position 나의 위치. 이걸쓰면 대체적으로 나의 발밑에서 레이가 나갈것이기 때문에*/ 
            //    weaponTr.position, /*방향벡터*/ attackPos /*, 거리 주기 가능. 거리를 지정안하면 float.MaxValue  혹은 Mathf.Infinity 무한과 같음, 레이어마스크 */ );

            //RaycastHit2D hits = Physics2D.Raycast(weaponTr.position, attackPos, 2, layermask);
            ////구조체로 애초에 결과값을 받는 방법과
            //if (hits.collider !=null) //존재하고있음
            //{
            //    //hits.transform.GetComponent<IHit>().Hit();
            //    //Instantiate(이펙트 프리팹, hits.point, Quaternion.identity);//피격이펙트 생성
            //    //if (hits.rigidbody != null)
            //    //    hits.rigidbody.AddForce(attackPos * /*힘*/10);
            //}         

            //Physics2D.BoxCast 
            //Physics2D.CapsuleCast(weaponTr.position, 5/*반지름*/,캡슐을 세로로할건지 가로로할건지.. );
            //Physics2D.CircleCast
            //Physics2D.Linecast

            //==================== ~Cast 라인은 방향벡터로 쏘아냄. 그리고 RaycastHit2D 구조체를 반환함. 먼저 닿는걸 반환하고, All~함수를 써야 여러개 다됨
            //==============   overlap~ 라인은 해당 지점에 콕 하고 찍음. 그리고 Collider2D를 반환함.

            //Physics2D.OverlapArea //직사각형
            //Physics2D.OverlapBox //정사각형
            //Physics2D.OverlapCapsule //캡슐형            
            //Physics2D.OverlapPoint //지점 점

            //Physics2D.OverlapCircle => All을 안붙이면 하나만 주긴하는데, 그게 내가 원하는 대상일지아닐지는 알수가 없음.. 
            //그래서 만약 해당 지점에서 나와 가장 가까운 하나를 추리고 싶다면

            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 5, layermask); //원형
            float neardist = Mathf.Infinity;
            int index = -1;

            for (int i = 0; i < cols.Length; i++)
            {
                if (Vector2.Distance( cols[i].transform.position, transform.position) < neardist)
                {
                    neardist = Vector2.Distance(cols[i].transform.position, transform.position);
                    index = i;
                }
            }
            //cols[index]  나랑 가장 가까운 대상/*콜라이더*/이 됨....                        
        }
    }

    //void OnDrawGizmos() //기본적으로 계속 내가 에디터를 플레이 안하고 있어도
    //                    //계속 돌아가는, 기즈모를 그리는 함수
    //{
        //if (~~~한경우에 )
        //{
            //    Gizmos.DrawSphere(transform.position - Vector3.right, 2);
            //    Gizmos.DrawWireSphere(transform.position + Vector3.right, 2);
        //}
    //}
    //void OnDrawGizmosSelected() //에디터를 활성화 해서 해당 객체를 누르면, 
    //    //그때부터 객체 선택 해제할때까지 계속 불리는 함수
    //{        
    //    Gizmos.DrawRay(transform.position, Vector2.up * 100);        
    //}

    void RotateWeapon()
    {
        lookdir = attackPos - (Vector2)transform.position; //내가  attackpos를 바라보는 방향벡터가 나옴.
         angle =Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
        weaponTr.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    public void Hit(float damage, Vector3 dir) 
    {        
        if (mystat.HP <=0)
        {
            return;
        }        

        this.mystat.HP = Mathf.Clamp(this.mystat.HP - damage, 0, this.mystat.MaxHP);                

        anim.SetTrigger("Hit");

        rigid.AddForce(dir, ForceMode2D.Impulse);//매개변수로 받은 (힘을가진)방향으로
                                                 //힘을 줌.
    }
    public float GetAtt()
    {
        return mystat.Att;
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
    void OnCollisionEnter2D(Collision2D collision) //누가 어떻게 때렸건, 어쨌거나부딪혔기때문에
        //이 함수가 불림. 그리고 상대방 태그 분별 똑같음...
    {
        //if (collision.transform.GetComponent<IHit>() !=null)
        //{
        //    collision.transform.GetComponent<IHit>().Hit(mystat.Att);
        //}
        
        if (collision.gameObject.CompareTag("Trap"))
        {
            isHit = true;
            // (나의 위치 - 함정의 위치)  == 함정이 나를 바라보는 방향 
            direction = (transform.position - collision.transform.position).normalized ;
            if (direction.y < 2)
            {
                direction.y = 2;
            }

            direction *= knockBackPower;
            Hit(5, direction);  //나의 hit함수          
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isHit = false;
            jumpCount = 0;//땅에 닿았을때 점프가 다시 가능하도록 점프카운트를 초기화시켜줌...
            rigid.velocity = Vector3.zero; //미끄러지는등의 일이 없도록 땅에 닿으면 속력을 0으로 만듬
        }        
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //부딪혔을당시 내가 몬스터 보다 위에있었음. 즉 위에서 공격함
            if (transform.position.y > collision.transform.position.y + 0.3f)
            {
                //내가 부딪힌 대상 == collision / 
                //내가 부딪힌 대상 객체 그 자체 == collision.gameObject
                //동일하게 부딪힌 대상의 transform도 접근 가능함.

                direction = (collision.transform.position - transform.position).normalized;
                direction.y *= -2; 
                direction *= knockBackPower; //최종 정규화

                ////1번 Enemy니까 적인게 확실해서 Enemy스크립트에 접근하여 Hit을 부른다
                //collision.transform.GetComponent<Enemy>().Hit(mystat.Att);
                //2번 인터페이스를 달았다면 Hit있는게 확실하니까 Hit을 부른다
                collision.transform.GetComponent<IHit>().Hit(mystat.Att,direction );
                //상대방 넉백주기

            }
            else //적이 나를 때림
            {
                isHit = true;
                direction = (transform.position - collision.transform.position).normalized;
                direction.y += 2;

                direction *= knockBackPower;

                ////나의 Hit부름.
                ////1번 방법중에 만약 상대방의 공격력이 public 변수 선언이 되어있다면
                //Hit(collision.transform.GetComponent<Enemy>().stat.Att);
                //2번방법이고
                Hit(collision.transform.GetComponent<IHit>().GetAtt(), direction);                
            }
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

    #region 애니메이션 이벤트 확인용
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
    #endregion
}
