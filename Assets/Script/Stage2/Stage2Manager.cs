using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

class Stage2Manager : Stage1Manager
{
    public static Stage2Manager Instance { get; set; }
    [SerializeField] private float MaxResultCount, SpawnTimer, MaxSpawnTime;
    [SerializeField] private GameObject[] HpObj, FishSpawnPoint, Fishs;
    public Animator[] animator;
    public int Hp;
    public bool GameEnd;
  
    private void FixedUpdate()
    {
        IsStageClear();
        FishSpawn();
        IsEnd();
    }
    private void IsEnd()
    {
        if(Hp <= 0 && !GameEnd)
        {
            GameEnd = true;
            StartCoroutine(GameOver(3));
        }
    }
    protected override void Start()
    {
        base.Start();
        for (int a = 0; a < 3; a++)
        {
            animator[a] = HpObj[a].GetComponent<Animator>();
        }
        MaxSpawnTime = Random.Range(4, 7);
    }
    private void FishSpawn()
    {
        if (IsStart && !GameClear)
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
    protected override void StartSetting()
    {
        base.StartSetting();
        Instance = this;
        Hp = 3;
    }
    private void IsStageClear()
    {
        ProgressImage.fillAmount = ResultCount / MaxResultCount;
        if(IsStart)
           ResultCount += Time.deltaTime;
        if (ResultCount >= MaxResultCount && !GameClear)
        {
            GameClear = true;
            ResultCount++;
            GameManager.Instance.StageClearCount++;
            StartCoroutine(StageClear());
        }
    }
    protected override IEnumerator StageClear()
    {
        yield return new WaitForSeconds(5);
        yield return base.StageClear();
    }
    #region 스테이지 버튼 모음
    protected override void ClickStageExitButton() => SceneManager.LoadScene(1);
    protected override void ClickStageRestartButton() => SceneManager.LoadScene(3);
    protected override void GoToNextStage() => SceneManager.LoadScene(4);
    #endregion
}
