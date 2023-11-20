using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Second : BaseScene
{
    void Start()
    {
        SceneLoadManager.Instance.SetLoadInfo(this);
    }
    public override void ClearScene()
    {
      
    }

    public override AllEnum.SceneKind GetSceneType()
    {
        SceneType = AllEnum.SceneKind.Second;
        return SceneType;
        //return SceneType;
    }

    public override void Init()
    {
        //해당씬에 들어올떄 해야하는 것들...        
    }    
    
}
