using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    #region 시작 화면 UI 모음
    [Header("시작 화면 UI 모음")]
    [SerializeField] private GameObject TitleNameObj; //게임 제목 오브젝트
    [SerializeField] private GameObject GameSettingPopUp, GameStartPanelObj, GameExitPanelObj; //설정 창 오브젝트, 게임 시작 화면 연출 오브젝트, 게임 종료 창 오브젝트
    [SerializeField] private Image GameStartPanel; //게임 시작 페이드 연출 이미지
    [Header("타이틀 버튼 오브젝트")]
    [SerializeField] private Button GameStartButton; //게임 시작 버튼
    [SerializeField] private Button GameExitPanelOnButton, GameExitPanelOffButton, GameSettingButton, GameSettingExitButton, GameExitButton; //게임 종료 팝업 버튼, 게임 종료 취소 버튼, 설정 창 팝업 버튼, 설정 창 종료 버튼, 게임 종료 버튼
    [SerializeField] private bool IsSettingUp, IsStarting; //설정 판별, 게임 시작 판별
    private Vector3 pos;
    #endregion


    void Start()
    {
        StartSettings();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTitleName();
    }

    private void StartSettings()
    {
        Color color = GameStartPanel.GetComponent<Image>().color;
        color.a = 0f;
        GameStartPanel.color = color;
        IsSettingUp = false;
        IsStarting = false;
        GameStartPanelObj.SetActive(false);
        GameExitPanelObj.SetActive(false);
        GameStartButton.onClick.AddListener(() => StartCoroutine(GameStartCoroutine(1f)));
        GameSettingButton.onClick.AddListener(GameSetting);
        GameExitPanelOnButton.onClick.AddListener(()=> GameExitPanelObj.SetActive(true));
        GameExitPanelOffButton.onClick.AddListener(() => GameExitPanelObj.SetActive(false));
        GameSettingExitButton.onClick.AddListener(GameSettingClose);
        GameExitButton.onClick.AddListener(() => Application.Quit());
    }
    private void MoveTitleName() => TitleNameObj.transform.position = new Vector3(0, 1.5f + Mathf.Sin(Time.time * 0.5f) * 1, 0);
    #region 타이틀 버튼 관련 함수

    private IEnumerator GameStartCoroutine(float duration)
    {
        if(IsStarting == false)
        {
            float timer = 0;
            IsStarting = true;
            Color color = GameStartPanel.color;
            GameStartPanelObj.SetActive(true);
            while (timer < duration)
            {
                color.a = timer;
                GameStartPanel.color = color;
                timer += Time.deltaTime;
                yield return null;
            }
            color.a = 1f;
            GameStartPanel.color = color;
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(1);
        }
    }

    private void GameSetting()
    {
        if (IsSettingUp == false)
        {
            GameSettingPopUp.transform.DOMove(new Vector3(1300, 540, 0), 1.2f).SetEase(Ease.InOutBack);
            IsSettingUp = true;
        }
    }
    private void GameSettingClose()
    {
        if (IsSettingUp == true)
        {
            GameSettingPopUp.transform.DOMove(new Vector3(2700, 540, 0), 1.2f).SetEase(Ease.InOutBack);
            IsSettingUp = false;
        }
    }
    #endregion
}