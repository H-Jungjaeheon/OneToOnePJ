using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Stage2Manager : Stage1Manager
{
    [SerializeField] private float MaxResultCount;
    [SerializeField] private int PHp;
    public int Hp { get; set; } = 3;
    private void FixedUpdate()
    {
        IsStageClear();
        PHp = Hp;
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
