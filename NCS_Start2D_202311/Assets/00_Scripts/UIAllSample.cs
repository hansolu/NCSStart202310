using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAllSample : MonoBehaviour
{
    #region ������ ������ �����
    public Button[] button; //���� ���ϴ� ��ư���� ���� ���ѵ�.   
    //public Toggle toggle;
    public Toggle[] toggles; //������ư ��� �����ϴ����
    //��3���� �������..

    public InputField inputfield;
    public Slider slider;
    int hp = 100;

    Coroutine cor = null;

    public GameObject buttonPrefab; //���� ������
    public Transform Tr;
    #endregion
    public Dropdown dropdown;
    public Transform contents; //��ũ�ѹ�ư�� �����ؼ� �ؿ� �޾��� �θ� Transform
    public GameObject scrollContentsPrefab; //��ũ�� ������ �ؿ� ���� ��ư
    public Dictionary<AllEnum.ItemType, List<Button>> allScrollContents = new Dictionary<AllEnum.ItemType, List<Button>>();

    //������... �������� ���ʹ����� �ش� �����۸� ���̰�.. �������� ������ Ŭ���� �ϸ� �ش� �� ��������.. 



    public Sprite[] sprites; //�ش� ��ũ�ѿ� ���� ��ư�� �̹����� ��ü�� ���ҽ�

    //public Scrollbar scrollbar;//�̰Ŵ� ��ũ�ѹ�.. ��ũ�� ���� �ٲ���������...

    //Dictionary<AllEnum.ItemType, Item> itemDic = new Dictionary<AllEnum.ItemType, Item>();    

    

    void Start()
    {
        #region ������ ������ �����
        //GameObject obj = Instantiate(buttonPrefab, Tr); //        
        //obj.GetComponent<Button>().onClick.AddListener(ButtonClick);

        //button[0].onClick.AddListener(ButtonClick);//        

        ////string inputval = inputfield.text;
        //slider.maxValue = hp;
        #endregion
                
        dropdown.ClearOptions(); //����ٿ �پ��ִ� �ɼǵ��� �� ������.
        
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>(); //����ٿ �߰��� �ɼ� ���� ����
        GameObject tmp;

        options.Add(new Dropdown.OptionData("��ü����", null)); //���� ù ��ҷ� ��ü���� �߰�

        for (int i = 0; i < (int)AllEnum.ItemType.End; i++)
        {
            options.Add(new Dropdown.OptionData("����"+(i+1),sprites[i])); //����ٿ� �ɼǿ�

            allScrollContents.Add((AllEnum.ItemType)i, new List<Button>()); //��ũ�ѵ�ųʸ�...�� 
            for (int j = 0; j< 2; j++)
            {
               tmp= Instantiate(scrollContentsPrefab, contents); //��ũ�� �ȿ� �� ������ ��ư�� �����ϰ�,
                                                                 //�ش� ��ư�� contents�� transform�ؿ� �ڽ����� �о����
                allScrollContents[(AllEnum.ItemType)i].Add( tmp.GetComponent<Button>());
                tmp.GetComponent<Image>().sprite = sprites[i];
            }
        }
        
        dropdown.AddOptions(options); //����ٿ ���� ������ �ɼǵ��� ����.

        dropdown.onValueChanged.AddListener(
            delegate { SelectDropdown(/*dropdown.value*/dropdown); }); //����ٿ��� Ŭ�������� ��ɵ� �߰�.


        dropdown.captionText.text = "��������";//
        //contents.GetComponent<RectTransform>().localPosition

        contents.GetComponent<RectTransform>().sizeDelta = new Vector2(100/*��ưũ��*/ * 6/*��ư ����*/+ 10/*���� ũ��*/*5 /*��ư����-1*/,
            
            contents.GetComponent<RectTransform>().sizeDelta.y); //�������� ���� ���� ũ�⸦ ������.

        //scrollbar.value = 1;
        SelectDropdown(dropdown); //�������ڸ��� ��������ֱ�
    }

    #region ������ ������ �����
    //void Update()
    //{        
    //if (Input.GetKeyDown(KeyCode.DownArrow)) //�Ʒ�����Ű
    //{
    //    hp -= 10;
    //}
    //else if (Input.GetKeyDown(KeyCode.UpArrow)) //�� ����Ű
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
        if (down.value == 0) //��ü���� ��ư�̶��
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
        Debug.Log("����ڰ� �Է��� ���� : "+inputfield.text);
    }

    public void ToggleClick()
    {
        //Debug.Log(toggle.isOn? "����� üũ�� ���� " : "����� üũ ������ ����");
        for (int i = 0; i < toggles.Length; i++)
        {
            Debug.Log(toggles[i].isOn?  $"{i}��° ����� �����ֽ��ϴ�" : $"{i}��° ��� ��������" );
        }
    }

    public void ButtonClick() //��ư�� ��������� ���.
    {
        Debug.Log("��ư�� Ŭ���ư� �Ű����� ���� �Լ�" );
    }
    public void ButtonClick(string a) //��ư�� ��������� ���.
    {
        Debug.Log("��ư�� Ŭ���ư� �Ű������� " + a + "�� ������");

        //�ڷ�ƾ ����
        if (cor == null)
        {
            loop = true;
            cor = StartCoroutine(CoroutineName(10));
        }

        //�ڷ�ƾ ���߱�
        loop = false;
        StopCoroutine( cor);
        cor = null;
    }

    bool loop = true;

    IEnumerator CoroutineName(int val /*�ʿ��ϴٸ� �Ű����� ����*/)
    {
        int i = 0;
        while (loop)
        {
            Debug.Log("�ڷ�ƾ �Ҹ�");
            yield return new WaitForSeconds(1f); //1�� �Ŀ� �� �Ҽ�����..
            Debug.Log("�ڷ�ƾ 1���� ");            
        }        

        cor = null;
    }
}
