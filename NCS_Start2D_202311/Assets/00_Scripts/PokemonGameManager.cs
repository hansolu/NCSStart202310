using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonGameManager : MonoBehaviour
{
    #region 싱글톤
    static PokemonGameManager instance = null;
    public static PokemonGameManager Instance => instance;
    #endregion               

    #region UI단    

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
        
    string[] name = new string[3] { "파이리", "이상해씨", "꼬부기" };
    
    int skillnum = 0;

    //Coroutine MainGame = null;
    void Awake()
    {
        #region 기본 싱글톤
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
        IsInputable = false; //인풋기능받는것을 허락할 bool
    }
    void Start()
    {
        notice.text = "포켓몬을 선택하고 체력과 공격력을 입력해주세요";
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

    public void ClickSkill(int num) //버튼용.
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
            notice.text = "포켓몬 선택없이 게임을 진행할 수 없습니다.";
            return;
        }
        else if (string.IsNullOrEmpty(input_HP.text)
            || string.IsNullOrEmpty(input_Att.text))
        {
            notice.text = "입력이 누락된 곳이 있습니다.";
            return;
        }
        
        int hp = int.Parse(input_HP.text);
        int att = int.Parse(input_Att.text);
        
        if (hp < att)
        {
            notice.text = "공격력보다 hp가 낮아서는 안됩니다";
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
            notice.text = "내 포켓몬의 <b><color=#ffffff>승리</color>!</b>";
        }
        else if (MyPokemon.pokemon.IsAlive == false)
        {
            notice.text = "플레이어의 <b><color=#ff0000>패배</color>...</b>";
        }
        else
            StartCoroutine(MainFlow());    
    }

    IEnumerator MainFlow()
    {        
        if (mytern)
        {
            notice.text = "플레이어의 턴입니다";

            yield return new WaitForSeconds(0.8f);
            notice.text = "공격 선택";
            IsInputable = true;

            yield return new WaitUntil(() => IsInputable == false);
            //IsInputable이 false가 될때까지 다음으로 안넘어가고 기다리겠다

            notice.text = skillnum + "번 공격 선택";            
            yield return new WaitForSeconds(0.5f);

            if (MyPokemon.Attack(EnemyPokemon.pokemon, skillnum, out float calDamage))
            {
                notice.text = "공격 데미지 : "+calDamage;
                EnemyPokemon.DrawHP();
            }
            else
            {
                notice.text = "아직 공격할 수 없는 시간입니다";
            }
            yield return new WaitForSeconds(0.8f);
            mytern = false;            
        }
        else
        {
            notice.text = "적의 턴입니다";
            yield return new WaitForSeconds(0.8f);
            notice.text = "적의 공격";
            skillnum = 0;
            if (EnemyPokemon.Attack(MyPokemon.pokemon, skillnum, out float calDamage))
            {
                notice.text = "적의 공격 데미지 : " + calDamage;
                MyPokemon.DrawHP();
            }
            else
            {
                notice.text = "아직 공격할 수 없는 시간입니다";
            }
            yield return new WaitForSeconds(0.8f);

            mytern = true;
        }
        GamePlay();
    }
}
