using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Bullet : MonoBehaviour
//{
//    //회전값도 미리 잘 맞춰서 줬다면...

//    Vector2 targetpos= Vector2.zero; //타겟 위치
//    Vector2 moveDir = Vector2.zero; //가는 방향
//    float speed = 0; //이동속도
//    public void SetInfo(Vector2 myposition, Vector2 targetPos /*Tranform Tr*/) //총알을 만들고, 데이터 세팅하는 곳
//    {
//        transform.position = myposition; //총알의 위치를 세팅해주는것. 만약 이 총알을 만들때 이미 총알 위치 세팅을 했다면 없어도됨.
//        this.targetpos = targetPos; 
//        moveDir = targetPos - myposition;
//        speed = Time.fixedDeltaTime * 10; 
//    }
//    void FixedUpdate() //충돌, 이동 같은것들은 update말고 fixedUpdate가 좀더 오류가 날 확률이 적음.
//    {
//        transform.Translate(moveDir * speed/*속력*/); //해당방향으로 speed의 속력으로 이동.

//        if (Vector2.Distance( transform.position , targetpos) <= 0.1f) // 도착...//Distance는 내가 편한거고...                        
//        {
//            //사라지기...
//        }
//    }
    
//}

//public class Monster :MonoBehaviour //오류안내려고 ㅇ기본 선언만 한것임
//{ }
//public class MonsterManager: MonoBehaviour //싱글톤
//{
//    public static MonsterManager instance;  //싱글톤으로 샘플 코드 부르기 위해서 대충 이렇게 만든것임
//    public List<Monster> monsterList = new List<Monster>(); //몬스터 리스트를 가지고있음    

//    int layermask = 0;

//    void Awake()
//    {
//        layermask = 1<< LayerMask.NameToLayer("Enemy");
//    }

//    public Transform GetRandomMonsterInRange(Vector3 playerPos, float range) //나의 범위내의 랜덤한 몬스터의 위치를 가져오기 위한함수
//    {
//        //1번
//        //List<Monster> tmplist = new List<Monster>(); //범위안의 모든 ㅁ
//        //for (int i = 0; i < monsterList.Count; i++)
//        //{
//        //    if (Vector2.Distance( monsterList[i].transform.position, playerPos) <= range) //
//        //    {
//        //        tmplist.Add(monsterList[i]);
//        //    }
//        //}

//        Collider2D[] cols= Physics2D.OverlapCircleAll(playerPos, range, layermask); //플레이어 위치에서 range만한 원을 빡 찍어서 layermask에 해당되는 애들만 검출함
//        if (cols.Length > 0) //검출 내역이 존재하면
//        {
//            //return tmplist[Random.Range(0, tmplist.Count)].transform;
//            return cols[Random.Range(0, cols.Length)].transform; //그 검출된것중에 랜덤으로 하나 뽑아서 트랜스폼을 반환함.
//        }
//        else
//            return null;
        
//    }
//}

//public class Player : MonoBehaviour
//{
//    void FollowAttack() //뭔가 유도공격 함수
//    {
//        Transform TargetPos = MonsterManager.instance.GetRandomMonsterInRange(this.transform.position, 5); //나의 위치와 range범위를 매개변수로 넘김

//        Bullet bullet = new Bullet(); //총알을 생성해 (보통은 오브젝트 풀에서 꺼내서)
//        bullet.SetInfo(transform.position/*혹은 총구*/, TargetPos.position); //총알에 데이터 세팅
//        bullet.gameObject.SetActive(true);
//    }
//}
