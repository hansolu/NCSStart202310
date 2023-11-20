using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//씬매니저가 다음씬으로 넘어갈떄마다
//이전씬을 정리하고
//새로운씬에서 세팅해야할것을 세팅할텐데

public abstract class BaseScene : MonoBehaviour 
{
    public AllEnum.SceneKind SceneType;
    public abstract AllEnum.SceneKind GetSceneType();
    
    public abstract void Init();//모든씬이, 초기화 함수와

    public abstract void ClearScene(); //정리하는 함수를 가지게 되겠죠..?
}
