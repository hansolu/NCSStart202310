using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region �̱��� ����
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
    public Text scoreText; //���� ��¿�
    public GameObject foodPrefab; //Ǫ�� ����...

    int score = 0;
    //�����ð����� foodprefab�����ؼ�
    //��ģ���� ����������...

    //������ ���� == Instantiate( ���� ������ ); //Gameobject�� ��ȯ�ϱ� ������

    //void aa()
    //{
    //    GameObject obj = Instantiate(foodPrefab);
    //    //obj.transform.position  =
    //  //
    //}

    public void AddScore()
    {
        score++;
        scoreText.text = "���� : " + score;
    }    
}
