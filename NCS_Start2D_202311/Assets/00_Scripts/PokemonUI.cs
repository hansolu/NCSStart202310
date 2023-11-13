using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonUI : MonoBehaviour
{
    public Text Name;
    public Slider HP;
    public Image[] Skill; 
    
    public SpriteRenderer PokeSpr;
    public Pokemon pokemon { get; private set; } = null;

    Coroutine[] cor = null;
    public void CreatePokemon(bool isEnemy, int selectnum, string name,int hp, int att, float[] arr)
    { 
        
        pokemon = new Pokemon((AllEnum.PokeType)selectnum,
            name, hp, att, arr);
        HP.maxValue = pokemon.MaxHP;
        cor = new Coroutine[arr.Length];
        for (int i = 0; i < cor.Length; i++)
        {
            cor[i] = null;
        }
        for (int i = 0; i < Skill.Length; i++)
        {
            Skill[i].fillAmount = 0;
        }
        switch (pokemon.type)
        {
            case AllEnum.PokeType.불:
                PokeSpr.color = Color.red;
                break;
            case AllEnum.PokeType.풀:
                PokeSpr.color = Color.green;
                break;
            case AllEnum.PokeType.물:
                PokeSpr.color = Color.blue;
                break;                        
        }
        
        if (isEnemy)
        {
            Name.text = "<b><color=#ff0000>"+
                pokemon.Name + "</color></b>";
        }
        else
        Name.text = pokemon.Name;
    }
    public bool Attack(Pokemon enemy, int skillnum, out float damage)
    {
        bool Isattck = pokemon.Attack(enemy, skillnum, out float _damage);
        damage = _damage;
        if (cor[skillnum] == null)
        {
            cor[skillnum] = StartCoroutine(SkillCool(skillnum));
        }
        
        return Isattck;
    }
    IEnumerator SkillCool(int skillnum)
    {
        Skill[skillnum].fillAmount = 1;
        float time = 0;
        float val = 0;
        while (Skill[skillnum].fillAmount > 0)
        {
            time += Time.fixedDeltaTime;//fixedupdate를 실행하는 시간.
            //Time.deltaTime; //update한번 시행할때마다 걸리는 시간
            val = pokemon.SkillCoolTime[skillnum] - time;
            Skill[skillnum].fillAmount = val / pokemon.SkillCoolTime[skillnum];
            yield return new WaitForFixedUpdate();
        }
        cor[skillnum] = null;
        pokemon.SetCoolReset(skillnum);
    }

    public void DrawHP()
    {
        HP.value = pokemon.HP;
    }
}
