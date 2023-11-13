using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//일정시간 후에 일을 시키기
//1번은 invoke
//2번은 coroutine ==> 요걸 추천...

public class MatchingGame : MonoBehaviour
{
    static MatchingGame instance = null; //나만씀...
    public static MatchingGame Instance => instance;

    public InputField inputfield; //레벨 입력용
    public Text alarmText; //경고문구

    public GameObject StartPanel; //레벨을 입력하는 시작패널
    public GameObject GamePanel; //게임 내용을 보여주는 게임패널.

    [SerializeField]
    int Level = 1; //사용자가 레벨을 입력하면, 1레벨에는 4개 그림 맞추기 /2레벨에는 6개 그림 맞추기/ 3레벨에는 8개그림맞추기...

    public Transform parentTr; //버튼을 만들어서 밑에 넣을 Transform 선언
    public GameObject ButtonPrefab; //버튼 프리팹

    #region 2023.11.06 진행한 내용
    public Text score; //맞춘 점수 표기. //맞춘횟수 / 시도 횟수    

    //MatchingButton 스크립트로 이사감
    //public Image[] ButtonImgs; //카드뒤집기의 그림에 해당됨..
    
    public MatchingButton[] ButtonArr; //카드뒤집기 그림 배열이 아니고,
                    //해당 버튼의 스크립트 배열.
    int[] answerArr; //판별용 데이터    

    public Sprite[] sprites; //내가 세팅해줄 그림 배열

    int rightPoint = 0; //맞춘횟수
    int tryPoint = 0; //시도횟수

    int index = -1; //
        
    bool IsPlayable = true;

    Coroutine cor = null; //코루틴을 담을 수 있는 변수.
    #endregion

