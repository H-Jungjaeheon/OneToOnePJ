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
    [SerializeField] private GameObject GameSettingPopUp, GameStartPanelObj, GameStartPanelObj2, CreditObj; //설정 창 오브젝트, 게임 시작 화면 연출 오브젝트, 게임 종료 창 오브젝트
    [SerializeField] private Image GameStartPanel, GameStartPanel2; //게임 시작 페이드 연출 이미지
    [Header("타이틀 버튼 오브젝트")]
    [SerializeField] private Button GameStartButton; //게임 시작 버튼
    [SerializeField] private Button GameSettingButton, GameSettingExitButton, GoSettingButton, GoCreditButton; //설정 창 팝업 버튼, 설정 창 종료 버튼
    [SerializeField] private bool IsSettingUp; //설정 판별, 게임 시작 판별
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
        GameStartPanel2.color = color;
        IsSettingUp = false;
        GameStartPanelObj2.SetActive(false);
        GameStartPanelObj.SetActive(false);
        GameStartButton.onClick.AddListener(() => StartCoroutine(GameStartCoroutine(1f)));
        GameSettingButton.onClick.AddListener(GameSetting);
        GameSettingExitButton.onClick.AddListener(GameSettingClose);
        GoSettingButton.onClick.AddListener(() => SettingOnOff(true));
        GoCreditButton.onClick.AddListener(() => SettingOnOff(false));
    }
    private void MoveTitleName() => TitleNameObj.transform.position = new Vector3(TitleNameObj.transform.position.x, TitleNameObj.transform.position.y + Mathf.Sin(Time.time * 1f) * 0.001f, 0);
    #region 타이틀 버튼 관련 함수

    private IEnumerator GameStartCoroutine(float duration)
    {
        WaitForSeconds WFS = new WaitForSeconds(1f);
        if(IsSettingUp == false)
        {
            float timer = 0;
            Color color = GameStartPanel.color;
            Color color2 = GameStartPanel2.color;
            GameStartPanelObj.SetActive(true);
            GameStartPanelObj2.SetActive(true);
            while (timer < duration)
            {
                color.a = timer;
                GameStartPanel.color = color;
                timer += Time.deltaTime;
                yield return null;
            }
            color.a = 1f;
            GameStartPanel.color = color;
            timer = 0;
            yield return WFS;
            while (timer < duration)
            {
                color2.a = timer;
                GameStartPanel2.color = color2;
                timer += Time.deltaTime;
                yield return null;
            }
            color2.a = 1f;
            GameStartPanel2.color = color2;
            yield return WFS;
            SceneManager.LoadScene(1);
        }
    }

    private void GameSetting()
    {
        if (IsSettingUp == false)
        {
            GameSettingPopUp.transform.DOScale(1, 0);
            IsSettingUp = true;
        }
    }
    private void GameSettingClose()
    {
        if (IsSettingUp == true)
        {
            GameSettingPopUp.transform.DOScale(0, 0);
            IsSettingUp = false;
            CreditObj.SetActive(false);
        }
    }
    private void SettingOnOff(bool IsSettingOn)
    {
        if (IsSettingOn)
            CreditObj.SetActive(false);
        else
            CreditObj.SetActive(true);
    }
    #endregion
}