using UnityEngine;

public interface IHit
{
    void Hit(float damage, Vector3 dir);
    //Constructure.Stat GetAtt();
    float GetAtt();
}