using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public Player player;

    public Transform[] posRange; //Ǫ�尡 ������� ���� Ʈ������..
    public Text scoreText; //���� ��¿�
    public GameObject foodPrefab; //Ǫ�� ����...
    public Sprite[] AllItemSprites;//Ǫ���� �׸��� �Ǿ��� ģ����

    float generateTime=0;
    [SerializeField] //�����̺��̾ �ν�����â�� ���̵����ϴ� attribute
    float generateTime_Min = 0;
    [SerializeField] //�����̺��̾ �ν�����â�� ���̵����ϴ� attribute
    float generateTime_Max = 0;
    int score = 0;

    int picnum = 0;

    //�����ð����� foodprefab�����ؼ�
    //��ģ���� ����������...
    GameObject tmpobj; //�ӽú���        
    Food tmpFood; //���� ��ũ��Ʈ ��� �޾Ƶ� �ӽú���
    Vector3 vec = Vector3.zero;

    //������ƮǮ �� ��������.    
    Queue<Food> objectPool = new Queue<Food>();
    //List,  ����  �ۿ��ִ� ���� ����..
    List<GameObject> allFoodList = new List<GameObject>(); //ť�̹ۿ��ִ� �ֵ��� ���� ��ƾ��Ұ�...
    Coroutine fooddroppin = null;
    void Start()
    {
        scoreText.text = "";
        //���� ������Ʈ Ǯ�� ���� �� �ִ� ��ü���� �����Ѱ���...
        //���� �ϳ��� �¾�� ���� ������ ����������
        //��ȭ�鿡 �ִ� 20���� ������ �ȳ����Ͱ���. ��� �����Ǹ�,
        //foodPrefabģ���� 20�� �����ΰ� ������ƮǮ�� �־��.
        //�ش� ������Ʈ Ǯ�� ������� ��� ����. (setactive(false)) 
        for (int i = 0; i < 20; i++)
        {
            tmpobj = Instantiate(foodPrefab, this.transform.GetChild(0));            
            objectPool.Enqueue(tmpobj.GetComponent<Food>());
            tmpobj.SetActive(false);
            allFoodList.Add(tmpobj);
        }

        fooddroppin = StartCoroutine(GenerateFood());
    }

    public void ControlCoroutine(bool isstart)
    {
        if (isstart)
        {
            ActivePlayer(true); //�÷��̾� Ȱ��ȭ.
            if (fooddroppin == null)
            {
                fooddroppin = StartCoroutine(GenerateFood());
            }            
        }
        else
        {
            StopCoroutine(fooddroppin); //���Ķ����� �ڷ�ƾ�� ���߰�
            fooddroppin = null;

            for (int i = 0; i < allFoodList.Count; i++) //��� ���ĵ� ��Ȱ��ȭ
            {
                allFoodList[i].SetActive(false);
                
            }

            ActivePlayer(false); //�÷��̾� ��Ȱ��ȭ.
        }        
    }

    public void ActivePlayer(bool ison)
    {
        player.gameObject.SetActive(ison);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //SceneManager.LoadScene("Scenes/SecondScene");
            //SceneManager.LoadScene("SecondScene"); //�׳� ���� �ѱ�� �ѱ������                        
            //SceneManager.LoadScene(1);
            SceneLoadManager.Instance.ChangeScene(AllEnum.SceneKind.Second);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //SceneManager.LoadScene("Scenes/2D_Start");
            //SceneManager.LoadScene("2D_Start");
            //SceneManager.LoadScene(0);
            SceneLoadManager.Instance.ChangeScene(AllEnum.SceneKind.Game);
        }
    }

    public Food GetFoodFromPool()
    {
        if (objectPool.Count > 0) //������Ʈ Ǯ�� ���빰�� �ִٸ�~
        {
            return objectPool.Dequeue();
        }
        else
        {
            tmpobj = Instantiate(foodPrefab);
            allFoodList.Add(tmpobj);
            return tmpobj.GetComponent<Food>();
        }
    }

    public void ReturnFoodPool(Food _food)
    {
        objectPool.Enqueue(_food); //���� �����ϱ����ؼ�, ���� ���� ������ �־�δ°Ű�
        _food.gameObject.SetActive(false); //�갡 ��� ���������� ��� ������ ���������̱� ������, ��Ȱ��ȭ �ص�.
    }

    IEnumerator GenerateFood()
    {
        while (true)
        {
            generateTime = Random.Range(generateTime_Min, generateTime_Max);
            yield return new WaitForSeconds(generateTime);

            //�����ð����� ������ ���� �ƴϰ�
            //������Ʈ Ǯ���ִ� ��ģ���� ������ ������ ����
            //setactive�� true�� �Ұ�..

            //������Ʈ Ǯ���� ������ ����...
            tmpFood = GetFoodFromPool();
            vec.y = posRange[0].position.y;
            vec.x = Random.Range(posRange[0].position.x, posRange[1].position.x);
            tmpFood.transform.position = vec; //��ġ ���� �Ϸ�
            picnum = Random.Range(0, AllItemSprites.Length);
            tmpFood.SetInfo(AllItemSprites[picnum], picnum+1);
            tmpFood.gameObject.SetActive(true);
            //������ �������� ��ġ ���� //���� foodprefab�� �����ϰ� �����ϱ� ���� ��ġ ����������.
            //�׷��� �¾ ģ���� �Ʒ��� �̵��ؾ���//������ٵ� �ָ� �ذ�Ǳ���.
            //������ �׷��� ���� �����ϸ� ������ߵ�...
        }        
    }

    public void AddScore(int _score)
    {
        this.score+= _score;
        scoreText.text = "���� : " + this.score;
    }
}
