using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarSample : MonoBehaviour
{
    #region HPBar 실습
    public Slider slider; //hp bar인것이고

    public int MaxHP = 100;//전체 피통
    int HP = 100;
    public int Damage = 10; //피격당할 데미지
    #endregion

    public Image[] coolImg; //쿨타임 이미지
        
    //[SerializeField]  //[유니티에서 제공하는 기능]   이렇게 사용하는 것을 Attribute라고 합니다. 
    public float[] skillCooltime = new float[3] {0,0,0};

    Coroutine[] coolCor = new Coroutine[3];

    void Start()
    {
        HP = MaxHP; //나의 현재 피통을 전체 피통으로 세팅.
        slider.maxValue = MaxHP;
        slider.value = MaxHP;

        for (int i = 0; i < coolCor.Length; i++)
        {
            coolCor[i] = null;
            coolImg[i].fillAmount = 0;
        }        
    }

    void Update() //input.getkey종류는 Update에서 해야 거의 정확하다...
    {
        #region HPBar관련
        if (Input.GetKeyDown(KeyCode.Space)) //스페이스가 눌린다면
        {
            HP -= Damage; //나의 현재 체력 damage로 깎기
            slider.value = HP; //슬라이더와 나의 현재 체력을 동일하게 함.
        }
        else if (Input.GetKeyDown(KeyCode.KeypadPlus)) //오른쪽 키패드 플러스를 누르면 승급?한다고 칩시다.
            //승급하면 피가 100 증가함.
        {
            MaxHP += 100;
            slider.maxValue = MaxHP;
            HP += 100;
            slider.value = HP;
        }
        #endregion 

        //Debug.Log("한프레임당 시간 체크 :  " + Time.deltaTime);
        //Debug.Log("한 fixedupdate 당 시간 :  " + Time.fixedDeltaTime);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (coolCor[0]==null)
            {
                coolCor[0] = StartCoroutine(Cooltime(0));
            }            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (coolCor[1] == null)
            {
                coolCor[1] = StartCoroutine(Cooltime(1));
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (coolCor[2] == null)
            {
                coolCor[2] = StartCoroutine(Cooltime(2));
            }
        }

        //Input.GetKeyDown //내가 키를 누른 한번
        //Input.GetKeyup //내가 키를 눌렀다가 뗐을때 한번
        //Input.GetKey //누른이상 계속... 2~3프레임동안 보통 누르고있기때문에..
        ////최소 2~3번씩은 불려요..

        //Input.GetMouseButtonDown(0) //왼쪽클릭
        //Input.GetMouseButtonDown(1) //왼쪽클릭
        //Input.GetMouseButtonDown(2) //휠클릭

        //KeyCode.Keypad1 //방향표 오른편에 있는 숫자 패드키는 키패드(숫자)
        //    자판 위에, 글자와 f1~12 사이에 있는 숫자키들은 Alpha숫자
    }

    //void FixedUpdate()//일정시간마다 시행. 충돌체크 같은 것 처리에 좋음.
    //{        
    //}

    //void LateUpdate() //모든 업데이트가 끝나고 시행. 카메라이동.. 같은곳에 자주 쓰임..
    //{        
    //}

    IEnumerator Cooltime(int num)
    {
        coolImg[num].fillAmount = 1;
        float time = 0;
        float val = 0;
        while (coolImg[num].fillAmount > 0)
        {
            time += Time.fixedDeltaTime;//fixedupdate를 실행하는 시간.
            //Time.deltaTime; //update한번 시행할때마다 걸리는 시간
            val = skillCooltime[num] - time;
            coolImg[num].fillAmount = val / skillCooltime[num];
            yield return new WaitForFixedUpdate();
        }
        coolCor[num] = null;
    }

    //쿨타임이 1초라고 할떄
    //없으면 0 있으면 1이고
    //쿨타임도 1이면
    //그냥 내가 뭔가 쿨타임이 증가~~~하다가 1이 되면 끝

    //float time = 0;
    //time +=Time.fixedDeltaTime;//0.02
    //fillamount = time;

    //쿨타임이 2초라고 할떄
    //없으면 0이고 이것도 다 차면 1

    //쿨타임 2: fillamount의 끝은 1  = 실제 지난시간 1 : fillamount의 현재 값은 X


    //float time = 0;
    //time +=Time.fixedDeltaTime;//0.02
    //fillamount = time / 쿨타임;

    //쿨타임 : 1 = time : X    == 0.5

    //(1 x1 )/2 == 0.5
}
