using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int CorrectAnswerCount
    {
        get { return correctAnswerCount; }
        set { if (correctAnswerCount < 3) correctAnswerCount = value; }
    }

    [SerializeField] private GameObject[] characterObjs;
    public GameObject[] SpeechBubbleObjs = new GameObject[6];
    public GameObject[] CookieObjs = new GameObject[6];
    public Image[] ClearImage = new Image[3]; 
    public List<GameObject> cookieDialog = new List<GameObject>();


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
        int ListIndexZero = 0;
        if ((int)CorrectAnswer.MaxCorrectAnswer == correctAnswerCount)
        {
            correctAnswerCount = 0;
            ResultCount++;
            ChangeClearImage();
            for (int NowCharacterIndex = (int)Character.Hansel; NowCharacterIndex <= (int)Character.Gretel; NowCharacterIndex++) 
            {
                characterObjs[NowCharacterIndex].GetComponent<DiscriminantObject>().NextQuestion();
                cookieDialog[ListIndexZero].GetComponent<MovingObjs>().CookieDialogAnim(false);
                cookieDialog.RemoveAt(ListIndexZero);
            }
        }
    }
    private void ChangeClearImage() 
    {
        ClearImage[(int)ResultCount - 1].GetComponent<Image>().color = new Color(0, 1, 0, 1);
    }
}
