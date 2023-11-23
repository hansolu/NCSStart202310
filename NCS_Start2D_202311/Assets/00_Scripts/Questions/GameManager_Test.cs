using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CharacterType
{ 
    Near,
    Far,

    End
}

public struct CharacterInfo //가볍고 빠름. 클래스는 참조형식 /구조체는 값형식.
{
    public string name;
    public float HP;
    public float MaxHP;
    public float Att;
    public CharacterType type;
    public CharacterInfo(string name, float hp, float att, CharacterType type)
    {
        this.name = name;
        this.HP = hp;
        this.MaxHP = hp;
        this.Att = att;
        this.type = type;
    }
}

public class Player2//기본 플레이어 클래스
{    
    CharacterInfo info;
    public CharacterType GetType => info.type;
    public void SetCharacterinfo(CharacterInfo info)
    {
        this.info = info;
    }
    //
}

public class GameManager_Test :Singleton<GameManager_Test>
{
    public Player2 myplayer;

    public void CreateMyCharacter(CharacterInfo info)
    {
        //myplayer = new Player( );
        myplayer.SetCharacterinfo(info);
    }
}
