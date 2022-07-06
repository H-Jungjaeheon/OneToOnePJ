using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Stage3Manager : Stage1Manager
{
    public static Stage3Manager Instance { get; set; }
    [Header("���� �� ������ ����")]
    [SerializeField] private float GiveMatchesCount;
    [Header("�մ� ������")]
    [SerializeField] private GameObject[] CustomersObj;
    [Header("��ǳ�� ������Ʈ")]
    public GameObject[] MatchesCountObj; //��ǳ�� ������Ʈ
    [Header("�մ� �� ������Ʈ")]
    public GameObject HandObj; //�� ������Ʈ
    [Header("�巡�� ����, ���� �ڽ� ������Ʈ")]
    public GameObject GiveMatchObj;
    [SerializeField] private GameObject MatchBox;
    [Header("ī�޶� ���� ����")]
    [SerializeField] private Camera Cam;
    [SerializeField] private Vector3 NowMousePos;
    [Header("�Ϸ� ��ư")]
    [SerializeField] private Button GiveCompleteButton;
    public int MaxMatchesCount, RoundCount, FailCount, SuccesCount; //����ϴ� ���� ����, ���� ����, ���� ī��Ʈ, ���� ī��Ʈ
    [HideInInspector]
    public bool IsFail, IsSuccess, CustomerArrival, IsTake, IsHandIn, IsMouseDown;

    private void FixedUpdate()
    {
        IsStageClear();
        StartPanelAnims();
        MouseRayCast();
        MatchMove();
    }
    public void MatchesCountPlus() => GiveMatchesCount++;
    private void MatchMove()
    {
        if (IsMouseDown)
        {
            NowMousePos = Input.mousePosition;
            NowMousePos = Cam.ScreenToWorldPoint(NowMousePos) + new Vector3(0, 0, 10);
            GiveMatchObj.transform.position = NowMousePos;
        }
    }
    public void HandAnimStart() => StartCoroutine(HandObjAnim());
    private IEnumerator HandObjAnim()
    {
        while (HandObj.transform.localScale.x >= 1)
        {
            HandObj.transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, 0);
            yield return null;
        }
        HandObj.transform.localScale = new Vector3(1, 1, 1);
        while (HandObj.transform.localScale.x <= 1.1f)
        {
            HandObj.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, 0);
            yield return null;
        }
        HandObj.transform.localScale = new Vector3(1.1f, 1.1f, 1);
    }
    private IEnumerator MatchBoxAnim()
    {
        while(MatchBox.transform.localScale.x >= 1)
        {
            MatchBox.transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime,0);
            yield return null;
        }
        MatchBox.transform.localScale = new Vector3(1, 1, 1);
        while (MatchBox.transform.localScale.x <= 1.05f)
        {
            MatchBox.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, 0);
            yield return null;
        }
        MatchBox.transform.localScale = new Vector3(1.05f, 1.05f, 1);
    }
    /// <summary>
    /// GameOver ���� �Լ�
    /// </summary>
    /// <param name="FaidTime"></param>
    /// <returns></returns>
    private IEnumerator GameOver(float FaidTime)
    {
        GameEnd = true;
        WaitForSeconds WFS = new WaitForSeconds(1.5f);
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
        SceneManager.LoadScene("Stage3");
    }
    private void MouseRayCast()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Matchs"))
            {
                if(!IsMouseDown)
                   StartCoroutine(MatchBoxAnim());
                GiveMatchObj.SetActive(true);
                IsMouseDown = true;
            }
        }
        else if(!Input.GetMouseButton(0) && IsMouseDown)
        {
            IsMouseDown = false;
            StartCoroutine(ReturnStartPosition());
        }
    }
    private IEnumerator ReturnStartPosition()
    { 
        IsHandIn = true;
        yield return new WaitForSeconds(0.1f);
        IsHandIn = false;
        GiveMatchObj.transform.position = new Vector3(0, 0, 0);
        GiveMatchObj.SetActive(false);
    }
    /// <summary>
    /// �������� ���� ���� �Լ�
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
        if (RoundCount < 5 && !GameEnd)
        {
            int CustomersRandCount = Random.Range(0, 3);
            MaxMatchesCount = Random.Range(1, 6);
            RoundCount++;
            Instantiate(CustomersObj[CustomersRandCount], new Vector3(-12.9f, -2.12f, 0), CustomersObj[CustomersRandCount].transform.rotation);
        }
    }

    private void IsStageClear() //���� Ŭ����, ���� ���� �ֱ�
    {
        ProgressImage.fillAmount = ResultCount / 2;
        if (FailCount == 3)
        {
            GameEnd = true;
            FailCount++;
            StartCoroutine(GameOver(3));
        }
        else if (ResultCount == 2)
        {
            GameEnd = true;
            ResultCount++;
            StartCoroutine(StageClear(2));
        }
    }

    private void Discrimination() //�� / ���� �Ǻ�
    {
        IsTake = false;
        if (CustomerArrival && !GameEnd)
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
            HandObj.transform.position = Vector3.Lerp(HandObj.transform.position, new Vector3(5, 7.22f, 0), 0.05f);
            a += Time.deltaTime;
            yield return null;
        }
        CustomerCommingEvent();
    }
    /// <summary>
    /// �������� ��ư ����
    /// </summary>
    protected override void ClickStageExitButton() => SceneMove(1);
    protected override void ClickStageRestartButton() => SceneMove(4);
    protected override void GoToNextStage() => SceneMove(5);
}
