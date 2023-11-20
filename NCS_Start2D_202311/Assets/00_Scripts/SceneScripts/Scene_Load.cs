using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene_Load : BaseScene
{
    public Image LoadingBar;    
    void Start()
    {
        SceneLoadManager.Instance.SetLoadInfo(this, true);
    }
    public void DrawLoadingBar(float val) //이안에도 코루틴을 넣어서 좀 찬찬히 표현하도록...
    {
        LoadingBar.fillAmount = val;
    }

    public override AllEnum.SceneKind GetSceneType()
    {
        return AllEnum.SceneKind.Load;
    }
    
    public override void ClearScene()
    {
        //이 씬을 나갈때에 해야하는 것들
        //예를 들면 진행하던 코루틴 종료//        
        //해당 씬에서 게임매니저가 특별하게 뭘하고 있었다면
        //혹은 플레이거ㅏ 뭘 특별하게 하고있었담녀

        // 그거 그만해라.. 
    }

    public override void Init() //게임씬에서.. 게임씬 초기화를 바로 가능..
    {
        //해당씬에 들어올떄 해야하는 것들...        
        LoadingBar.fillAmount = 0;
    }           
}
