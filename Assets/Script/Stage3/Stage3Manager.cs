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
    [Header("������ �մ� ����Ʈ")]
    [SerializeField] private List<GameObject> Customers = new List<GameObject>();
    [Header("�մ� ������")]
    [SerializeField] private GameObject[] CustomersObj;
    [Header("��ǳ�� ������Ʈ")]
    public GameObject[] MatchesCountObj; //��ǳ�� ������Ʈ
    [Header("�մ� �� ������Ʈ")]
    public GameObject HandObj; //�� ������Ʈ
    [Header("�Ϸ� ��ư")]
    [SerializeField] private Button GiveCompleteButton;
    public int MaxMatchesCount, RoundCount, FailCount, SuccesCount; //����ϴ� ���� ����, ���� ����, ���� ī��Ʈ, ���� ī��Ʈ
    public bool IsFail, IsSuccess, CustomerArrival;

    private void FixedUpdate()
    {
        IsStageClear();
        StartPanelAnims();
    }

    /// <summary>
    /// GameOver ���� �Լ�
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
        if(RoundCount < 4)
        {
            int CustomersRandCount = Random.Range(0, 3);
            MaxMatchesCount = Random.Range(1, 6);
            RoundCount++;
            Customers.Add(Instantiate(CustomersObj[CustomersRandCount], new Vector3(-12.9f, -2.12f, 0), CustomersObj[CustomersRandCount].transform.rotation));
        }
    }

    private void IsStageClear() //���� Ŭ����, ���� ���� �ֱ�
    {
        ProgressImage.fillAmount = ResultCount / 2;
    }

    private void Discrimination() //�� / ���� �Ǻ�
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
    /// �������� ��ư ����
    /// </summary>
    protected override void ClickStageExitButton() => SceneMove(1);
    protected override void ClickStageRestartButton() => SceneMove(4);
    protected override void GoToNextStage() => SceneMove(5);
}
