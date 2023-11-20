using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //���̵��� ���� �ݵ�� �־���ϴ� using.. 

//���Ŵ����� ���� �������, �ε��Ŵ����� ���� ����� ���̱��ѵ�..
public class SceneLoadManager : Singleton<SceneLoadManager>
{
    Coroutine cor = null;
    //0�����ϰ��
    //�� �ʱ�ȭ => ĳ���� ��ġ ����
    //������Ʈ Ǯ (�׾�����������) �������Ұ� �־��ٸ� �������
    //���������� �����ϴ��� ����ߵǸ� ���߱�...

    //===================================================================
    //���Ŵ��� �帧
    //=========================
    //����� ���� �������� �ϴ� ��׶���� �غ���
    //�������� �ҷ������� �ٷ� �������ʵ��� ����.
    //�ε����� �ϴ� �ҷ��� ���� ������ �Ŀ�
    //�������� ����
    //�������� ������ �Ϸ�Ǹ�,
    //�������� Ȱ��ȭ�ϰ�
    //�ε����� ����

    Scene SceneInfo;
    BaseScene NowSceneScript;
    Scene_Load LoadSceneScript;
    bool isLoadsceneLoaded = false;

    //void Start()
    //{
    //    NowSceneScript = GameObject.FindObjectOfType<BaseScene>();
    //}
    
    public void SetLoadInfo(BaseScene _scene, bool Isloadingscene=false)
    {
        this.isLoadsceneLoaded = Isloadingscene;
        if (Isloadingscene)
        {
            LoadSceneScript = _scene as Scene_Load;
        }
        else
        {
            NowSceneScript = _scene;
        }
    }
    public void ChangeScene(AllEnum.SceneKind _nextscene)
    {
        if (cor == null)
        {
            cor = StartCoroutine(LoadCoroutine((int)_nextscene));
        }
        //SceneManager.LoadScene((int)_kind); //�⺻�� LoadSceneMode.Single�Դϴ�. �׳� ���������� �ٷ� �Ѿ��..
    }
    IEnumerator LoadCoroutine(int nextnum)
    {
        SceneInfo = SceneManager.GetActiveScene(); //���� Ȱ��ȭ�� ��

        SceneManager.LoadScene((int)AllEnum.SceneKind.Load, LoadSceneMode.Additive); //���� �ε��� �ε�. �ε����� ������ ������...
        NowSceneScript.ClearScene(); //���� �� ���־��ϴ°� ���� //�ڵ�� ����..
        yield return new WaitUntil(()=> isLoadsceneLoaded); //������ �ε���� start�� �Ҹ������� ���� ��⸦ ��

        SceneManager.UnloadSceneAsync(SceneInfo, UnloadSceneOptions.None);

        AsyncOperation op = SceneManager.LoadSceneAsync(nextnum, LoadSceneMode.Additive); //�ϴ� ������ �θ� �غ� ��..
        op.allowSceneActivation = false; //�������� �ϴ� �񵿱�� �θ�����, �ٷ� Ȱ��ȭ == �ٷ� �������� �ʰ���...
         
        while (!op.isDone )
        {                        
            Debug.Log("op.progress : " + op.progress);
            LoadSceneScript.DrawLoadingBar(op.progress); //�� ���൵��ŭ ��ĥ ����

            yield return null;
            //�������� �ε��� �ø��� 

            if (op.progress >= 0.9f ) //90�۴� ���� �� ���� �̷�� ���뿡 ���� �ε�
            {
                LoadSceneScript.DrawLoadingBar(1f); //������ �� ä�����°� �� ������
                yield return new WaitForSeconds(0.3f);
                op.allowSceneActivation = true; //�ҷ����� �������� Ȱ��ȭ ��Ű�� 
            }
        }
                
        SceneInfo = SceneManager.GetSceneByBuildIndex((int)AllEnum.SceneKind.Load); //�ε��� ����
        SceneManager.UnloadSceneAsync(SceneInfo, UnloadSceneOptions.None); //�ε����� ����
        isLoadsceneLoaded = false; //���� �ε��� �ٽ� �ϱ� ���� ����� �ε����� ���ÿ� bool�� false ����
        cor = null; //�ε��� �ڷ�ƾ �ٽ� �ϱ����ؼ� �������� null���·� ����

        NowSceneScript.Init();
    }
}
