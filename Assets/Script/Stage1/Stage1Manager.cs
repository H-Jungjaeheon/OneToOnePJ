using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Stage1Manager : MonoBehaviour
{
    [Header("게임 오브젝트 모음")]
    [SerializeField] private GameObject GamePauseObj; //일시정지 오브젝트
    [SerializeField] private GameObject GameStartPanelObj, StartPanelObj, StartPanelPos, // 게임시작 연출 전체 오브젝트, 게임시작 연출 간판 오브젝트, 간판 오브젝트 위치 오브젝트
    SameBlackBG, GameHelpObj, StartAnimSkipButtonObj; // 공용사용(Faid) 연출 오브젝트, 게임 도움말 오브젝트, 스타트 애니메이션 스킵 버튼 오브젝트
    [Header("버튼 모음")]
    [SerializeField] private Button StagePauseButton;
    [SerializeField] private Button StageContinueButton, StageExitButton,
    StageRestartButton, GameHelpButton, ExitGameHelpButton, StartAnimSkipButton;
    [Header("이미지 모음")]
    [SerializeField] private Image StartFaidBackGround;
    [SerializeField] private Image StartPanelImage, ProgressImage;
    [SerializeField] private Text[] StartText;
    [SerializeField] private Text SkipText;
    public float ResultCount;
    
    private void Awake()
    {
        StartSetting();
    }
    private void Start()
    {
        StartCoroutine(StartAnim(1.5f));
    }
    private void FixedUpdate()
    {
        IsStageClear();
    }
    private void StartSetting()
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
        if(GameManager.Instance.IsSkipAble[0]) StartAnimSkipButtonObj.SetActive(true);
        else StartAnimSkipButtonObj.SetActive(false);
    }
    #region 스테이지 클리어 함수
    private void IsStageClear()
    {
        ProgressImage.fillAmount = ResultCount / 7;
        if (ResultCount >= 7)
        {
            //클리어 연출 출력(미개발)
            GameManager.Instance.StageClearCount++;
            SceneManager.LoadScene(1);
        }
    }
    #endregion
    #region 시작 애니메이션
    IEnumerator StartAnim(float FaidTime)
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
    }
    #endregion

    #region 스테이지 버튼 모음
    private void ClickStagePauseButton()
    {
        GamePauseObj.SetActive(true);
        SameBlackBG.SetActive(true);
    }
    private void ClickStageContinueButton()
    {
        GamePauseObj.SetActive(false);
        SameBlackBG.SetActive(false);
    }
    private void ClickStageHelpButton()
    {
        SameBlackBG.SetActive(true);
        GameHelpObj.transform.DOMove(new Vector3(1480, GameHelpObj.transform.position.y, 0), 1.2f).SetEase(Ease.InOutBack);
    }
    private void ClickStageHelpExitButton()
    {
        SameBlackBG.SetActive(false);
        GameHelpObj.transform.DOMove(new Vector3(3250, GameHelpObj.transform.position.y, 0), 1.2f).SetEase(Ease.InOutBack);
    }
    private void ClickStageStartAnimSkipButton()
    {
        StopCoroutine(StartAnim(0));
        GameStartPanelObj.SetActive(false);
    }
    private void ClickStageExitButton() => SceneManager.LoadScene(1);
    private void ClickStageRestartButton() => SceneManager.LoadScene(2);
    #endregion
}
