using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Manager : Stage1Manager
{
    private static Stage5Manager instance = null;

    public static Stage5Manager Instance 
    {
        get 
        {
            if (null == instance)
            {
                return null;
            }
            else 
            {
                return instance;
            }
        }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void FixedUpdate()
    {
        IsStageClear();
        StartPanelAnims();
        IsCorrectAnswer();
    }
    private void IsStageClear()
    {
        if (ResultCount == 5)
        {
            GameEnd = true;
            ResultCount++;
            StartCoroutine(StageClear(2));
        }
    }
    private void IsCorrectAnswer()
    {
       
    }
    protected override void ClickStageRestartButton() => SceneMove(6);
    protected override void GoToNextStage() => SceneMove(7);
}
