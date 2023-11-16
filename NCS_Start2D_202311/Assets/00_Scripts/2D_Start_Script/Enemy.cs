using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour,IHit
{
    public Slider slider;//피UI
    //Image img; //slider 대신 이미지 사용도 가능
    ////img.fillAmount = stat.HP / stat.MaxHP;  //img사용시 피통의 표현은 이렇게 해야함..
    Rigidbody2D rigid;
    Animator anim;
    bool IsMove = false;
    bool IsLeft = true;

    Transform childTr;
    Vector3 vec = Vector3.right;
    Vector3 scale = Vector3.one; //(1,1,1)
    Coroutine enemyCor = null;        

    public Constructure.Stat stat;

    void Start()
    {               
        rigid = GetComponent<Rigidbody2D>();
        stat = new Constructure.Stat(100,10);
        childTr = transform.GetChild(0);
        anim = childTr.GetComponent<Animator>();

        slider.maxValue = stat.MaxHP;
        slider.value = stat.HP;
        ////애니메이터가 자식에게 있을경우
        //anim = transform.GetChild(0).GetComponent<Animator>();
        vec *= 3 * Time.fixedDeltaTime;//아예 스피드와 시간당을 곱한값 //(0.06,0,0) 
        enemyCor = StartCoroutine(EnemyMove());
    }

    IEnumerator EnemyMove()
    {
        while (true)
        {
            IsMove = true;
            anim.SetBool("IsMove", IsMove);
            yield return new WaitForSeconds( Random.Range (1f,3f));
            IsMove = false; 
            anim.SetBool("IsMove", IsMove);
            IsLeft = Random.Range(0, 2) == 0 ? true : false;
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }
    }
    void FixedUpdate()
    {
        if (IsMove)
        {
            scale.x = (IsLeft ? 1 : -1);
            childTr.localScale = scale;
            transform.Translate(vec * (IsLeft ? -1: 1));

            if (transform.position.x <= GameManager.Instance.posRange[0].position.x)
            {
                IsLeft = false;
            }
            else if (transform.position.x >= GameManager.Instance.posRange[1].position.x)
            {
                IsLeft = true;
            }
        }
    }
    public void Hit(float damage, Vector3 dir)
    {        
        if (stat.HP <= 0)
        {
            return;
        }

        this.stat.HP = Mathf.Clamp(this.stat.HP - damage, 0, this.stat.MaxHP);
        slider.value = this.stat.HP;
        anim.SetTrigger("Hit");
        rigid.AddForce(dir, ForceMode2D.Impulse);
    }
    public float GetAtt()
    {
        return stat.Att;
    }
    //void Start()
    //{
    //    anim = transform.GetChild(0).GetComponent<Animator>();
    //    isMove = false;
    //    StartCoroutine(EnemyAction());
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (isMove==false)
    //    {
    //        transform.Translate(Vector3.right * Time.deltaTime * 2);
    //    }
    //}

    //IEnumerator EnemyAction()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
    //        isMove = !isMove;

    //        anim.SetBool("IsMovePos", isMove);
    //    }        
    //}
}
