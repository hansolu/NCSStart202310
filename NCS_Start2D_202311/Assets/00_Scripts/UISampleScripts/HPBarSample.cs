using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarSample : MonoBehaviour
{
    #region HPBar �ǽ�
    public Slider slider; //hp bar�ΰ��̰�

    public int MaxHP = 100;//��ü ����
    int HP = 100;
    public int Damage = 10; //�ǰݴ��� ������
    #endregion

    public Image[] coolImg; //��Ÿ�� �̹���
        
    //[SerializeField]  //[����Ƽ���� �����ϴ� ���]   �̷��� ����ϴ� ���� Attribute��� �մϴ�. 
    public float[] skillCooltime = new float[3] {0,0,0};

    Coroutine[] coolCor = new Coroutine[3];

    void Start()
    {
        HP = MaxHP; //���� ���� ������ ��ü �������� ����.
        slider.maxValue = MaxHP;
        slider.value = MaxHP;

        for (int i = 0; i < coolCor.Length; i++)
        {
            coolCor[i] = null;
            coolImg[i].fillAmount = 0;
        }        
    }

    void Update() //input.getkey������ Update���� �ؾ� ���� ��Ȯ�ϴ�...
    {
        #region HPBar����
        if (Input.GetKeyDown(KeyCode.Space)) //�����̽��� �����ٸ�
        {
            HP -= Damage; //���� ���� ü�� damage�� ���
            slider.value = HP; //�����̴��� ���� ���� ü���� �����ϰ� ��.
        }
        else if (Input.GetKeyDown(KeyCode.KeypadPlus)) //������ Ű�е� �÷����� ������ �±�?�Ѵٰ� Ĩ�ô�.
            //�±��ϸ� �ǰ� 100 ������.
        {
            MaxHP += 100;
            slider.maxValue = MaxHP;
            HP += 100;
            slider.value = HP;
        }
        #endregion 

        //Debug.Log("�������Ӵ� �ð� üũ :  " + Time.deltaTime);
        //Debug.Log("�� fixedupdate �� �ð� :  " + Time.fixedDeltaTime);
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

        //Input.GetKeyDown //���� Ű�� ���� �ѹ�
        //Input.GetKeyup //���� Ű�� �����ٰ� ������ �ѹ�
        //Input.GetKey //�����̻� ���... 2~3�����ӵ��� ���� �������ֱ⶧����..
        ////�ּ� 2~3������ �ҷ���..

        //Input.GetMouseButtonDown(0) //����Ŭ��
        //Input.GetMouseButtonDown(1) //����Ŭ��
        //Input.GetMouseButtonDown(2) //��Ŭ��

        //KeyCode.Keypad1 //����ǥ ������ �ִ� ���� �е�Ű�� Ű�е�(����)
        //    ���� ����, ���ڿ� f1~12 ���̿� �ִ� ����Ű���� Alpha����
    }

    //void FixedUpdate()//�����ð����� ����. �浹üũ ���� �� ó���� ����.
    //{        
    //}

    //void LateUpdate() //��� ������Ʈ�� ������ ����. ī�޶��̵�.. �������� ���� ����..
    //{        
    //}

    IEnumerator Cooltime(int num)
    {
        coolImg[num].fillAmount = 1;
        float time = 0;
        float val = 0;
        while (coolImg[num].fillAmount > 0)
        {
            time += Time.fixedDeltaTime;//fixedupdate�� �����ϴ� �ð�.
            //Time.deltaTime; //update�ѹ� �����Ҷ����� �ɸ��� �ð�
            val = skillCooltime[num] - time;
            coolImg[num].fillAmount = val / skillCooltime[num];
            yield return new WaitForFixedUpdate();
        }
        coolCor[num] = null;
    }

    //��Ÿ���� 1�ʶ�� �ҋ�
    //������ 0 ������ 1�̰�
    //��Ÿ�ӵ� 1�̸�
    //�׳� ���� ���� ��Ÿ���� ����~~~�ϴٰ� 1�� �Ǹ� ��

    //float time = 0;
    //time +=Time.fixedDeltaTime;//0.02
    //fillamount = time;

    //��Ÿ���� 2�ʶ�� �ҋ�
    //������ 0�̰� �̰͵� �� ���� 1

    //��Ÿ�� 2: fillamount�� ���� 1  = ���� �����ð� 1 : fillamount�� ���� ���� X


    //float time = 0;
    //time +=Time.fixedDeltaTime;//0.02
    //fillamount = time / ��Ÿ��;

    //��Ÿ�� : 1 = time : X    == 0.5

    //(1 x1 )/2 == 0.5
}
