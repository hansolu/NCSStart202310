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
        if (collision.CompareTag("Player")) //점수의 획득이고
        {
            GameManager.Instance.AddScore(score);
            //Destroy(this.gameObject);
            GameManager.Instance.ReturnFoodPool(this);
        }
        else if (collision.CompareTag("Ground")) //땅과 부딪히면 그냥 소멸..
        {
            //Destroy(this.gameObject);
            GameManager.Instance.ReturnFoodPool(this);
        }

        //Destroy로 없애버리는 것이 아니고
        //게임매니저에게 나 일끝났으니, 다시 비활성화 시켜달라..고 요청..
    }
}
