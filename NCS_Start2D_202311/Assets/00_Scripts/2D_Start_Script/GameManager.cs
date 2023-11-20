using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public Player player;

    public Transform[] posRange; //푸드가 만들어질 범위 트랜스폼..
    public Text scoreText; //점수 출력용
    public GameObject foodPrefab; //푸드 원본...
    public Sprite[] AllItemSprites;//푸드의 그림이 되어줄 친구들

    float generateTime=0;
    [SerializeField] //프라이빗이어도 인스펙터창에 보이도록하는 attribute
    float generateTime_Min = 0;
    [SerializeField] //프라이빗이어도 인스펙터창에 보이도록하는 attribute
    float generateTime_Max = 0;
    int score = 0;

    int picnum = 0;

    //일정시간마다 foodprefab생성해서
    //그친구를 내려보내기...
    GameObject tmpobj; //임시변수        
    Food tmpFood; //음식 스크립트 잠시 받아둘 임시변수
    Vector3 vec = Vector3.zero;

    //오브젝트풀 용 변수선언.    
    Queue<Food> objectPool = new Queue<Food>();
    //List,  만들어서  밖에있는 음식 관리..
    List<GameObject> allFoodList = new List<GameObject>(); //큐ㅜ밖에있는 애들을 여기 담아야할것...
    Coroutine fooddroppin = null;
    void Start()
    {                
        scoreText.text = "";
        //먼저 오브젝트 풀에 내가 쓸 최대 객체들을 만들어둘것임...
        //젤리 하나가 태어나서 땅에 떨어져 죽을떄까지
        //한화면에 최대 20개의 젤리는 안넘을것같다. 라고 생각되면,
        //foodPrefab친구를 20개 만들어두고 오브젝트풀에 넣어둠.
        //해당 오브젝트 풀의 내용들을 모두 꺼둠. (setactive(false)) 
        for (int i = 0; i < 20; i++)
        {
            tmpobj = Instantiate(foodPrefab, this.transform.GetChild(0));
            //tmpobj.transform.position = 위치;
            //tmpobj.transform.rotation = 회전값;

            //위의 세줄의 경우보다 밑의 한줄이 훨씬 가볍고 빠르다.
            //tmpobj = Instantiate(foodPrefab, Vector3.zero, Quaternion.identity, this.transform.GetChild(0));

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
            ActivePlayer(true); //플레이어 활성화.
            if (fooddroppin == null)
            {
                fooddroppin = StartCoroutine(GenerateFood());
            }            
        }
        else
        {
            StopCoroutine(fooddroppin); //음식떨구던 코루틴도 멈추고
            fooddroppin = null;

            for (int i = 0; i < allFoodList.Count; i++) //모든 음식들 비활성화
            {
                allFoodList[i].SetActive(false);
                
            }

            ActivePlayer(false); //플레이어 비활성화.
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
            //SceneManager.LoadScene("SecondScene"); //그냥 씬만 넘기면 넘길수있음                        
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
        if (objectPool.Count > 0) //오브젝트 풀에 내용물이 있다면~
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
        objectPool.Enqueue(_food); //내가 관리하기위해서, 나의 관리 변수에 넣어두는거고
        _food.gameObject.SetActive(false); //얘가 계속 켜져있으면 계속 밑으로 떨어질것이기 때문에, 비활성화 해둠.
    }

    IEnumerator GenerateFood()
    {
        while (true)
        {
            generateTime = Random.Range(generateTime_Min, generateTime_Max);
            yield return new WaitForSeconds(generateTime);

            //일정시간마다 만들어내는 것이 아니고
            //오브젝트 풀에있던 ㅇ친구를 꺼내서 데이터 세팅
            //setactive를 true를 할것..

            //오브젝트 풀에서 꺼내서 쓸것...
            tmpFood = GetFoodFromPool();
            vec.y = posRange[0].position.y;
            vec.x = Random.Range(posRange[0].position.x, posRange[1].position.x);
            tmpFood.transform.position = vec; //위치 설정 완료
            picnum = Random.Range(0, AllItemSprites.Length);
            tmpFood.SetInfo(AllItemSprites[picnum], picnum+1);
            tmpFood.gameObject.SetActive(true);
            //생성된 프리팹의 위치 수정 //내가 foodprefab을 생성하고 있으니까 내가 위치 조절가능함.
            //그렇게 태어난 친구는 아래로 이동해야함//리지드바디를 주면 해결되긴함.
            //음식이 그렇게 땅에 도달하면 사라져야됨...
        }        
    }

    public void AddScore(int _score)
    {
        this.score+= _score;
        scoreText.text = "점수 : " + this.score;
    }
}
