using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage1Manager : MonoBehaviour
{
    [SerializeField] private GameObject GameManager;
    [SerializeField] private Button StagePauseButton, PauseOffButton, StageContinueButton, StageExitButton, StageRestartButton;
    public int ResultCount;
    private void Awake()
    {
        GameManager = GameObject.Find("GameManager");
        StagePauseButton.onClick.AddListener(()=> ClickStagePauseButton());
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
    private void ClickStagePauseButton()
    {

    }
    private void ClickPauseOffButton()
    {

    }
    private void ClickStageContinueButton()
    {

    }
    private void ClickStageExitButton()
    {

    }
    private void ClickStageRestartButton()
    {

    }
}
