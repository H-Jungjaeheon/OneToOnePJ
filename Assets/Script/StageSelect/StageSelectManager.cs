using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageSelectManager : MonoBehaviour
{
    #region 스테이지 선택 씬 버튼 및 오브젝트 모음
    [Header("버튼 모음")]
    [SerializeField] private Button StagePassButtonLeft;
    [SerializeField] private Button StagePassButtonRight, SettingButton, SettingCloseButton, ReturnTitleButton;
    [SerializeField] private Button[] StageLockButton, GoStageButton;
    [Header("오브젝트 모음")]
    [SerializeField] private GameObject ReturnTitleObj, GameSettingPopUp;
    [SerializeField] private GameObject[] BookObjs, StageLockObj;
    [Header("씬 이동 연출 이미지")]
    [SerializeField] private Image ReturnToTitleImage;
    [Header("스테이지 잠금 연출 오브젝트")]
    [SerializeField] private Image StageLockImage;
    [SerializeField] private Text StageLockText;
    [Header("현재 스테이지 선택 인덱스")]
    [SerializeField] private int StageSelectCount;
    [SerializeField] private bool IsSettingUp, IsLocking;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartSetting();
    }

    void Update()
    {
        BookAnim();
    }

    private void BookAnim()
    {
        for (int a = 0; a < 6; a++)
        {
            BookObjs[a].transform.position = new Vector3(BookObjs[a].transform.position.x, 540 + Mathf.Cos(Time.time * 1.6f) * 10, 0);
        }
    }

    private void StartSetting()
    {
        int StageLockCount = 0;
        StageSelectCount = 0;
        ReturnTitleObj.SetActive(false);
        SettingButton.onClick.AddListener(SettingPopUp);
        SettingCloseButton.onClick.AddListener(SettingClose);
        ReturnTitleButton.onClick.AddListener(() => StartCoroutine(ReturnToTitle(1f)));
        StagePassButtonLeft.onClick.AddListener(StagePassL);
        StagePassButtonRight.onClick.AddListener(StagePassR);
        for (int a = 0; a < 5; a++)
        {
            StageLockButton[a].onClick.AddListener(() => StartCoroutine(StageLock(1f)));
        }
        for (int a = 0; a < 6; a++)
        {
            int StageIndex = a;
            GoStageButton[a].onClick.AddListener(() => StageButtonSetting(StageIndex + 1));
        }
        while (true)
        {
            StageLockObj[StageLockCount].SetActive(false);
            if (StageLockCount < GameManager.Instance.StageClearCount)
                StageLockCount++;
            else
                break;
        }
    }
    private void StageButtonSetting(int StageIndex) => SceneManager.LoadScene($"Stage{StageIndex}");
    private IEnumerator StageLock(float FaidTime)
    {
        if (IsLocking == false)
        {
            IsLocking = true;
            float NowFaidTime = FaidTime;
            Color color = StageLockImage.color;
            Color color2 = StageLockText.color;
            color.a = 1;
            color2.a = 1;
            StageLockImage.color = color;
            StageLockText.color = color2;
            yield return new WaitForSeconds(1);
            while (NowFaidTime >= 0)
            {
                NowFaidTime -= Time.deltaTime;
                color.a = NowFaidTime;
                color2.a = NowFaidTime;
                StageLockImage.color = color;
                StageLockText.color = color2;
                yield return null;
            }
            IsLocking = false;
        }
    }
    private void SettingPopUp()
    {
        if (IsSettingUp == false)
        {
            GameSettingPopUp.transform.DOMove(new Vector3(1300, 540, 0), 1.2f).SetEase(Ease.InOutBack);
            IsSettingUp = true;
        }
    }
    private void SettingClose()
    {
        if (IsSettingUp == true)
        {
            GameSettingPopUp.transform.DOMove(new Vector3(2700, 540, 0), 1.2f).SetEase(Ease.InOutBack);
            IsSettingUp = false;
        }
    }
    private IEnumerator ReturnToTitle(float FaidTime)
    {
        if (IsSettingUp == false)
        {
            ReturnTitleObj.SetActive(true);
            Color color = ReturnToTitleImage.color;
            float NowFaidTime = 0;
            while (NowFaidTime <= FaidTime)
            {
                color.a = NowFaidTime;
                ReturnToTitleImage.color = color;
                NowFaidTime += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(0);
        }
    }
    private void StagePassL()
    {
        if ((StageSelectCount - 1) >= 0 && IsSettingUp == false)
        {
            StageSelectCount--;
            BookObjs[StageSelectCount].transform.DOMove(new Vector3(960,540,0), 1).SetEase(Ease.InBack);
            BookObjs[StageSelectCount + 1].transform.DOMove(new Vector3(2890,540,0), 1).SetEase(Ease.InBack);
        }
    }
    private void StagePassR()
    { 
        if ((StageSelectCount + 1) <= 5 && IsSettingUp == false)
        {
            StageSelectCount++;
            BookObjs[StageSelectCount].transform.DOMove(new Vector3(960, 540, 0), 1).SetEase(Ease.InBack);
            BookObjs[StageSelectCount - 1].transform.DOMove(new Vector3(-970, 540, 0), 1).SetEase(Ease.InBack);
        }
    }
}
