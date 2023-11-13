using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonGameManager : MonoBehaviour
{
    #region �̱���
    static PokemonGameManager instance = null;
    public static PokemonGameManager Instance => instance;
    #endregion               

    #region UI��    

    public GameObject startPanel;
    public GameObject gamePanel;
    
    public Toggle[] TypeSelect;
    public InputField input_HP;
    public InputField input_Att;

    public Text notice;
    #endregion
    
    public PokemonUI MyPokemon;
    public PokemonUI EnemyPokemon;

    bool mytern = true;
    bool IsInputable = false;
        
    string[] name = new string[3] { "���̸�", "�̻��ؾ�", "���α�" };
    
    int skillnum = 0;

    //Coroutine MainGame = null;
    void Awake()
    {
        #region �⺻ �̱���
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
        #endregion
        IsInputable = false; //��ǲ��ɹ޴°��� ����� bool
    }
    void Start()
    {
        notice.text = "���ϸ��� �����ϰ� ü�°� ���ݷ��� �Է����ּ���";
        EnemyPokemon.gameObject.SetActive(false);
        gamePanel.SetActive(false);
        startPanel.SetActive(true);
        for (int i = 0; i < TypeSelect.Length; i++)
        {
            TypeSelect[i].transform.GetChild(1).GetComponent<Text>().text = name[i];
            TypeSelect[i].isOn = false;
        }        
    }

    public void SelectCharacter()
    {
        //if (TypeSelect[0].isOn == false 
        //    && TypeSelect[1].isOn == false
        //    && TypeSelect[2].isOn == false)
        //{
        //    MyPokemon.PokeSpr.color = Color.white;
        //}
        //else
        MyPokemon.PokeSpr.color = new Color(TypeSelect[0].isOn? 1:0,
            TypeSelect[1].isOn ? 1 : 0,
            TypeSelect[2].isOn ? 1 : 0,
            1);                
    }

    void Update()
    {
        if (IsInputable == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ClickSkill(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ClickSkill(1);
        }
    }

    public void ClickSkill(int num) //��ư��.
    {
        skillnum = num;
        IsInputable = false;
    }
    public void GameStart()
    {
        int selectnum = -1;
        for (int i = 0; i < TypeSelect.Length; i++)
        {
            if (TypeSelect[i].isOn)
            {
                selectnum = i;
                break;
            }            
        }

        if (selectnum < 0)
        {
            notice.text = "���ϸ� ���þ��� ������ ������ �� �����ϴ�.";
            return;
        }
        else if (string.IsNullOrEmpty(input_HP.text)
            || string.IsNullOrEmpty(input_Att.text))
        {
            notice.text = "�Է��� ������ ���� �ֽ��ϴ�.";
            return;
        }
        
        int hp = int.Parse(input_HP.text);
        int att = int.Parse(input_Att.text);
        
        if (hp < att)
        {
            notice.text = "���ݷº��� hp�� ���Ƽ��� �ȵ˴ϴ�";
            return;
        }
        
        MyPokemon.CreatePokemon(false, selectnum,
            name[selectnum], hp, att, new float[2] { 1.5f, 2f });

        int enemytype = Random.Range(0, (int)AllEnum.PokeType.END);
        EnemyPokemon.gameObject.SetActive(true);

        EnemyPokemon.CreatePokemon(true,enemytype,
            name[enemytype],
            Random.Range(1, hp), Random.Range(1, att), new float[1] { Random.Range(15, 21) * 0.1f } );
        
        gamePanel.SetActive(true);
        startPanel.SetActive(false);

        mytern = true;
        GamePlay();
    }

    void GamePlay()
    {        
        if (EnemyPokemon.pokemon.IsAlive == false)
        {
            notice.text = "�� ���ϸ��� <b><color=#ffffff>�¸�</color>!</b>";
        }
        else if (MyPokemon.pokemon.IsAlive == false)
        {
            notice.text = "�÷��̾��� <b><color=#ff0000>�й�</color>...</b>";
        }
        else
            StartCoroutine(MainFlow());    
    }

    IEnumerator MainFlow()
    {        
        if (mytern)
        {
            notice.text = "�÷��̾��� ���Դϴ�";

            yield return new WaitForSeconds(0.8f);
            notice.text = "���� ����";
            IsInputable = true;

            yield return new WaitUntil(() => IsInputable == false);
            //IsInputable�� false�� �ɶ����� �������� �ȳѾ�� ��ٸ��ڴ�

            notice.text = skillnum + "�� ���� ����";            
            yield return new WaitForSeconds(0.5f);

            if (MyPokemon.Attack(EnemyPokemon.pokemon, skillnum, out float calDamage))
            {
                notice.text = "���� ������ : "+calDamage;
                EnemyPokemon.DrawHP();
            }
            else
            {
                notice.text = "���� ������ �� ���� �ð��Դϴ�";
            }
            yield return new WaitForSeconds(0.8f);
            mytern = false;            
        }
        else
        {
            notice.text = "���� ���Դϴ�";
            yield return new WaitForSeconds(0.8f);
            notice.text = "���� ����";
            skillnum = 0;
            if (EnemyPokemon.Attack(MyPokemon.pokemon, skillnum, out float calDamage))
            {
                notice.text = "���� ���� ������ : " + calDamage;
                MyPokemon.DrawHP();
            }
            else
            {
                notice.text = "���� ������ �� ���� �ð��Դϴ�";
            }
            yield return new WaitForSeconds(0.8f);

            mytern = true;
        }
        GamePlay();
    }
}
