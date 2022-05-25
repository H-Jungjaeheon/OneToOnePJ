using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    #region 시작 화면 UI 모음
    [Header("시작 화면 UI 모음")]
    [SerializeField] private GameObject TitleName;
    [SerializeField] private Button GameStartButton;
    [SerializeField] private Button GameExitButton, GameSettingButton;
    #endregion


    void Start()
    {
        GameStartButton.onClick.AddListener(GameStart);
        GameSettingButton.onClick.AddListener(GameSetting);
        GameExitButton.onClick.AddListener(GameExit);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTitleName();
    }
    private void MoveTitleName()
    {
       TitleName.transform.position = new Vector3(0,3 + Mathf.Sin(Time.time * 0.5f) * 0.5f, 0);
    }
    private void GameStart()
    {
        Debug.Log("빠르게시작~");
    }
    private void GameExit()
    {
        Debug.Log("종료할거면 왜킴? ㄹㅇ ㅋㅋ");
        Application.Quit();
    }
    private void GameSetting()
    {
        Debug.Log("세팅을 왜함? ㄹㅇ ㅋㅋ");
    }
}
