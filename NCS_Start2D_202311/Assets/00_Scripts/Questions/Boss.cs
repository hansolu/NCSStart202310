using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{    
    float ShootRadius = 0; //������ ����..
    public float ShootDegree = 0;//������ �����ϰų�
    [Range(1,360)]
    public int ShootCount = 12;//360���� ��� �����

    float shootInterval = 0.8f; 
    float shootIntervalCheck = 0; //�ð� üũ��... ���� ���������� Ȱ������ �ʾ���.
    Vector2 bulletpos = Vector2.zero;

    //void Start()
    //{
    //    StartShoot();
    //}
    //public override void StartShoot()
    //{                
    //    ShootDegree = 360 / ShootCount; //���� 360���� �Ѿ� ��� �ɰ��� ������
    //    if (transform.childCount > 0 && ShootRadius <= 0) //ShootRadius�� �������� �ʾҴٸ�. �� ù��° �ڽ�ģ������ �Ÿ��� ShootRadius�� ����Ұ��̴�.
    //    {
    //        ShootRadius = Mathf.Abs(transform.GetChild(0).transform.localPosition.y);//������
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
    //            dir = Quaternion.AngleAxis(-ShootDegree, Vector3.forward) * dir; //������ ������ �ִ� dir�� �ִµ�
    //            //�ش� dir�� -ShootDegree��ŭ ȸ������ �޾Ƴ���.
    //            bulletpos.x = dir.x;
    //            bulletpos.y = dir.y + ShootRadius; //ȸ����Ų �⺻ dir���� ���� ������ shootradius��ŭ �Ÿ��� ����


    //            //���ӸŴ������� Bullet ������Ʈ Ǯ���� �Ѿ��� ������ �������ִ� �Լ���.
    //            //�Ű������� �Ѿ��� ��ġ, �÷��̾��� �Ѿ�����, �Ѿ��� Ÿ���� ��������, �Ѿ��� ���⼺, �Ѿ��� ���� �� �ֵ��� ®��.
    //            GameManager.Instance.CreateBullet(bulletpos, false, CTEnum.BulletKind.Boss, dir.normalized, power);
    //        }//������


    //        yield return new WaitForSeconds(shootInterval);
    //        for (int i = 0; i < ShootCount; i++)
    //        {
    //            dir = Quaternion.AngleAxis(-ShootDegree, Vector3.forward) * dir;
    //            bulletpos.x = dir.x;
    //            bulletpos.y = dir.y + ShootRadius;
    //            GameManager.Instance.CreateBullet(bulletpos, false, CTEnum.BulletKind.Boss, dir.normalized, power);
    //            yield return new WaitForSeconds(0.1f); //���� ������ �ڵ��ε� �ð����� �־� ������ ȸ����ġ�� ������ �ش�                
    //        }

    //        yield return new WaitForSeconds(shootInterval);
    //        bulletpos = transform.GetChild(0).transform.position;
    //        for (float i = -1; i < 1; )
    //        {
    //            i += 0.2f;
    //            dir.x = Mathf.Sin(i);   //sin�Լ��� ���ΰ����� �� �ִ� -1���� 1�� ��ȯ��. ����Ƽ���� �ﰢ�Լ��� ������ radian�� �ޱ⋚����
    //            //���ڰ� 10~������ �ƴ�. ���� 10������ �������� �� �ϰ� �ʹٸ� Mathf.Sin(���ϴ°��� * Mathf.Deg2Rad) �ؾ� Degree�� Radian���� �ٲ㼭 �����մ�
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

    //            //�� �ڵ�� �����ѵ� �ð����� ���̴� �� ������ ��ä�� ������ ��������.
    //        }           
    //    }
    //}
}
