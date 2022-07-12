using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CorrectAnswer
{
    FirstAnswer,
    SecondAnswer
}


public class Stage4Manager : Stage1Manager
{
    private static Stage4Manager instance = null;
    public static Stage4Manager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public bool[] IsCorrectAnswerLR = new bool[2];




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
    private void IsStageClear() //게임 클리어, 오버 연출 넣기
    {
        if (ResultCount == 3)
        {
            GameEnd = true;
            ResultCount++;
            StartCoroutine(StageClear(2));
        }
    }
    private void IsCorrectAnswer()
    {
        if(IsCorrectAnswerLR[(int)CorrectAnswer.FirstAnswer] && IsCorrectAnswerLR[(int)CorrectAnswer.SecondAnswer]) //만약 둘 다 정답이라면
        {
            for(int AnswerIndex = (int)CorrectAnswer.FirstAnswer; AnswerIndex <= (int)CorrectAnswer.SecondAnswer; AnswerIndex++)
            {
                IsCorrectAnswerLR[AnswerIndex] = false;
            }
            ResultCount++;
        }
    }
}
