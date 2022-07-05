using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Stage3Manager : Stage1Manager
{
    public static Stage3Manager Instance { get; set; }
    [Header("현재 준 성냥의 개수")]
    [SerializeField] private float GiveMatchesCount;
    [Header("등장한 손님 리스트")]
    [SerializeField] private List<GameObject> Customers = new List<GameObject>();
    [Header("손님 프리펩")]
    [SerializeField] private GameObject[] CustomersObj;
    [Header("말풍선 오브젝트")]
    public GameObject[] MatchesCountObj; //말풍선 오브젝트
    [Header("손님 손 오브젝트")]
    public GameObject HandObj; //손 오브젝트
    [Header("완료 버튼")]
    [SerializeField] private Button GiveCompleteButton;
    public int MaxMatchesCount, RoundCount, FailCount, SuccesCount; //줘야하는 성냥 개수, 현재 라운드, 실패 카운트, 성공 카운트
    public bool IsFail, IsSuccess, CustomerArrival;

    private void FixedUpdate()
    {
        IsStageClear();
        StartPanelAnims();
    }

    /// <summary>
    /// GameOver 연출 함수
    /// </summary>
    /// <param name="FaidTime"></param>
    /// <returns></returns>
    private IEnumerator GameOver(float FaidTime)
    {
        GameEnd = true;
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
        SceneManager.LoadScene("Stage3");
    }

    /// <summary>
    /// 스테이지 시작 세팅 함수
    /// </summary>
    protected override void StartSetting()
    {
        base.StartSetting();
        GiveCompleteButton.onClick.AddListener(() => Discrimination());
        Instance = this;
        CustomerCommingEvent();
    }

    private void CustomerCommingEvent()
    {
        if(RoundCount < 4)
        {
            int CustomersRandCount = Random.Range(0, 3);
            MaxMatchesCount = Random.Range(1, 6);
            RoundCount++;
            Customers.Add(Instantiate(CustomersObj[CustomersRandCount], new Vector3(-12.9f, -2.12f, 0), CustomersObj[CustomersRandCount].transform.rotation));
        }
    }

    private void IsStageClear() //게임 클리어, 오버 연출 넣기
    {
        ProgressImage.fillAmount = ResultCount / 2;
    }

    private void Discrimination() //정 / 오답 판별
    {
        if (CustomerArrival)
        {
            if (MaxMatchesCount == GiveMatchesCount)
            {
                IsSuccess = true;
                SuccesCount++;
            }
            else
            {
                IsFail = true;
                FailCount++;
            }
            GiveMatchesCount = 0;
            StartCoroutine(NextCustomerComming());
        }
    }
    private IEnumerator NextCustomerComming()
    {
        float a = 0;
        while(a < 7)
        {
            MatchesCountObj[MaxMatchesCount - 1].transform.DOScale(0, 0.5f);
            HandObj.transform.position = Vector3.Lerp(HandObj.transform.position, new Vector3(5, 7, 0), 0.05f);
            a += Time.deltaTime;
            yield return null;
        }
        CustomerCommingEvent();
    }
    /// <summary>
    /// 스테이지 버튼 모음
    /// </summary>
    protected override void ClickStageExitButton() => SceneMove(1);
    protected override void ClickStageRestartButton() => SceneMove(4);
    protected override void GoToNextStage() => SceneMove(5);
}
