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
    [SerializeField] private Sprite ClearedGoStageButtonImage, StageButtonHLImage, StageButtonPSImage;
    [Header("오브젝트 모음")]
    [SerializeField] private GameObject ReturnTitleObj, GameSettingPopUp;
    [SerializeField] private GameObject[] BookObjs, StageLockObj, PositionObj;
    [SerializeField] private Sprite[] ChangeBookObjsImage;
    [Header("씬 이동 연출 이미지")]
    [SerializeField] private Image ReturnToTitleImage;
    [Header("스테이지 잠금 연출 오브젝트")]
    [SerializeField] private Image StageLockImage;
    [SerializeField] private Text StageLockText;
    [Header("현재 스테이지 선택 인덱스")]
    [SerializeField] private int StageSelectCount;
    [SerializeField] private bool IsSettingUp, IsLocking, IsStarting;
    #endregion

    [Header("사운드 모음")]
    [Space(20)]
    [SerializeField] private AudioClip BasicButtonClickClip;
    [SerializeField] private AudioClip LockButtonClickClip;

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
            BookObjs[a].transform.position = new Vector3(BookObjs[a].transform.position.x, BookObjs[a].transform.position.y + Mathf.Sin(Time.time * 1f) * 0.001f, 0);
        }
    }

    private void StartSetting()
    {
        SpriteState SS = new SpriteState();
        SS.highlightedSprite = StageButtonHLImage;
        SS.pressedSprite = StageButtonPSImage;
        int StageLockCount = 0;
        IsStarting = false;
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
            GoStageButton[a].onClick.AddListener(() => StartCoroutine(GameStartFaid(1, StageIndex + 1)));
            if (GameManager.Instance.SD[a].IsClear)
            {
                Image NewBookImage = BookObjs[a].transform.Find($"Stage{a+1}Book").gameObject.GetComponent<Image>();
                NewBookImage.sprite = ChangeBookObjsImage[a];
                GoStageButton[a].GetComponent<Image>().sprite = ClearedGoStageButtonImage;
                GoStageButton[a].GetComponent<Button>().spriteState = SS;
            }
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
    private IEnumerator GameStartFaid(float FaidTime, int StageIndex)
    {
        BasicButtonClickSound();
        if (IsStarting == false)
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
            SceneManager.LoadScene($"Stage{StageIndex}");
        }
    }
    private IEnumerator StageLock(float LockTime)
    {
        LockButtonClickSound();
        if (IsLocking == false)
        {
            IsLocking = true;
            float NowLockTime = LockTime;
            Color color = StageLockImage.color;
            Color color2 = StageLockText.color;
            color.a = 1;
            color2.a = 1;
            StageLockImage.color = color;
            StageLockText.color = color2;
            yield return new WaitForSeconds(1);
            while (NowLockTime >= 0)
            {
                NowLockTime -= Time.deltaTime;
                color.a = NowLockTime;
                color2.a = NowLockTime;
                StageLockImage.color = color;
                StageLockText.color = color2;
                yield return null;
            }
            IsLocking = false;
        }
    }
    private void SettingPopUp()
    {
        BasicButtonClickSound();
        if (IsSettingUp == false)
        {
            //GameSettingPopUp.transform.DOMove(SettingPositionObj[0].transform.position, 0.7f).SetEase(Ease.InFlash);
            IsSettingUp = true;
        }
    }
    private void SettingClose()
    {
        BasicButtonClickSound();
        if (IsSettingUp == true)
        {
            //GameSettingPopUp.transform.DOMove(SettingPositionObj[1].transform.position, 0.7f).SetEase(Ease.InFlash);
            IsSettingUp = false;
        }
    }
    private IEnumerator ReturnToTitle(float FaidTime)
    {
        BasicButtonClickSound();
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
        BasicButtonClickSound();
        if ((StageSelectCount - 1) >= 0 && IsSettingUp == false)
        {
            StageSelectCount--;
            BookObjs[StageSelectCount].transform.DOMove(PositionObj[1].transform.position, 1).SetEase(Ease.InBack);
            BookObjs[StageSelectCount + 1].transform.DOMove(PositionObj[2].transform.position, 1).SetEase(Ease.InBack);
        }
    }
    private void StagePassR()
    {
        BasicButtonClickSound();
        if ((StageSelectCount + 1) <= 5 && IsSettingUp == false)
        {
            StageSelectCount++;
            BookObjs[StageSelectCount].transform.DOMove(PositionObj[1].transform.position, 1).SetEase(Ease.InBack);
            BookObjs[StageSelectCount - 1].transform.DOMove(PositionObj[0].transform.position, 1).SetEase(Ease.InBack);
        }
    }
    private void BasicButtonClickSound() => SoundManager.Instance.SFXPlay("BasicButtonClick", BasicButtonClickClip);
    private void LockButtonClickSound() => SoundManager.Instance.SFXPlay("LockButtonClick", LockButtonClickClip);
}
