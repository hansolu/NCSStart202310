using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Bullet : MonoBehaviour
//{
//    //ȸ������ �̸� �� ���缭 ��ٸ�...

//    Vector2 targetpos= Vector2.zero; //Ÿ�� ��ġ
//    Vector2 moveDir = Vector2.zero; //���� ����
//    float speed = 0; //�̵��ӵ�
//    public void SetInfo(Vector2 myposition, Vector2 targetPos /*Tranform Tr*/) //�Ѿ��� �����, ������ �����ϴ� ��
//    {
//        transform.position = myposition; //�Ѿ��� ��ġ�� �������ִ°�. ���� �� �Ѿ��� ���鶧 �̹� �Ѿ� ��ġ ������ �ߴٸ� �����.
//        this.targetpos = targetPos; 
//        moveDir = targetPos - myposition;
//        speed = Time.fixedDeltaTime * 10; 
//    }
//    void FixedUpdate() //�浹, �̵� �����͵��� update���� fixedUpdate�� ���� ������ �� Ȯ���� ����.
//    {
//        transform.Translate(moveDir * speed/*�ӷ�*/); //�ش�������� speed�� �ӷ����� �̵�.

//        if (Vector2.Distance( transform.position , targetpos) <= 0.1f) // ����...//Distance�� ���� ���ѰŰ�...                        
//        {
//            //�������...
//        }
//    }
    
//}

//public class Monster :MonoBehaviour //�����ȳ����� ���⺻ ���� �Ѱ���
//{ }
//public class MonsterManager: MonoBehaviour //�̱���
//{
//    public static MonsterManager instance;  //�̱������� ���� �ڵ� �θ��� ���ؼ� ���� �̷��� �������
//    public List<Monster> monsterList = new List<Monster>(); //���� ����Ʈ�� ����������    

//    int layermask = 0;

//    void Awake()
//    {
//        layermask = 1<< LayerMask.NameToLayer("Enemy");
//    }

//    public Transform GetRandomMonsterInRange(Vector3 playerPos, float range) //���� �������� ������ ������ ��ġ�� �������� �����Լ�
//    {
//        //1��
//        //List<Monster> tmplist = new List<Monster>(); //�������� ��� ��
//        //for (int i = 0; i < monsterList.Count; i++)
//        //{
//        //    if (Vector2.Distance( monsterList[i].transform.position, playerPos) <= range) //
//        //    {
//        //        tmplist.Add(monsterList[i]);
//        //    }
//        //}

//        Collider2D[] cols= Physics2D.OverlapCircleAll(playerPos, range, layermask); //�÷��̾� ��ġ���� range���� ���� �� �� layermask�� �ش�Ǵ� �ֵ鸸 ������
//        if (cols.Length > 0) //���� ������ �����ϸ�
//        {
//            //return tmplist[Random.Range(0, tmplist.Count)].transform;
//            return cols[Random.Range(0, cols.Length)].transform; //�� ����Ȱ��߿� �������� �ϳ� �̾Ƽ� Ʈ�������� ��ȯ��.
//        }
//        else
//            return null;
        
//    }
//}

//public class Player : MonoBehaviour
//{
//    void FollowAttack() //���� �������� �Լ�
//    {
//        Transform TargetPos = MonsterManager.instance.GetRandomMonsterInRange(this.transform.position, 5); //���� ��ġ�� range������ �Ű������� �ѱ�

//        Bullet bullet = new Bullet(); //�Ѿ��� ������ (������ ������Ʈ Ǯ���� ������)
//        bullet.SetInfo(transform.position/*Ȥ�� �ѱ�*/, TargetPos.position); //�Ѿ˿� ������ ����
//        bullet.gameObject.SetActive(true);
//    }
//}
