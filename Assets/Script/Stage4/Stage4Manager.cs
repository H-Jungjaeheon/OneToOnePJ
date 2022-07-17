using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CorrectAnswer
{
    MaxCorrectAnswer = 2
}

public enum Character
{
    Hansel,
    Gretel
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

    [SerializeField] private int correctAnswerCount;
    [SerializeField] private GameObject[] characterObjs;
    public GameObject[] SpeechBubbleObjs = new GameObject[6];
    public GameObject[] CookieObjs = new GameObject[6];
    public int CorrectAnswerCount
    {
        get { return correctAnswerCount; }
        set { if(correctAnswerCount < 3) correctAnswerCount = value; }
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
        if (ResultCount == 3)
        {
            GameEnd = true;
            ResultCount++;
            StartCoroutine(StageClear(2));
        }
    }
    private void IsCorrectAnswer()
    {
        if ((int)CorrectAnswer.MaxCorrectAnswer == correctAnswerCount)
        {
            correctAnswerCount = 0;
            ResultCount++;
            for (int NowCharacterIndex = (int)Character.Hansel; NowCharacterIndex <= (int)Character.Gretel; NowCharacterIndex++) 
            {
                characterObjs[NowCharacterIndex].GetComponent<DiscriminantObject>().NextQuestion();
            }
        }
    }
}
