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
        //�ش���� ���Ë� �ؾ��ϴ� �͵�...        
    }    
    
}
