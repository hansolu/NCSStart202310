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
    void Start()
    {
        rigid = transform.GetComponent<Rigidbody2D>();        
        sprend = transform.GetComponent<SpriteRenderer>();
    }

    void Update() 
    {
        //Input.GetAxis //���� ��ǲ�� �����ϰ� �޾ƿ� //-1~1 �޾ƿ� //�Ǽ��� ����. 
        //�˾Ƶ�����.. 0.1.... 
        //Input.GetAxisRaw //���� ��ǲ�� -1,0,1 �̷��� �޾ƿ�//�Ϲ����� Ű���� ��ǲ

        //Horizontal == ���ΰ�
        //Vertical == ���ΰ�

        x = Input.GetAxisRaw("Horizontal"); //���ΰ�
        y = Input.GetAxisRaw("Vertical"); //���ΰ�        

        vec.x = x;
        vec.y = y;

        //transform.position += vec * Time.deltaTime * speed; //Time.deltatime == �������Ӵ� �ɸ��� �ð�... 
        //������ ���� �����ϴ� �۾�.

        transform.Translate(vec.normalized * Time.deltaTime * speed); //position += �������ϱ� ��ɰ� ������.
        //����.normalized == ����ȭ�� ����. ũ�Ⱑ 1¥���� �⺻ ���ͷ� ��.

        //���� ��ǻ�Ͷ� 1�ʿ� 60�������� ���ٰ� Ĩ�ô�.  =>   1/60 => 0.016
        //������ ��ǻ�Ͱ� �־�� 1�ʿ� 30�������� ���ٰ� Ĩ�ô�. => 1/30 => 0.032

        //Debug.Log($"x = {x}, y = {y}"); //-1,0,1

        //1�� ������ �ݴ�� �ϴ¹� //������ �ݴ�� �ϴ� �� ����Ʈ...
        if (vec.x != 0)
        {
            scaleVec.x = vec.x;
        }
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

    //��� �����̶� Ʈ���� üũ�Ǿ������� ������ Ʈ���� �Լ��� �Ҹ�.
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Ʈ������ ���� ����");
        if (collision.CompareTag("Item"))  //���� �ε��� ����� �±װ� Item�̶��...
        {
            GameManager.Instance.AddScore();
            Destroy(collision.gameObject);//���� �ε��� ģ�� ��ü�� ���� 
        }
    }
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
    }
    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.Log("�׳� �ݸ��� ���� ����");
    //}
    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log("�׳� �ݸ��� ���� ��");
    //}
}
