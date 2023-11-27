using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //void Start()
    //{
    //    AA();
    //}
    void AA()
    {
        //PlayerPrefs가 간편하지만 잘 안쓰는 이유 : 
        //보안에 취약함 

        //저장 위치가 레지스트리에 저장됨.

        //=> 딜리트를 강제로 해주지않으면
        //게임을 삭제해도 내역이 남아있음...


        //PlayerPrefs.SetInt("Level", /*플레이어 레벨*/3);

        //int 플레이어레벨 = PlayerPrefs.GetInt("Level",1);
        //Debug.Log("레벨키가 있음?" +PlayerPrefs.HasKey("Level"));
        //Debug.Log("AA키가 있음?" + PlayerPrefs.HasKey("AA"));

        //Debug.Log("레벨값?" + PlayerPrefs.GetInt("Level"));

        //PlayerPrefs.SetInt("SceneNum", /*씬번호*/0);
        //PlayerPrefs.SetString("SceneName", /*씬이름*/"Game");

        //string infos = "Name:캐릭터아이디,HP:100,MaxHP:200";
        //PlayerPrefs.SetString("PlayerInfos", infos);

        //string info2 = PlayerPrefs.GetString("PlayerInfos");
        //string[] splitarr= info2.Split(',');
        //string ID = splitarr[0].Split(":")[1];
        //string HP = splitarr[1].Split(":")[1];
        //string MaxHP = splitarr[2].Split(":")[1];
    }
}


