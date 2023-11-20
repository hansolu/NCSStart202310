using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Game : BaseScene
{
    void Start()
    {
        SceneLoadManager.Instance.SetLoadInfo(this);
    }
    public override void ClearScene()
    {
        //이 씬을 나갈때에 해야하는 것들
        //예를 들면 진행하던 코루틴 종료//        
        //해당 씬에서 게임매니저가 특별하게 뭘하고 있었다면
        //혹은 플레이거ㅏ 뭘 특별하게 하고있었담녀

        // 그거 그만해라.. 

        GameManager.Instance.ControlCoroutine(false); //음식생성 코루틴 멈춤.
        //플레이어 비활성화

    }

    public override AllEnum.SceneKind GetSceneType()
    {
        return AllEnum.SceneKind.Game;
    }

    public override void Init()
    {
        //해당씬에 들어올떄 해야하는 것들...        

        GameManager.Instance.ControlCoroutine(true); //음식생성 코루틴 멈춤.
    }    
    
}
