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
        //Input.GetAxis //���� ��ǲ�� �����ϰ� �޾ƿ� //-1~1 �޾ƿ� //�Ǽ��� ����. 
        //�˾Ƶ�����.. 0.1.... 
        //Input.GetAxisRaw //���� ��ǲ�� -1,0,1 �̷��� �޾ƿ�//�Ϲ����� Ű���� ��ǲ

        //Horizontal == ���ΰ�
        //Vertical == ���ΰ�

        x = Input.GetAxisRaw("Horizontal"); //���ΰ�
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
            scaleVec.x = vec.x;
            anim.SetBool("IsMove", true);
        }
        else //vex.x == x == 0 �ƹ� �Է��� ���� ����.
        {
            anim.SetBool("IsMove", false);
        }
        //anim.SetFloat("�ش� float�� �����̸�", �Ǽ�);
        //anim.SetInteger("�ش� int�� �����̸�", ����);
        transform.localScale = scaleVec; //1,1,1 // -1,1,1


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
    void OnCollisionEnter2D(Collision2D collision) 
    {
        jumpCount = 0;//���� ������� ������ �ٽ� �����ϵ��� ����ī��Ʈ�� �ʱ�ȭ������...
        if (collision.gameObject.CompareTag("Trap"))
        {
            anim.SetTrigger("Hit");
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
}
