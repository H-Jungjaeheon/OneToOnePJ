using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Stage1Manager : MonoBehaviour
{
    [SerializeField] private GameObject GameManager, GamePauseObj, GameStartPanelObj, GameHelpObj;
    [SerializeField] private Button StagePauseButton, StageContinueButton, StageExitButton, StageRestartButton, GameHelpButton;
    [SerializeField] private bool StageStarting;
    public int ResultCount;
    
    private void Awake()
    {
        GamePauseObj.SetActive(false);
        GameManager = GameObject.Find("GameManager");
        StagePauseButton.onClick.AddListener(()=> ClickStagePauseButton());
        StageContinueButton.onClick.AddListener(() => ClickStageContinueButton());
        StageExitButton.onClick.AddListener(() => ClickStageExitButton());
        StageRestartButton.onClick.AddListener(() => ClickStageRestartButton());
    }
    private void FixedUpdate()
    {
        IsStageClear();
    }
    private void IsStageClear()
    {
        if(ResultCount >= 7)
        {
            //클리어 연출 출력(미개발)
            GameManager.GetComponent<GameManager>().StageClearCount++;
            SceneManager.LoadScene(1);
        }
    }
    #region 스테이지 버튼 모음
    private void ClickStagePauseButton() => GamePauseObj.SetActive(true);
    private void ClickStageContinueButton() => GamePauseObj.SetActive(false);
    private void ClickStageExitButton() => SceneManager.LoadScene(1);
    private void ClickStageRestartButton() => SceneManager.LoadScene(2);
    #endregion
}
