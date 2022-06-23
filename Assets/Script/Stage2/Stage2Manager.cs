using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

class Stage2Manager : Stage1Manager
{
    public static Stage2Manager Instance { get; set; }

    #region 물고기 생성 변수
    [Header("물고기 생성 현재시간, 최대 생성 시간 변수")]
    [SerializeField] private float SpawnTimer;
    [SerializeField] private float MaxSpawnTime;
    #endregion

    #region 오브젝트들 모음
    [Header("체력 오브젝트")]
    [SerializeField] private GameObject[] HpObj;
    [Header("물고기 생성위치, 물고기 종류 오브젝트")]
    [SerializeField] private GameObject[] FishSpawnPoint, Fishs;
    #endregion

    #region 게임 진행도 변수
    [Header("최대 게임 진행도")]
    public float MaxResultCount;
    #endregion

    #region 체력 변수들 모음
    [Header("플레이어 체력")]
    public int Hp;
    [Header("플레이어 체력 애니메이터")]
    public Animator[] animator = new Animator[3];
    private RectTransform[] HeartRT = new RectTransform[3];
    [Header("하트 크기 상태 판별 변수")]
    [SerializeField] private bool IsBigging;
    #endregion

    private void FixedUpdate()
    {
        StageInformation();
        FishSpawn();
        IsEnd();
        HeartAnim();
    }

    /// <summary>
    /// 게임 진행도 함수
    /// </summary>
    private void StageInformation()
    {
        ProgressImage.fillAmount = ResultCount / MaxResultCount;
        if(IsStart)
           ResultCount += Time.deltaTime;
    }

    /// <summary>
    /// 하트 애니메이션 함수
    /// </summary>
    private void HeartAnim()
    {
        if (GameEnd)
        {
            for (int HeartObjIndex = 0; HeartObjIndex < 3; HeartObjIndex++)
            {
                if (IsBigging)
                {
                    HeartRT[HeartObjIndex].localScale += new Vector3(Time.deltaTime / 15, Time.deltaTime / 15, 0);
                    if (HeartRT[HeartObjIndex].localScale.x >= 0.75f)
                        IsBigging = false;
                }
                else
                {
                    HeartRT[HeartObjIndex].localScale -= new Vector3(Time.deltaTime / 15, Time.deltaTime / 15, 0);
                    if (HeartRT[HeartObjIndex].localScale.x <= 0.65f)
                        IsBigging = true;
                }
            }
        }
    }

   
    /// <summary>
    /// 게임 종료(GameOver & GameClear) 판별 함수
    /// </summary>
    private void IsEnd()
    {
        if(Hp <= 0 && !GameEnd)
        {
            GameEnd = true;
            StartCoroutine(GameOver(3));
        }
        else if(ResultCount >= MaxResultCount && !GameEnd)
        {
            GameEnd = true;
            StartCoroutine(StageClear(4f));
        }
    }

    /// <summary>
    /// 물고기(장애물) 생성 함수
    /// </summary>
    private void FishSpawn()
    {
        if (IsStart && !GameEnd)
        {
            SpawnTimer += Time.deltaTime;
            if(SpawnTimer >= MaxSpawnTime)
            {
                MaxSpawnTime = Random.Range(5, 8);
                int FishIndex = Random.Range(0, 4);
                int SpawnIndex = Random.Range(0, 3);
                SpawnTimer = 0;
                Instantiate(Fishs[FishIndex], FishSpawnPoint[SpawnIndex].transform.position, Fishs[FishIndex].transform.rotation);
            }
        }
    }

    /// <summary>
    /// GameOver 연출 함수
    /// </summary>
    /// <param name="FaidTime"></param>
    /// <returns></returns>
    private IEnumerator GameOver(float FaidTime)
    {
        WaitForSeconds WFS = new WaitForSeconds(0.5f);
        float NowFaidTime = 0;
        yield return WFS;
        Color color = StartFaidBackGround.color;
        GameStartPanelObj.SetActive(true);
        while (NowFaidTime < FaidTime)
        {
            color.a = NowFaidTime;
            StartFaidBackGround.color = color;
            NowFaidTime += Time.deltaTime;
            yield return null;
        }
        yield return WFS;
        SceneManager.LoadScene(3);
    }

    /// <summary>
    /// 스테이지 시작 세팅 함수
    /// </summary>
    protected override void StartSetting()
    {
        base.StartSetting();
        Instance = this;
        for (int HeartObjIndex = 0; HeartObjIndex < 3; HeartObjIndex++)
        {
            HeartRT[HeartObjIndex] = HpObj[HeartObjIndex].GetComponent<RectTransform>();
            animator[HeartObjIndex] = HpObj[HeartObjIndex].GetComponent<Animator>();
        }
        MaxSpawnTime = Random.Range(4, 7);
    }

    /// <summary>
    /// 스테이지 버튼 모음
    /// </summary>
    protected override void ClickStageExitButton() => SceneManager.LoadScene(1);
    protected override void ClickStageRestartButton() => SceneManager.LoadScene(3);
    protected override void GoToNextStage() => SceneManager.LoadScene(4);
}
