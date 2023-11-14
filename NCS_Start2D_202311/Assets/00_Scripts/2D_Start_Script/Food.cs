using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    SpriteRenderer spren;
    [SerializeField]
    float speedMin = 0;
    [SerializeField]
    float speedMax = 0;
    float speed = 0;
    [SerializeField]
    int score = 0;

    public void SetInfo(Sprite _spr, int score)
    {
        if (spren == null)
        {
            spren = this.transform.GetComponent<SpriteRenderer>();
        }        
        spren.sprite = _spr;
        this.score = score;
        speed = Random.Range(speedMin, speedMax);
    }
     
    void FixedUpdate()
    {
        transform.Translate(Vector3.down * Time.fixedDeltaTime * speed);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //������ ȹ���̰�
        {
            GameManager.Instance.AddScore(score);
            //Destroy(this.gameObject);
            GameManager.Instance.ReturnFoodPool(this);
        }
        else if (collision.CompareTag("Ground")) //���� �ε����� �׳� �Ҹ�..
        {
            //Destroy(this.gameObject);
            GameManager.Instance.ReturnFoodPool(this);
        }

        //Destroy�� ���ֹ����� ���� �ƴϰ�
        //���ӸŴ������� �� �ϳ�������, �ٽ� ��Ȱ��ȭ ���Ѵ޶�..�� ��û..
    }
}