    void Awake()
    {
        #region 싱글톤 세팅
        if (instance == null) //내가 막 태어나서 아무것도 없음
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)//이미 인스턴스가 생성되어있고
            {//뭔가 잘못된 이유로 내가 다시 생성되려고 한다면
                Destroy(this.gameObject);
            }
        }
        #endregion

        alarmText.transform.parent.gameObject.SetActive(false);
        GamePanel.SetActive(false);
        StartPanel.SetActive(true);
        cor = null;
    }

    public void Button_GameStart() //게임 시작 버튼에 달아둘 기능
    {
        if (int.TryParse( inputfield.text , out int level))
        {
            if (level <= 0)
            {
                alarmText.text = "레벨은 1 이상이어야 합니다";
                alarmText.transform.parent.gameObject.SetActive(true);
            }
            else if (sprites.Length <= level) //레벨1== 2장/레벨2 == 3장...
            {
                alarmText.text = $"최대 가능 레벨은 {sprites.Length - 1} 입니다";
                alarmText.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                Level = level;                
                StartGame();

                GamePanel.SetActive(true);
                StartPanel.SetActive(false);
            }                
        }
        else
        {            
            alarmText.text = "잘못된 레벨 입력입니다";
            alarmText.transform.parent.gameObject.SetActive(true);
        }
    }

    void StartGame()
    {                
        answerArr = new int[2 * (Level+1)]; // 2+ 2*Level        
        ButtonArr = new MatchingButton[2 * (Level + 1)];//배열 크기 선언

        GameObject obj; //만든 버튼을 잠시 담아둘 변수 선언.
        List<int> list = new List<int>(); //0,0,1,1,2,2...
        for (int i = 0; i < Level+1; i++) //레벨 1일때 4개의 정보, 레벨 2일때 6개의 정보... 레벨 4라면 10개의 정보..
        {
            //1번방법
            for (int j = 0; j < 2; j++)
            {
                list.Add(i);
                obj = Instantiate(ButtonPrefab, parentTr);
                ButtonArr[2 * i + j] = obj.GetComponent<MatchingButton>();
            }
            ////2번방법
            //list.Add(i);
            //list.Add(i);
        }
        int index = 0;
        for (int i = 0; i < answerArr.Length; i++)
        {
            index = Random.Range(0, list.Count); //0에서 list의 개수 사이 숫자를 가져옴 == index로 사용가능.
            answerArr[i] = list[index]; //list[index] 에 해당하는 숫자를 answerarr배열에 차례로 넣음
            list.RemoveAt(index); //해당 인덱스위치의 숫자를 삭제함. 겹치지않는 배분 위함.

            ButtonArr[i].SetInfo( i, sprites[answerArr[i]]);

            #region 이전에 Image배열로 들고 있던 때의 코드
            //ButtonImgs[i].sprite = sprites[answerArr[i]]; //버튼 이미지들도 6개로 동일하고, 차례로 그 이미지들을 세팅할건데,
            ////세팅할 이미지들을 sprites에 넣어놨고, 이또한 배열이라서, 0,1,2 로 각 sprite에 접근가능하고,
            ////answerArr[i]에 0,1,2중 숫자가 들어갔기 때문에 가능

            //ButtonImgs[i].gameObject.SetActive(false);
            #endregion
        }

        #region 레벨이 정해져있던 기존 코드
        ////숫자 안겹치게 6개 배분하기
        //List<int> list = new List<int>(6) { 0,0,1,1,2,2};
        //int index = 0;
        //for (int i = 0; i < answerArr.Length; i++)
        //{
        //    index = Random.Range(0, list.Count); //0에서 list의 개수 사이 숫자를 가져옴 == index로 사용가능.
        //    answerArr[i] = list[ index]; //list[index] 에 해당하는 숫자를 answerarr배열에 차례로 넣음
        //    list.RemoveAt(index); //해당 인덱스위치의 숫자를 삭제함. 겹치지않는 배분 위함.
        //    ButtonImgs[i].sprite = sprites[answerArr[i]]; //버튼 이미지들도 6개로 동일하고, 차례로 그 이미지들을 세팅할건데,
        //    //세팅할 이미지들을 sprites에 넣어놨고, 이또한 배열이라서, 0,1,2 로 각 sprite에 접근가능하고,
        //    //answerArr[i]에 0,1,2중 숫자가 들어갔기 때문에 가능

        //    ButtonImgs[i].gameObject.SetActive(false);
        //}
        #endregion
        IsPlayable = true;
    }

    public bool CheckClick(int num)
    {        
        if (IsPlayable == false)
        {            
            return false;
        }

        if (index == -1) //내가 지금 선택한게 첫 선택
        {
            index = num;            
        }
        else //기존에 선택한게 있는 상태.
        {
            if (answerArr[index] == answerArr[num]) //이전에 선택한 것의 숫자내용과
                                                    //새로 선택한 것의 숫자 내용이 같다면
            {
                rightPoint++;
            }
            else
            {
                IsPlayable = false;
                //Invoke("Reverse", 1f);
                if (cor ==null)
                {
                    cor = StartCoroutine(Reverse(index, num));
                }                
            }
            tryPoint++; //시도횟수는 올리기
            index = -1; //인덱스도 초기화 해주기

            score.text = $"맞춘 횟수 : {rightPoint}  /  시도 횟수 : {tryPoint}";                        
        }
        
        return true;
    }

    ////코루틴을 중간에 정지하고 싶을 경우...
    //StopCoroutine(cor); 
    //cor 라는 변수에 담아 씀으로써,
    //StopCoroutine(Reverse(매개변수 조차도 필요했었는데.. 그것도 다 굳이 몰라도 가능하게 됐음));
    //cor = null; //내가 예약했던 해당 코루틴이 정지하고, 새로운 코루틴을 다시 등록할 수 있는 상태가 됨...


    IEnumerator Reverse(int index1, int index2) //코루틴..
    {
        yield return new WaitForSeconds(1f); //무조건 초단위. 
        ButtonArr[index1].SetImageFalse(false); //이전선택친구 뒤집기
        ButtonArr[index2].SetImageFalse(false);//방금 선택친구 뒤집기
        IsPlayable = true;

        cor = null;
    }

    //void Reverse()
    //{
    //    ButtonArr[index1].SetImageFalse(false); //이전선택친구 뒤집기
    //    ButtonArr[index2].SetImageFalse(false);//방금 선택친구 뒤집기
    //    IsPlayable = true;
    //}
}
