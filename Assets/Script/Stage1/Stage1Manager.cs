using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Stage1Manager : MonoBehaviour
{
    [Header("게임 오브젝트 모음")]
    [SerializeField] protected GameObject GamePauseObj; //일시정지 오브젝트
    [SerializeField] protected GameObject GameStartPanelObj, StartPanelObj, StartPanelPos, // 게임시작 연출 전체 오브젝트, 게임시작 연출 간판 오브젝트, 간판 오브젝트 위치 오브젝트
    SameBlackBG, GameHelpObj, StartAnimSkipButtonObj, StageClearParticleObj, ParticleObjSpawner, // 공용사용(Faid) 연출 오브젝트, 게임 도움말 오브젝트, 스타트 애니메이션 스킵 버튼 오브젝트
    GameClearBalloonObj, GameClearObj;
    [Header("버튼 모음")]
    [SerializeField] protected Button StagePauseButton;
    [SerializeField] protected Button StageContinueButton, StageExitButton,
    StageRestartButton, GameHelpButton, ExitGameHelpButton, StartAnimSkipButton,
    NextStageButton, ReturnStageSelectButton;
    [Header("이미지 모음")]
    [SerializeField] protected Image StartFaidBackGround;
    [SerializeField] protected Image StartPanelImage, ProgressImage;
    [SerializeField] protected Text[] StartText;
    [SerializeField] protected Text SkipText;
    public float ResultCount;
    [SerializeField] protected bool Closing, GameClear, IsStart;
    
    protected virtual void Awake()
    {
        StartSetting();
    }
    protected virtual void Start()
    {
        StartCoroutine(StartAnim(1.5f));
    }
    private void FixedUpdate()
    {
        IsStageClear();
    }
    protected virtual void StartSetting()
    {
        GamePauseObj.SetActive(false);
        GameStartPanelObj.SetActive(true);
        StagePauseButton.onClick.AddListener(() => ClickStagePauseButton());
        StageContinueButton.onClick.AddListener(() => ClickStageContinueButton());
        StageExitButton.onClick.AddListener(() => ClickStageExitButton());
        StageRestartButton.onClick.AddListener(() => ClickStageRestartButton());
        GameHelpButton.onClick.AddListener(()=> ClickStageHelpButton());
        ExitGameHelpButton.onClick.AddListener(() => ClickStageHelpExitButton());
        StartAnimSkipButton.onClick.AddListener(() => ClickStageStartAnimSkipButton());
        NextStageButton.onClick.AddListener(() => GoToNextStage());
        ReturnStageSelectButton.onClick.AddListener(() => ClickStageExitButton());
        if (GameManager.Instance.IsSkipAble[0]) StartAnimSkipButtonObj.SetActive(true);
        else StartAnimSkipButtonObj.SetActive(false);
    }
    #region 스테이지 클리어 함수
    private void IsStageClear()
    {
        ProgressImage.fillAmount = ResultCount / 7;
        if (ResultCount == 7)
        {
            GameClear = true;
            ResultCount++;
            GameManager.Instance.StageClearCount++;
            StartCoroutine(StageClear());
        }
    }
    protected virtual IEnumerator StageClear()
    {
        Instantiate(StageClearParticleObj, ParticleObjSpawner.transform.position + new Vector3(0,0,-90), StageClearParticleObj.transform.rotation);
        GameClearBalloonObj.SetActive(true);
        yield return new WaitForSeconds(7f);
        SameBlackBG.SetActive(true);
        GameClearObj.transform.DOScale(1, 0.8f).SetEase(Ease.InCubic);
    }
    #endregion
    #region 시작 애니메이션
    protected virtual IEnumerator StartAnim(float FaidTime)
    {
        float NowFaidTime = FaidTime;
        StartPanelObj.transform.DOMove(StartPanelPos.transform.position, 1.5f);
        yield return new WaitForSeconds(1.8f);
        Color color = StartFaidBackGround.color;
        Color color2 = StartPanelImage.color;
        Color color3 = StartAnimSkipButton.GetComponent<Image>().color;
        while (NowFaidTime >= 0)
        {
            color.a = NowFaidTime;
            color2.a = NowFaidTime;
            color3.a = NowFaidTime;
            StartFaidBackGround.color = color;
            StartPanelImage.color = color2;
            StartAnimSkipButton.GetComponent<Image>().color = color3;
            for (int a = 0; a < StartText.Length; a++){
                StartText[a].color = color;
                SkipText.color = color;
            }
            NowFaidTime -= Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.IsSkipAble[0] = true;
        GameStartPanelObj.SetActive(false);
        IsStart = true;
    }
    #endregion

    #region 스테이지 버튼 모음
    protected virtual void ClickStagePauseButton()
    {
        if(GameClear == false)
        {
            GamePauseObj.SetActive(true);
            SameBlackBG.SetActive(true);
        }
    }
    protected virtual void ClickStageContinueButton()
    {
        GamePauseObj.SetActive(false);
        SameBlackBG.SetActive(false);
    }
    protected virtual void ClickStageHelpButton()
    {
        if (Closing == false && GameClear == false)
        {
            SameBlackBG.SetActive(true);
            GameHelpObj.transform.DOMove(new Vector3(0, GameHelpObj.transform.position.y, GameHelpObj.transform.position.z), 1.2f).SetEase(Ease.InOutBack);
            Closing = true;
        }
    }
    protected virtual void ClickStageHelpExitButton()
    {
        if(Closing == true)
        {
            SameBlackBG.SetActive(false);
            GameHelpObj.transform.DOMove(new Vector3(15, GameHelpObj.transform.position.y, GameHelpObj.transform.position.z), 1.2f).SetEase(Ease.InOutBack);
            Closing = false;
        }
    }
    protected virtual void ClickStageStartAnimSkipButton()
    {
        StopCoroutine(StartAnim(0));
        GameStartPanelObj.SetActive(false);
    }
    protected virtual void ClickStageExitButton() => SceneManager.LoadScene(1);
    protected virtual void ClickStageRestartButton() => SceneManager.LoadScene(2);
    protected virtual void GoToNextStage() => SceneManager.LoadScene(3);
    #endregion
}
