using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region 싱글톤 묶음
    private static GameManager instance = null;
    public static GameManager Instance => instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance !=this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    #endregion
    public Text scoreText; //점수 출력용
    public GameObject foodPrefab; //푸드 원본...

    int score = 0;
    //일정시간마다 foodprefab생성해서
    //그친구를 내려보내기...

    //프리팹 생성 == Instantiate( 원본 프리팹 ); //Gameobject를 반환하기 때문에

    //void aa()
    //{
    //    GameObject obj = Instantiate(foodPrefab);
    //    //obj.transform.position  =
    //  //
    //}

    public void AddScore()
    {
        score++;
        scoreText.text = "점수 : " + score;
    }    
}
