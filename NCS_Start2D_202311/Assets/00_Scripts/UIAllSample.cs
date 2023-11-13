using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAllSample : MonoBehaviour
{
    #region 이전에 진행한 내용들
    public Button[] button; //내가 원하는 버튼들을 연결 시켜둠.   
    //public Toggle toggle;
    public Toggle[] toggles; //라디오버튼 기능 구현하느라고
    //한3개쯤 들고있음..

    public InputField inputfield;
    public Slider slider;
    int hp = 100;

    Coroutine cor = null;

    public GameObject buttonPrefab; //원본 프리팹
    public Transform Tr;
    #endregion
    public Dropdown dropdown;
    public Transform contents; //스크롤버튼을 생성해서 밑에 달아줄 부모 Transform
    public GameObject scrollContentsPrefab; //스크롤 컨텐츠 밑에 붙일 버튼
    public Dictionary<AllEnum.ItemType, List<Button>> allScrollContents = new Dictionary<AllEnum.ItemType, List<Button>>();

    //아이템... 상점에서 필터누르면 해당 아이템만 보이고.. 상점에서 아이템 클릭을 하면 해당 뭐 구매진행.. 



    public Sprite[] sprites; //해당 스크롤에 만든 버튼의 이미지를 교체할 리소스

    //public Scrollbar scrollbar;//이거는 스크롤바.. 스크롤 방향 바꿨을떄썼음...

    //Dictionary<AllEnum.ItemType, Item> itemDic = new Dictionary<AllEnum.ItemType, Item>();    

    

    void Start()
    {
        #region 이전에 진행한 내용들
        //GameObject obj = Instantiate(buttonPrefab, Tr); //        
        //obj.GetComponent<Button>().onClick.AddListener(ButtonClick);

        //button[0].onClick.AddListener(ButtonClick);//        

        ////string inputval = inputfield.text;
        //slider.maxValue = hp;
        #endregion
                
        dropdown.ClearOptions(); //드랍다운에 붙어있던 옵션들을 싹 지워줌.
        
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>(); //드랍다운에 추가할 옵션 변수 선언
        GameObject tmp;

        options.Add(new Dropdown.OptionData("전체선택", null)); //가장 첫 요소로 전체선택 추가

        for (int i = 0; i < (int)AllEnum.ItemType.End; i++)
        {
            options.Add(new Dropdown.OptionData("젤리"+(i+1),sprites[i])); //드랍다운 옵션용

            allScrollContents.Add((AllEnum.ItemType)i, new List<Button>()); //스크롤딕셔너리...에 
            for (int j = 0; j< 2; j++)
            {
               tmp= Instantiate(scrollContentsPrefab, contents); //스크롤 안에 들어갈 컨텐츠 버튼을 생성하고,
                                                                 //해당 버튼을 contents의 transform밑에 자식으로 밀어넣음
                allScrollContents[(AllEnum.ItemType)i].Add( tmp.GetComponent<Button>());
                tmp.GetComponent<Image>().sprite = sprites[i];
            }
        }
        
        dropdown.AddOptions(options); //드랍다운에 내가 설정한 옵션들을 더함.

        dropdown.onValueChanged.AddListener(
            delegate { SelectDropdown(/*dropdown.value*/dropdown); }); //드랍다운을 클릭했을때 기능도 추가.


        dropdown.captionText.text = "젤리선택";//
        //contents.GetComponent<RectTransform>().localPosition

        contents.GetComponent<RectTransform>().sizeDelta = new Vector2(100/*버튼크기*/ * 6/*버튼 개수*/+ 10/*간격 크기*/*5 /*버튼개수-1*/,
            
            contents.GetComponent<RectTransform>().sizeDelta.y); //컨텐츠의 가로 세로 크기를 정해줌.

        //scrollbar.value = 1;
        SelectDropdown(dropdown); //시작하자마자 실행시켜주기
    }

    #region 이전에 진행한 내용들
    //void Update()
    //{        
    //if (Input.GetKeyDown(KeyCode.DownArrow)) //아래방향키
    //{
    //    hp -= 10;
    //}
    //else if (Input.GetKeyDown(KeyCode.UpArrow)) //위 방향키
    //{
    //    hp += 10;
    //}
    //slider.value = hp;        
    //}
    #endregion

    public void ScrollVec()
    {
        Debug.Log("aaaa");
    }

    public void SelectDropdown(/*int val*/ Dropdown down)
    {        
        if (down.value == 0) //전체선택 버튼이라면
        {
            foreach (var item in allScrollContents)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    item.Value[i].gameObject.SetActive(true);
                }
            }
            return;
        }

        int val = down.value - 1;
        foreach (var item in allScrollContents)
        {
            for (int i = 0; i < item.Value.Count; i++)
            {
                item.Value[i].gameObject.SetActive(item.Key == (AllEnum.ItemType)val ? true : false);
            }
        }
    }

    public void GetInputField()
    {
        Debug.Log("사용자가 입력한 내용 : "+inputfield.text);
    }

    public void ToggleClick()
    {
        //Debug.Log(toggle.isOn? "토글이 체크된 상태 " : "토글이 체크 해제된 상태");
        for (int i = 0; i < toggles.Length; i++)
        {
            Debug.Log(toggles[i].isOn?  $"{i}번째 토글이 눌려있습니다" : $"{i}번째 토글 해제상태" );
        }
    }

    public void ButtonClick() //버튼에 연결시켜줄 기능.
    {
        Debug.Log("버튼이 클릭됐고 매개변수 없는 함수" );
    }
    public void ButtonClick(string a) //버튼에 연결시켜줄 기능.
    {
        Debug.Log("버튼이 클릭됐고 매개변수로 " + a + "가 들어왔음");

        //코루틴 실행
        if (cor == null)
        {
            loop = true;
            cor = StartCoroutine(CoroutineName(10));
        }

        //코루틴 멈추기
        loop = false;
        StopCoroutine( cor);
        cor = null;
    }

    bool loop = true;

    IEnumerator CoroutineName(int val /*필요하다면 매개변수 가능*/)
    {
        int i = 0;
        while (loop)
        {
            Debug.Log("코루틴 불림");
            yield return new WaitForSeconds(1f); //1초 후에 뭘 할수잇음..
            Debug.Log("코루틴 1초후 ");            
        }        

        cor = null;
    }
}
