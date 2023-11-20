using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //씬이동을 위해 반드시 있어야하는 using.. 

//씬매니저를 따로 만들던지, 로딩매니저를 따로 만드는 편이긴한데..
public class SceneLoadManager : Singleton<SceneLoadManager>
{
    Coroutine cor = null;
    //0번씬일경우
    //씬 초기화 => 캐릭터 위치 세팅
    //오브젝트 풀 (그씬에서만쓰는) 만들어야할게 있었다면 만들던지
    //이전씬에서 진행하던거 멈춰야되면 멈추기...

    //===================================================================
    //씬매니저 흐름
    //=========================
    //현재씬 에서 다음씬을 일단 백그라운드로 준비함
    //다음씬이 불러와져도 바로 보이지않도록 세팅.
    //로딩씬을 일단 불러서 위에 세팅한 후에
    //기존씬을 없앰
    //다음씬이 세팅이 완료되면,
    //다음씬을 활성화하고
    //로딩씬을 지움

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
        //SceneManager.LoadScene((int)_kind); //기본이 LoadSceneMode.Single입니다. 그냥 다음씬으로 바로 넘어가기..
    }
    IEnumerator LoadCoroutine(int nextnum)
    {
        SceneInfo = SceneManager.GetActiveScene(); //현재 활성화된 씬

        SceneManager.LoadScene((int)AllEnum.SceneKind.Load, LoadSceneMode.Additive); //먼저 로딩씬 로드. 로딩씬은 가볍기 때문에...
        NowSceneScript.ClearScene(); //기존 씬 없애야하는것 실행 //코드상 실행..
        yield return new WaitUntil(()=> isLoadsceneLoaded); //실제로 로드씬의 start가 불릴때까지 내가 대기를 함

        SceneManager.UnloadSceneAsync(SceneInfo, UnloadSceneOptions.None);

        AsyncOperation op = SceneManager.LoadSceneAsync(nextnum, LoadSceneMode.Additive); //일단 다음씬 부를 준비를 함..
        op.allowSceneActivation = false; //다음씬을 일단 비동기로 부르지만, 바로 활성화 == 바로 보여주지 않겠음...
         
        while (!op.isDone )
        {                        
            Debug.Log("op.progress : " + op.progress);
            LoadSceneScript.DrawLoadingBar(op.progress); //내 진행도만큼 색칠 진행

            yield return null;
            //거짓말로 로딩바 올리는 

            if (op.progress >= 0.9f ) //90퍼는 실제 그 씬을 이루는 내용에 대한 로딩
            {
                LoadSceneScript.DrawLoadingBar(1f); //강제로 다 채워지는걸 함 보여줌
                yield return new WaitForSeconds(0.3f);
                op.allowSceneActivation = true; //불러와진 다음씬을 활성화 시키고 
            }
        }
                
        SceneInfo = SceneManager.GetSceneByBuildIndex((int)AllEnum.SceneKind.Load); //로딩씬 정보
        SceneManager.UnloadSceneAsync(SceneInfo, UnloadSceneOptions.None); //로딩씬을 해제
        isLoadsceneLoaded = false; //다음 로딩을 다시 하기 위해 사라진 로딩씬과 동시에 bool도 false 세팅
        cor = null; //로딩씬 코루틴 다시 하기위해서 끝났으면 null상태로 만듦

        NowSceneScript.Init();
    }
}
