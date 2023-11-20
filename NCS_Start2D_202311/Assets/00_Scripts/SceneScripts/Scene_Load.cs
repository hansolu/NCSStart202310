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
    public void DrawLoadingBar(float val) //�̾ȿ��� �ڷ�ƾ�� �־ �� ������ ǥ���ϵ���...
    {
        LoadingBar.fillAmount = val;
    }

    public override AllEnum.SceneKind GetSceneType()
    {
        return AllEnum.SceneKind.Load;
    }
    
    public override void ClearScene()
    {
        //�� ���� �������� �ؾ��ϴ� �͵�
        //���� ��� �����ϴ� �ڷ�ƾ ����//        
        //�ش� ������ ���ӸŴ����� Ư���ϰ� ���ϰ� �־��ٸ�
        //Ȥ�� �÷��̰Ť� �� Ư���ϰ� �ϰ��־����

        // �װ� �׸��ض�.. 
    }

    public override void Init() //���Ӿ�����.. ���Ӿ� �ʱ�ȭ�� �ٷ� ����..
    {
        //�ش���� ���Ë� �ؾ��ϴ� �͵�...        
        LoadingBar.fillAmount = 0;
    }           
}
