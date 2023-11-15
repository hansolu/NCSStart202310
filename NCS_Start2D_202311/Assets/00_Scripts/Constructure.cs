using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructure
{
    public struct Stat
    {
        public float HP;
        public float MaxHP;
        public float Att;
        public Stat(float hp, float att)
        {
            this.HP = hp;
            this.MaxHP = hp;
            this.Att = att;
        }
    }
}
