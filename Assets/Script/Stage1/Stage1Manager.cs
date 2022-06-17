using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Stage1Manager : MonoBehaviour
{
    [SerializeField] private GameObject GameManager, GamePauseObj, GameStartPanelObj, StartPanelObj, StartPanelPos, SameBlackBG;
    [SerializeField] private Button StagePauseButton, StageContinueButton, StageExitButton, StageRestartButton, GameHelpButton, ExitGameHelpButton;
    [SerializeField] private Image StartFaidBackGround, StartPanelImage, ProgressImage, GameHelpImage;
    [SerializeField] private Text[] StartText;
    public float ResultCount;
    public RectTransform a;
    
    private void Awake()
    {
        //float b = a.anchoredPosition.x;
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
        GameManager = GameObject.Find("GameManager");
        StagePauseButton.onClick.AddListener(() => ClickStagePauseButton());
        StageContinueButton.onClick.AddListener(() => ClickStageContinueButton());
        StageExitButton.onClick.AddListener(() => ClickStageExitButton());
        StageRestartButton.onClick.AddListener(() => ClickStageRestartButton());
        GameHelpButton.onClick.AddListener(()=> ClickStageHelpButton());
        ExitGameHelpButton.onClick.AddListener(() => ClickStageHelpExitButton());
    }
    private void IsStageClear()
    {
        ProgressImage.fillAmount = ResultCount / 7;
        if (ResultCount >= 7)
        {
            //클리어 연출 출력(미개발)
            GameManager.GetComponent<GameManager>().StageClearCount++;
            SceneManager.LoadScene(1);
        }
    }
    IEnumerator StartAnim(float FaidTime)
    {
        float NowFaidTime = FaidTime;
        StartPanelObj.transform.DOMove(StartPanelPos.transform.position, 1.5f);
        yield return new WaitForSeconds(1.8f);
        Color color = StartFaidBackGround.color;
        Color color2 = StartPanelImage.color;
        while (NowFaidTime >= 0)
        {
            color.a = NowFaidTime;
            color2.a = NowFaidTime;
            StartFaidBackGround.color = color;
            StartPanelImage.color = color2;
            for (int a = 0; a < StartText.Length; a++){
                StartText[a].color = color;
            }
            NowFaidTime -= Time.deltaTime;
            yield return null;
        }
        GameStartPanelObj.SetActive(false);
    }
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
    }
    private void ClickStageHelpExitButton()
    {
        SameBlackBG.SetActive(false);
    }
    private void ClickStageExitButton() => SceneManager.LoadScene(1);
    private void ClickStageRestartButton() => SceneManager.LoadScene(2);
    #endregion
}
