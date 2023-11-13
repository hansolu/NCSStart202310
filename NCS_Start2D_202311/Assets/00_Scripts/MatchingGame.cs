using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�����ð� �Ŀ� ���� ��Ű��
//1���� invoke
//2���� coroutine ==> ��� ��õ...

public class MatchingGame : MonoBehaviour
{
    static MatchingGame instance = null; //������...
    public static MatchingGame Instance => instance;

    public InputField inputfield; //���� �Է¿�
    public Text alarmText; //�����

    public GameObject StartPanel; //������ �Է��ϴ� �����г�
    public GameObject GamePanel; //���� ������ �����ִ� �����г�.

    [SerializeField]
    int Level = 1; //����ڰ� ������ �Է��ϸ�, 1�������� 4�� �׸� ���߱� /2�������� 6�� �׸� ���߱�/ 3�������� 8���׸����߱�...

    public Transform parentTr; //��ư�� ���� �ؿ� ���� Transform ����
    public GameObject ButtonPrefab; //��ư ������

    #region 2023.11.06 ������ ����
    public Text score; //���� ���� ǥ��. //����Ƚ�� / �õ� Ƚ��    

    //MatchingButton ��ũ��Ʈ�� �̻簨
    //public Image[] ButtonImgs; //ī��������� �׸��� �ش��..
    
    public MatchingButton[] ButtonArr; //ī������� �׸� �迭�� �ƴϰ�,
                    //�ش� ��ư�� ��ũ��Ʈ �迭.
    int[] answerArr; //�Ǻ��� ������    

    public Sprite[] sprites; //���� �������� �׸� �迭

    int rightPoint = 0; //����Ƚ��
    int tryPoint = 0; //�õ�Ƚ��

    int index = -1; //
        
    bool IsPlayable = true;

    Coroutine cor = null; //�ڷ�ƾ�� ���� �� �ִ� ����.
    #endregion

