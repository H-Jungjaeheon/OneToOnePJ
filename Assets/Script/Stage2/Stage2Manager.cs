using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

class Stage2Manager : Stage1Manager
{
    public static Stage2Manager Instance { get; set; }
    [SerializeField] private float MaxResultCount;
    [SerializeField] private GameObject[] HpObj;
    public Animator[] animator;
    public int Hp;
  
    private void FixedUpdate()
    {
        IsStageClear();
    }
    protected override void Start()
    {
        base.Start();
        for (int a = 0; a < 3; a++)
        {
            animator[a] = HpObj[a].GetComponent<Animator>();
        }
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
        if (ResultCount >= MaxResultCount && GameClear == false)
        {
            GameClear = true;
            ResultCount++;
            GameManager.Instance.StageClearCount++;
            StartCoroutine(StageClear());
        }
    }
    #region 스테이지 버튼 모음
    protected override void ClickStageExitButton() => SceneManager.LoadScene(1);
    protected override void ClickStageRestartButton() => SceneManager.LoadScene(3);
    protected override void GoToNextStage() => SceneManager.LoadScene(4);
    #endregion
}
