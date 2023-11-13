using System.Collections;
using System.Collections.Generic;
using System;

//모노비헤이비어를 상속받지 않는 클래스의사용 :
//구조체마냥 
//기능 많은 구조체 라는 느낌으로

public class Pokemon
{    
    public string Name { get; private set; } = "";
    public AllEnum.PokeType type = AllEnum.PokeType.END;        
    public bool IsAlive => HP > 0;
    float HP_float = 0;
    public int HP { get; private set; } = 0;
    public int MaxHP { get; private set; } = 0;
    
    public float Damage { get; private set; } = 0;
    
    public float[] SkillCoolTime { get; private set; }
    public bool[] Attackable;
    public Pokemon(AllEnum.PokeType type, string name, int hp, int damage,
        float[] cooltime)
    {
        this.type = type;
        Name = name;
        this.HP = hp;
        this.MaxHP = hp;
        this.Damage = damage;
        HP_float = hp;
        SkillCoolTime = cooltime;
        Attackable = new bool[cooltime.Length];
        for (int i = 0; i < Attackable.Length; i++)
        {
            Attackable[i] = true;
        }
    }

    public void SetCoolReset(int skillnum)
    {
        Attackable[skillnum] = true;
    }

    public bool Attack(Pokemon enemyPoke, int skillnum, out float damage )
    {
        if (Attackable[skillnum] == false)
        {
            damage = 0;
            return false;
        }

        Attackable[skillnum] = false; //바로 쿨타임 셋
        switch (skillnum)
        {
            case 1:
                damage = Skill_UsingType(enemyPoke);
                break;
            default:
                damage = Skill_Normal(enemyPoke);
                break;
        }
        return true;
    }

    public void Hit(float damage)
    {
        HP_float -= damage;
        HP = (int)HP_float;
    }
    float CheckType(AllEnum.PokeType enemytype)
    {
        if (enemytype == type)
        {
            return 1;
        }
        else
        {
            if (enemytype < type) 
            {
                if (enemytype == AllEnum.PokeType.불 && type == AllEnum.PokeType.물)
                {
                    return 2;
                }
                else
                    return 0.5f;
            }
            else
            {
                if (enemytype == AllEnum.PokeType.물 && type == AllEnum.PokeType.불)
                {
                    return 0.5f;
                }
                else
                    return 2f;
            }
        }
    }
    public float Skill_Normal(Pokemon enemypoke) 
    {                 
        enemypoke.Hit(Damage);
        return Damage;
    }
    public float Skill_UsingType(Pokemon enemypoke)
    {
        float val = Damage * CheckType(enemypoke.type);
        enemypoke.Hit(val);
        return val;
    }
}