    void Awake()
    {
        #region �̱��� ����
        if (instance == null) //���� �� �¾�� �ƹ��͵� ����
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)//�̹� �ν��Ͻ��� �����Ǿ��ְ�
            {//���� �߸��� ������ ���� �ٽ� �����Ƿ��� �Ѵٸ�
                Destroy(this.gameObject);
            }
        }
        #endregion

        alarmText.transform.parent.gameObject.SetActive(false);
        GamePanel.SetActive(false);
        StartPanel.SetActive(true);
        cor = null;
    }

    public void Button_GameStart() //���� ���� ��ư�� �޾Ƶ� ���
    {
        if (int.TryParse( inputfield.text , out int level))
        {
            if (level <= 0)
            {
                alarmText.text = "������ 1 �̻��̾�� �մϴ�";
                alarmText.transform.parent.gameObject.SetActive(true);
            }
            else if (sprites.Length <= level) //����1== 2��/����2 == 3��...
            {
                alarmText.text = $"�ִ� ���� ������ {sprites.Length - 1} �Դϴ�";
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
            alarmText.text = "�߸��� ���� �Է��Դϴ�";
            alarmText.transform.parent.gameObject.SetActive(true);
        }
    }

    void StartGame()
    {                
        answerArr = new int[2 * (Level+1)]; // 2+ 2*Level        
        ButtonArr = new MatchingButton[2 * (Level + 1)];//�迭 ũ�� ����

        GameObject obj; //���� ��ư�� ��� ��Ƶ� ���� ����.
        List<int> list = new List<int>(); //0,0,1,1,2,2...
        for (int i = 0; i < Level+1; i++) //���� 1�϶� 4���� ����, ���� 2�϶� 6���� ����... ���� 4��� 10���� ����..
        {
            //1�����
            for (int j = 0; j < 2; j++)
            {
                list.Add(i);
                obj = Instantiate(ButtonPrefab, parentTr);
                ButtonArr[2 * i + j] = obj.GetComponent<MatchingButton>();
            }
            ////2�����
            //list.Add(i);
            //list.Add(i);
        }
        int index = 0;
        for (int i = 0; i < answerArr.Length; i++)
        {
            index = Random.Range(0, list.Count); //0���� list�� ���� ���� ���ڸ� ������ == index�� ��밡��.
            answerArr[i] = list[index]; //list[index] �� �ش��ϴ� ���ڸ� answerarr�迭�� ���ʷ� ����
            list.RemoveAt(index); //�ش� �ε�����ġ�� ���ڸ� ������. ��ġ���ʴ� ��� ����.

            ButtonArr[i].SetInfo( i, sprites[answerArr[i]]);

            #region ������ Image�迭�� ��� �ִ� ���� �ڵ�
            //ButtonImgs[i].sprite = sprites[answerArr[i]]; //��ư �̹����鵵 6���� �����ϰ�, ���ʷ� �� �̹������� �����Ұǵ�,
            ////������ �̹������� sprites�� �־����, �̶��� �迭�̶�, 0,1,2 �� �� sprite�� ���ٰ����ϰ�,
            ////answerArr[i]�� 0,1,2�� ���ڰ� ���� ������ ����

            //ButtonImgs[i].gameObject.SetActive(false);
            #endregion
        }

        #region ������ �������ִ� ���� �ڵ�
        ////���� �Ȱ�ġ�� 6�� ����ϱ�
        //List<int> list = new List<int>(6) { 0,0,1,1,2,2};
        //int index = 0;
        //for (int i = 0; i < answerArr.Length; i++)
        //{
        //    index = Random.Range(0, list.Count); //0���� list�� ���� ���� ���ڸ� ������ == index�� ��밡��.
        //    answerArr[i] = list[ index]; //list[index] �� �ش��ϴ� ���ڸ� answerarr�迭�� ���ʷ� ����
        //    list.RemoveAt(index); //�ش� �ε�����ġ�� ���ڸ� ������. ��ġ���ʴ� ��� ����.
        //    ButtonImgs[i].sprite = sprites[answerArr[i]]; //��ư �̹����鵵 6���� �����ϰ�, ���ʷ� �� �̹������� �����Ұǵ�,
        //    //������ �̹������� sprites�� �־����, �̶��� �迭�̶�, 0,1,2 �� �� sprite�� ���ٰ����ϰ�,
        //    //answerArr[i]�� 0,1,2�� ���ڰ� ���� ������ ����

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

        if (index == -1) //���� ���� �����Ѱ� ù ����
        {
            index = num;            
        }
        else //������ �����Ѱ� �ִ� ����.
        {
            if (answerArr[index] == answerArr[num]) //������ ������ ���� ���ڳ����
                                                    //���� ������ ���� ���� ������ ���ٸ�
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
            tryPoint++; //�õ�Ƚ���� �ø���
            index = -1; //�ε����� �ʱ�ȭ ���ֱ�

            score.text = $"���� Ƚ�� : {rightPoint}  /  �õ� Ƚ�� : {tryPoint}";                        
        }
        
        return true;
    }

    ////�ڷ�ƾ�� �߰��� �����ϰ� ���� ���...
    //StopCoroutine(cor); 
    //cor ��� ������ ��� �����ν�,
    //StopCoroutine(Reverse(�Ű����� ������ �ʿ��߾��µ�.. �װ͵� �� ���� ���� �����ϰ� ����));
    //cor = null; //���� �����ߴ� �ش� �ڷ�ƾ�� �����ϰ�, ���ο� �ڷ�ƾ�� �ٽ� ����� �� �ִ� ���°� ��...


    IEnumerator Reverse(int index1, int index2) //�ڷ�ƾ..
    {
        yield return new WaitForSeconds(1f); //������ �ʴ���. 
        ButtonArr[index1].SetImageFalse(false); //��������ģ�� ������
        ButtonArr[index2].SetImageFalse(false);//��� ����ģ�� ������
        IsPlayable = true;

        cor = null;
    }

    //void Reverse()
    //{
    //    ButtonArr[index1].SetImageFalse(false); //��������ģ�� ������
    //    ButtonArr[index2].SetImageFalse(false);//��� ����ģ�� ������
    //    IsPlayable = true;
    //}
}
