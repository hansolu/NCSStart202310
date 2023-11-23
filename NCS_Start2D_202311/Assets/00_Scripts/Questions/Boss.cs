using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{    
    float ShootRadius = 0; //반지름 길이..
    public float ShootDegree = 0;//각도를 지정하거나
    [Range(1,360)]
    public int ShootCount = 12;//360도로 몇개나 쏠건지

    float shootInterval = 0.8f; 
    float shootIntervalCheck = 0; //시간 체크용... 여기 본문에서는 활용하지 않았음.
    Vector2 bulletpos = Vector2.zero;

    //void Start()
    //{
    //    StartShoot();
    //}
    //public override void StartShoot()
    //{                
    //    ShootDegree = 360 / ShootCount; //내가 360도를 총알 몇개로 쪼개서 쓸건지
    //    if (transform.childCount > 0 && ShootRadius <= 0) //ShootRadius를 지정하지 않았다면. 내 첫번째 자식친구와의 거리로 ShootRadius를 사용할것이다.
    //    {
    //        ShootRadius = Mathf.Abs(transform.GetChild(0).transform.localPosition.y);//반지름
    //    }
    //    power = 3;
    //    HP = 50;
    //    HPMax = 50;
    //    orgpos.x = 0;
    //    orgpos.y = GameManager.Instance.Height -3;
    //    transform.position = orgpos;
    //    dir = (transform.GetChild(0).transform.position - transform.position).normalized;
    //    base.StartShoot();        
    //    cor = StartCoroutine(StartMove());
    //}

    //protected /*override*/ IEnumerator StartMove()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(shootInterval);
    //        for (int i = 0; i < ShootCount; i++)
    //        {
    //            dir = Quaternion.AngleAxis(-ShootDegree, Vector3.forward) * dir; //방향을 가지고 있는 dir가 있는데
    //            //해당 dir를 -ShootDegree만큼 회전시켜 받아내기.
    //            bulletpos.x = dir.x;
    //            bulletpos.y = dir.y + ShootRadius; //회전시킨 기본 dir에서 기존 점에서 shootradius만큼 거리를 벌림


    //            //게임매니저에서 Bullet 오브젝트 풀에서 총알을 꺼내서 세팅해주는 함수임.
    //            //매개변수로 총알의 위치, 플레이어의 총알인지, 총알의 타입은 무엇인지, 총알의 방향성, 총알의 위력 을 주도록 짰음.
    //            GameManager.Instance.CreateBullet(bulletpos, false, CTEnum.BulletKind.Boss, dir.normalized, power);
    //        }//전방위


    //        yield return new WaitForSeconds(shootInterval);
    //        for (int i = 0; i < ShootCount; i++)
    //        {
    //            dir = Quaternion.AngleAxis(-ShootDegree, Vector3.forward) * dir;
    //            bulletpos.x = dir.x;
    //            bulletpos.y = dir.y + ShootRadius;
    //            GameManager.Instance.CreateBullet(bulletpos, false, CTEnum.BulletKind.Boss, dir.normalized, power);
    //            yield return new WaitForSeconds(0.1f); //위와 동일한 코드인데 시간차를 주어 공격이 회오리치는 느낌을 준다                
    //        }

    //        yield return new WaitForSeconds(shootInterval);
    //        bulletpos = transform.GetChild(0).transform.position;
    //        for (float i = -1; i < 1; )
    //        {
    //            i += 0.2f;
    //            dir.x = Mathf.Sin(i);   //sin함수는 내부값으로 뭘 주던 -1에서 1을 반환함. 유니티에서 삼각함수는 변수로 radian을 받기떄문에
    //            //숫자가 10~단위가 아님. 만약 10도씩의 개념으로 뭘 하고 싶다면 Mathf.Sin(원하는각도 * Mathf.Deg2Rad) 해야 Degree를 Radian으로 바꿔서 쓸수잇다
    //            dir.y = -1;
    //            GameManager.Instance.CreateBullet(bulletpos, false, CTEnum.BulletKind.Boss, dir.normalized, power);
    //            yield return new WaitForSeconds(0.2f);
    //        }
    //        for (float i = 1; i > -1;)
    //        {
    //            i -= 0.2f;
    //            dir.x = Mathf.Sin(i);                
    //            dir.y = -1;
    //            GameManager.Instance.CreateBullet(bulletpos, false, CTEnum.BulletKind.Boss, dir.normalized, power);
    //            yield return new WaitForSeconds(0.2f);
    //        }

    //        yield return new WaitForSeconds(shootInterval);
    //        bulletpos = transform.GetChild(0).transform.position;
    //        for (float i = -1; i < 1;)
    //        {
    //            i += 0.2f;
    //            dir.x = Mathf.Sin(i);
    //            dir.y = -1;
    //            GameManager.Instance.CreateBullet(bulletpos, false, CTEnum.BulletKind.Boss, dir.normalized, power);                

    //            //위 코드와 동일한데 시간차를 줄이는 것 만으로 부채꼴 공격이 가능해짐.
    //        }           
    //    }
    //}
}
