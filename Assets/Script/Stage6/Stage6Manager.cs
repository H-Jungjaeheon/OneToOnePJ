using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Manager : Stage1Manager
{
    private static Stage6Manager instance = null;

    public static Stage6Manager Instance 
    {
        get 
        {
            if (null == instance)
            {
                return null;
            }
            else
                return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void FixedUpdate()
    {
        IsStageClear();
        StartPanelAnims();
    }
    private void IsStageClear()
    {
        ProgressImage.fillAmount = ResultCount / 3;
        if (ResultCount == 3)
        {
            GameEnd = true;
            ResultCount++;
        }
    }

    protected override void ClickStageRestartButton() => SceneMove(7);
    protected override void GoToNextStage() => SceneMove(7);
}
