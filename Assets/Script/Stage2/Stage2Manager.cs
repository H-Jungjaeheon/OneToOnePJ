using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

class Stage2Manager : Stage1Manager
{
    public static Stage2Manager Instance { get; set; }

    #region ����� ���� ����
    [Header("����� ���� ����ð�, �ִ� ���� �ð� ����")]
    [SerializeField] private float SpawnTimer;
    [SerializeField] private float MaxSpawnTime;
    #endregion

    #region ������Ʈ�� ����
    [Header("ü�� ������Ʈ")]
    [SerializeField] private GameObject[] HpObj;
    [Header("����� ������ġ, ����� ���� ������Ʈ")]
    [SerializeField] private GameObject[] FishSpawnPoint, Fishs;
    #endregion

    #region ���� ���൵ ����
    [Header("�ִ� ���� ���൵")]
    public float MaxResultCount;
    #endregion

    #region ü�� ������ ����
    [Header("�÷��̾� ü��")]
    public int Hp;
    [Header("�÷��̾� ü�� �ִϸ�����")]
    public Animator[] animator = new Animator[3];
    private RectTransform[] HeartRT = new RectTransform[3];
    [Header("��Ʈ ũ�� ���� �Ǻ� ����")]
    [SerializeField] private bool IsBigging;
    #endregion

    private void FixedUpdate()
    {
        StageInformation();
        FishSpawn();
        IsEnd();
        HeartAnim();
    }

    /// <summary>
    /// ���� ���൵ �Լ�
    /// </summary>
    private void StageInformation()
    {
        ProgressImage.fillAmount = ResultCount / MaxResultCount;
        if(IsStart)
           ResultCount += Time.deltaTime;
    }

    /// <summary>
    /// ��Ʈ �ִϸ��̼� �Լ�
    /// </summary>
    private void HeartAnim()
    {
        if (GameEnd)
        {
            for (int HeartObjIndex = 0; HeartObjIndex < 3; HeartObjIndex++)
            {
                if (IsBigging)
                {
                    HeartRT[HeartObjIndex].localScale += new Vector3(Time.deltaTime / 15, Time.deltaTime / 15, 0);
                    if (HeartRT[HeartObjIndex].localScale.x >= 0.75f)
                        IsBigging = false;
                }
                else
                {
                    HeartRT[HeartObjIndex].localScale -= new Vector3(Time.deltaTime / 15, Time.deltaTime / 15, 0);
                    if (HeartRT[HeartObjIndex].localScale.x <= 0.65f)
                        IsBigging = true;
                }
            }
        }
    }

   
    /// <summary>
    /// ���� ����(GameOver & GameClear) �Ǻ� �Լ�
    /// </summary>
    private void IsEnd()
    {
        if(Hp <= 0 && !GameEnd)
        {
            GameEnd = true;
            StartCoroutine(GameOver(3));
        }
        else if(ResultCount >= MaxResultCount && !GameEnd)
        {
            GameEnd = true;
            StartCoroutine(StageClear(4f));
        }
    }

    /// <summary>
    /// �����(��ֹ�) ���� �Լ�
    /// </summary>
    private void FishSpawn()
    {
        if (IsStart && !GameEnd)
        {
            SpawnTimer += Time.deltaTime;
            if(SpawnTimer >= MaxSpawnTime)
            {
                MaxSpawnTime = Random.Range(5, 8);
                int FishIndex = Random.Range(0, 4);
                int SpawnIndex = Random.Range(0, 3);
                SpawnTimer = 0;
                Instantiate(Fishs[FishIndex], FishSpawnPoint[SpawnIndex].transform.position, Fishs[FishIndex].transform.rotation);
            }
        }
    }

    /// <summary>
    /// GameOver ���� �Լ�
    /// </summary>
    /// <param name="FaidTime"></param>
    /// <returns></returns>
    private IEnumerator GameOver(float FaidTime)
    {
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
        SceneManager.LoadScene(3);
    }

    /// <summary>
    /// �������� ���� ���� �Լ�
    /// </summary>
    protected override void StartSetting()
    {
        base.StartSetting();
        Instance = this;
        for (int HeartObjIndex = 0; HeartObjIndex < 3; HeartObjIndex++)
        {
            HeartRT[HeartObjIndex] = HpObj[HeartObjIndex].GetComponent<RectTransform>();
            animator[HeartObjIndex] = HpObj[HeartObjIndex].GetComponent<Animator>();
        }
        MaxSpawnTime = Random.Range(4, 7);
    }

    /// <summary>
    /// �������� ��ư ����
    /// </summary>
    protected override void ClickStageExitButton() => SceneManager.LoadScene(1);
    protected override void ClickStageRestartButton() => SceneManager.LoadScene(3);
    protected override void GoToNextStage() => SceneManager.LoadScene(4);
}
