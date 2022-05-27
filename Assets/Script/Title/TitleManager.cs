using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    #region ���� ȭ�� UI ����
    [Header("���� ȭ�� UI ����")]
    [SerializeField] private GameObject TitleNameObj;
    [SerializeField] private GameObject GameSettingPopUp;
    [Header("Ÿ��Ʋ ��ư ������Ʈ")]
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
    #region Ÿ��Ʋ ��ư ���� �Լ�
    private void GameStart()
    {
        SceneManager.LoadScene(1);
        Debug.Log("�����Խ���~");
    }
    private void GameExit()
    {
        Debug.Log("�����ҰŸ� ��Ŵ? ���� ����");
        Application.Quit();
    }
    private void GameSetting()
    {
        if(IsSettingUp == false)
        {
            Debug.Log("������ ����? ���� ����");
            GameSettingPopUp.transform.DOScale(1, 0.6f).SetEase(Ease.InOutQuad);
            IsSettingUp = true;
        }
    }
    private void GameSettingClose()
    {
        if(IsSettingUp == true)
        {
            Debug.Log("���� ���� ȭ�� ����");
            GameSettingPopUp.transform.DOScale(0, 0.6f).SetEase(Ease.InOutQuad);
            IsSettingUp = false;
        }
    }
    #endregion
}
 