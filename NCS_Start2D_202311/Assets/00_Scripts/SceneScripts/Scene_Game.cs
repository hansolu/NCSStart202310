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
        //�� ���� �������� �ؾ��ϴ� �͵�
        //���� ��� �����ϴ� �ڷ�ƾ ����//        
        //�ش� ������ ���ӸŴ����� Ư���ϰ� ���ϰ� �־��ٸ�
        //Ȥ�� �÷��̰Ť� �� Ư���ϰ� �ϰ��־����

        // �װ� �׸��ض�.. 

        GameManager.Instance.ControlCoroutine(false); //���Ļ��� �ڷ�ƾ ����.
        //�÷��̾� ��Ȱ��ȭ

    }

    public override AllEnum.SceneKind GetSceneType()
    {
        return AllEnum.SceneKind.Game;
    }

    public override void Init()
    {
        //�ش���� ���Ë� �ؾ��ϴ� �͵�...        

        GameManager.Instance.ControlCoroutine(true); //���Ļ��� �ڷ�ƾ ����.
    }    
    
}
