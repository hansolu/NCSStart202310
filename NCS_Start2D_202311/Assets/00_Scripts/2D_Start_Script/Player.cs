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

    #region ���ݿ� �߰�

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
    
        //~selectLayer //������ ���̾� ������ ���..
        layermask = 1 << LayerMask.NameToLayer("Enemy") /*| 1 << LayerMask.NameToLayer("Enemy2")*/;
        ////���� �ش� layermask�� ������ ������ ���̾��
        //~layermask
    }

    void Update() 
    {
        if (isHit)
        {
            return;
        }
        //Input.GetAxis //���� ��ǲ�� �����ϰ� �޾ƿ� //-1~1 �޾ƿ� //�Ǽ��� ����. 
        //�˾Ƶ�����.. 0.1.... 
        //Input.GetAxisRaw //���� ��ǲ�� -1,0,1 �̷��� �޾ƿ�//�Ϲ����� Ű���� ��ǲ

        //Horizontal == ���ΰ�
        //Vertical == ���ΰ�

        x = Input.GetAxisRaw("Horizontal"); //���ΰ�//������ ������ -1, �ƹ��͵� �ȴ����� 0, ����Ű ������ 1
        //y = Input.GetAxisRaw("Vertical"); //���ΰ�        

        vec.x = x;
        //vec.y = y;

        //transform.position += vec * Time.deltaTime * speed; //Time.deltatime == �������Ӵ� �ɸ��� �ð�... 
        //������ ���� �����ϴ� �۾�.

        transform.Translate(vec.normalized * Time.deltaTime * speed); //position += �������ϱ� ��ɰ� ������.
        //����.normalized == ����ȭ�� ����. ũ�Ⱑ 1¥���� �⺻ ���ͷ� ��.

        //���� ��ǻ�Ͷ� 1�ʿ� 60�������� ���ٰ� Ĩ�ô�.  =>   1/60 => 0.016
        //������ ��ǻ�Ͱ� �־�� 1�ʿ� 30�������� ���ٰ� Ĩ�ô�. => 1/30 => 0.032

        //Debug.Log($"x = {x}, y = {y}"); //-1,0,1

        //1�� ������ �ݴ�� �ϴ¹� //������ �ݴ�� �ϴ� �� ����Ʈ...
        if (vec.x != 0)//���� ���� �����̰� �ִ� ����
        {
            scaleVec.x = vec.x; //-1 �̶� 1
            anim.SetBool("IsMove", true);
        }
        else //vex.x == x == 0 �ƹ� �Է��� ���� ����.
        {
            anim.SetBool("IsMove", false);
        }
        //anim.SetFloat("�ش� float�� �����̸�", �Ǽ�);
        //anim.SetInteger("�ش� int�� �����̸�", ����);
        transform.localScale = scaleVec; //1,1,1 �������� ���� ��� ������ // -1,1,1 ������ ������ ���������


        ////2���� ��������Ʈ�������� ������ ��. //�̹���� ����, �ݶ��̴��� �ȵ������� ���ڸ��� ������ �ֱ⶧��.
        //if (vec.x < 0)
        //{
        //    sprend.flipX = true;
        //}
        //else if(vec.x > 0)
        //{
        //    sprend.flipX = false;
        //}

        ////�ִϸ��̼� ���ǵ� ������ ����
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    anim.speed += 1; 
        //}
        //else if (Input.GetKeyDown(KeyCode.DownArrow))
        //{            
        //    if (anim.speed > 0.1f)
        //    {
        //        anim.speed -= 0.1f; //speed == 0 �ƿ� �ȿ�����
        //    }
        //}

        //���� ����� �ƴ����� ��͵� ����...
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("��ų1"))
        //{
        //    if (Input.GetKeyDown(KeyCode.��ų2��Ű))
        //    {
        //        ������ �޺�..? Ư�����...
        //    }
        //}

        ////���� ���� Hit �ִϸ��̼� �÷��� ���̰�, �ش� �ִϸ��̼���
        ////80�� �̻� �÷��� �Ǿ��ٸ�...
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")
        //    && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f
        //    )
        //{            
        //        Debug.Log("�� �ִϸ��̼��� �� ���������� �ؾ��ϴ� ��");         
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

        if (Input.GetKeyDown(KeyCode.Z))// �Ͽ��� zŰ�� ������ �� �۾�...
        {            
            TilemapManager.Instance.SetTile(transform.position + 
                Vector3.right * scaleVec.x,  //������ ���� (1,0,0) * -1 == (-1,0,0)
                
                AllEnum.TileKind.Coin_Bronze);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            TilemapManager.Instance.SetTile(transform.position + Vector3.right * scaleVec.x, AllEnum.TileKind.End);
            //TilemapManager.Instance.SwapTile(
            //    TilemapManager.Instance.GetTile( transform.position + Vector3.right * scaleVec.x), null);
        }


        attackPos = camera.ScreenToWorldPoint(Input.mousePosition); //���� ���콺 Ŭ���� ��ġ�� ���� ���������� �ٲ�
        RotateWeapon();
        if (Input.GetMouseButtonDown(0)) //���콺����Ŭ��
        {
            //camera.ScreenPointToRay(/*���*/); //ī�޶��� ��ũ���� � ���� ���� (����)���� ��ڴ�. 
            ////�츮�� 2dī�޶� == orthographic�̰� �̰Ŵ� ���� �̶� z���� ������� ����
            //camera.ScreenToWorldPoint(/*���*/); //����� ��������Ʈ�� �����ϰ���. ���� ������...  ȭ����� x,y������ �ٲٰڴ�.

            //��Ÿ���� ��á���� ���ư���
            //�����ϱ�
            
            //distance�� ���ص��ǰ�����.. 
            if (Vector2.Distance(attackPos, transform.position) <= 2) //�̰� �ձ� ���
            {
                Debug.Log("����");
            }

            ////�̰Ŵ� �簢���
            //if (attackPos.x <= transform.position.x + 1 
            //    && attackPos.x >= transform.position.x - 1
            //    && attackPos.y <= transform.position.y + 1
            //    && attackPos.y >= transform.position.y - 1)
            //{                
            //}

            //����ĳ��Ʈ

            //hits =Physics2D.Raycast(/*������ ������*//*tranform.position ���� ��ġ. �̰ɾ��� ��ü������ ���� �߹ؿ��� ���̰� �������̱� ������*/ 
            //    weaponTr.position, /*���⺤��*/ attackPos /*, �Ÿ� �ֱ� ����. �Ÿ��� �������ϸ� float.MaxValue  Ȥ�� Mathf.Infinity ���Ѱ� ����, ���̾��ũ */ );

            //RaycastHit2D hits = Physics2D.Raycast(weaponTr.position, attackPos, 2, layermask);
            ////����ü�� ���ʿ� ������� �޴� �����
            //if (hits.collider !=null) //�����ϰ�����
            //{
            //    //hits.transform.GetComponent<IHit>().Hit();
            //    //Instantiate(����Ʈ ������, hits.point, Quaternion.identity);//�ǰ�����Ʈ ����
            //    //if (hits.rigidbody != null)
            //    //    hits.rigidbody.AddForce(attackPos * /*��*/10);
            //}         

            //Physics2D.BoxCast 
            //Physics2D.CapsuleCast(weaponTr.position, 5/*������*/,ĸ���� ���η��Ұ��� ���η��Ұ���.. );
            //Physics2D.CircleCast
            //Physics2D.Linecast

            //==================== ~Cast ������ ���⺤�ͷ� ��Ƴ�. �׸��� RaycastHit2D ����ü�� ��ȯ��. ���� ��°� ��ȯ�ϰ�, All~�Լ��� ��� ������ �ٵ�
            //==============   overlap~ ������ �ش� ������ �� �ϰ� ����. �׸��� Collider2D�� ��ȯ��.

            //Physics2D.OverlapArea //���簢��
            //Physics2D.OverlapBox //���簢��
            //Physics2D.OverlapCapsule //ĸ����            
            //Physics2D.OverlapPoint //���� ��

            //Physics2D.OverlapCircle => All�� �Ⱥ��̸� �ϳ��� �ֱ��ϴµ�, �װ� ���� ���ϴ� ��������ƴ����� �˼��� ����.. 
            //�׷��� ���� �ش� �������� ���� ���� ����� �ϳ��� �߸��� �ʹٸ�

            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 5, layermask); //����
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
            //cols[index]  ���� ���� ����� ���/*�ݶ��̴�*/�� ��....                        
        }
    }

    //void OnDrawGizmos() //�⺻������ ��� ���� �����͸� �÷��� ���ϰ� �־
    //                    //��� ���ư���, ����� �׸��� �Լ�
    //{
        //if (~~~�Ѱ�쿡 )
        //{
            //    Gizmos.DrawSphere(transform.position - Vector3.right, 2);
            //    Gizmos.DrawWireSphere(transform.position + Vector3.right, 2);
        //}
    //}
    //void OnDrawGizmosSelected() //�����͸� Ȱ��ȭ �ؼ� �ش� ��ü�� ������, 
    //    //�׶����� ��ü ���� �����Ҷ����� ��� �Ҹ��� �Լ�
    //{        
    //    Gizmos.DrawRay(transform.position, Vector2.up * 100);        
    //}

    void RotateWeapon()
    {
        lookdir = attackPos - (Vector2)transform.position; //����  attackpos�� �ٶ󺸴� ���⺤�Ͱ� ����.
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

        rigid.AddForce(dir, ForceMode2D.Impulse);//�Ű������� ���� (��������)��������
                                                 //���� ��.
    }
    public float GetAtt()
    {
        return mystat.Att;
    }
    //�ڷ�ƾ ���

    //IEnumerator �ڷ�ƾ�̸�()
    //{
    //    //�ϴ� ����
    //    yield return new WaitForSeconds(n����);
    //    //n���� ����
    //}

    //2������ ����


    //�Ⱦ��� �������� �� ��..

    ////��� �����̶� Ʈ���� üũ�Ǿ������� ������ Ʈ���� �Լ��� �Ҹ�.
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //Debug.Log("Ʈ������ ���� ����");
    // // �Ʒ� ������ üũ��, Food��ũ��Ʈ�� �Ű�����.
    //    if (collision.CompareTag("Item"))  //���� �ε��� ����� �±װ� Item�̶��...
    //    {
    //        GameManager.Instance.AddScore();
    //        Destroy(collision.gameObject);//���� �ε��� ģ�� ��ü�� ���� 
    //    }
    //}
    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("Ʈ������ ���� ����");
    //}
    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("Ʈ������ ���� ��");
    //}
    //�浹�Լ��� �Ⱥҷ���=>
    //1 �Ѵ� rigidbody�� ����
    //2 ��������� Triggerüũ�� �Ǿ�����

    ////�ݸ��� ������ �Ѵ� �ݸ����̾��, �Ȱ�ġ�� �ݸ��� �Լ��� �Ҹ�
    void OnCollisionEnter2D(Collision2D collision) //���� ��� ���Ȱ�, ��·�ų��ε����⶧����
        //�� �Լ��� �Ҹ�. �׸��� ���� �±� �к� �Ȱ���...
    {
        //if (collision.transform.GetComponent<IHit>() !=null)
        //{
        //    collision.transform.GetComponent<IHit>().Hit(mystat.Att);
        //}
        
        if (collision.gameObject.CompareTag("Trap"))
        {
            isHit = true;
            // (���� ��ġ - ������ ��ġ)  == ������ ���� �ٶ󺸴� ���� 
            direction = (transform.position - collision.transform.position).normalized ;
            if (direction.y < 2)
            {
                direction.y = 2;
            }

            direction *= knockBackPower;
            Hit(5, direction);  //���� hit�Լ�          
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isHit = false;
            jumpCount = 0;//���� ������� ������ �ٽ� �����ϵ��� ����ī��Ʈ�� �ʱ�ȭ������...
            rigid.velocity = Vector3.zero; //�̲������µ��� ���� ������ ���� ������ �ӷ��� 0���� ����
        }        
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //�ε�������� ���� ���� ���� �����־���. �� ������ ������
            if (transform.position.y > collision.transform.position.y + 0.3f)
            {
                //���� �ε��� ��� == collision / 
                //���� �ε��� ��� ��ü �� ��ü == collision.gameObject
                //�����ϰ� �ε��� ����� transform�� ���� ������.

                direction = (collision.transform.position - transform.position).normalized;
                direction.y *= -2; 
                direction *= knockBackPower; //���� ����ȭ

                ////1�� Enemy�ϱ� ���ΰ� Ȯ���ؼ� Enemy��ũ��Ʈ�� �����Ͽ� Hit�� �θ���
                //collision.transform.GetComponent<Enemy>().Hit(mystat.Att);
                //2�� �������̽��� �޾Ҵٸ� Hit�ִ°� Ȯ���ϴϱ� Hit�� �θ���
                collision.transform.GetComponent<IHit>().Hit(mystat.Att,direction );
                //���� �˹��ֱ�

            }
            else //���� ���� ����
            {
                isHit = true;
                direction = (transform.position - collision.transform.position).normalized;
                direction.y += 2;

                direction *= knockBackPower;

                ////���� Hit�θ�.
                ////1�� ����߿� ���� ������ ���ݷ��� public ���� ������ �Ǿ��ִٸ�
                //Hit(collision.transform.GetComponent<Enemy>().stat.Att);
                //2������̰�
                Hit(collision.transform.GetComponent<IHit>().GetAtt(), direction);                
            }
        }
    }

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.Log("�׳� �ݸ��� ���� ����");
    //}
    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log("�׳� �ݸ��� ���� ��");
    //}

    #region �ִϸ��̼� �̺�Ʈ Ȯ�ο�
    public void Test() //hit ���߿� �Ҹ� �׽�Ʈ�Լ�
    {
        Debug.Log("�ƾ�");
    }

    public int Test1()
    {
        Debug.Log("�ƾ�1");
        return 0;
    }
    public void Test2(int val)
    {
        Debug.Log("�ƾ�2");        
    }
    public int Test3(int val)
    {
        Debug.Log("�ƾ�3");
        return 0;
    }
    public int Test4(int val, string str) //�Ű������� 2���̻��̶� �ִϸ��̼��̺�Ʈ �߰� x
    {
        Debug.Log("�ƾ�4");
        return 0;
    }

    public void Test5(int val, string str)//�Ű������� 2���̻��̶� �ִϸ��̼��̺�Ʈ �߰� x
    {
        Debug.Log("�ƾ�5");        
    }

    public string Test6() 
    {
        Debug.Log("test6");
        return "";
    }

    //�� �ܿ� string,int, float.... �̷� Ÿ���� ��ȯ, Ȥ�� ��ȯ���� + �Ű����� 1������ ��
    //�ִϸ��̼� �̺�Ʈ�� �߰� ����.
    #endregion
}
