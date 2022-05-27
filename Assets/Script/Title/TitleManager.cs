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
    [SerializeField] private GameObject TitleNameObj;
    [SerializeField] private GameObject GameSettingPopUp;
    [Header("타이틀 버튼 오브젝트")]
    [SerializeField] private Button GameStartButton;
    [SerializeField] private Button GameExitButton, GameSettingButton, GameSettingExitButton;
    [SerializeField] private bool IsSettingUp;
    private Vector3 pos;
    #endregion


    void Start()
    {
        GameStartButton.onClick.AddListener(GameStart);
        GameSettingButton.onClick.AddListener(GameSetting);
        GameExitButton.onClick.AddListener(GameExit);
        GameSettingExitButton.onClick.AddListener(GameSettingClose);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTitleName();
    }
 
    private void MoveTitleName()
    {
        TitleNameObj.transform.position = new Vector3(0,3 + Mathf.Sin(Time.time * 0.5f) * 0.5f, 0);
    }
    #region 타이틀 버튼 관련 함수
    private void GameStart()
    {
        SceneManager.LoadScene(1);
        Debug.Log("빠르게시작~");
    }
    private void GameExit()
    {
        Debug.Log("종료할거면 왜킴? ㄹㅇ ㅋㅋ");
        Application.Quit();
    }
    private void GameSetting()
    {
        if(IsSettingUp == false)
        {
            Debug.Log("세팅을 왜함? ㄹㅇ ㅋㅋ");
            GameSettingPopUp.transform.DOScale(1, 0.6f).SetEase(Ease.InOutQuad);
            IsSettingUp = true;
        }
    }
    private void GameSettingClose()
    {
        if(IsSettingUp == true)
        {
            Debug.Log("게임 세팅 화면 종료");
            GameSettingPopUp.transform.DOScale(0, 0.6f).SetEase(Ease.InOutQuad);
            IsSettingUp = false;
        }
    }
    #endregion
}
 