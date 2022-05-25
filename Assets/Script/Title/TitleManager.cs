using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    #region ���� ȭ�� UI ����
    [Header("���� ȭ�� UI ����")]
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
        Debug.Log("�����Խ���~");
    }
    private void GameExit()
    {
        Debug.Log("�����ҰŸ� ��Ŵ? ���� ����");
        Application.Quit();
    }
    private void GameSetting()
    {
        Debug.Log("������ ����? ���� ����");
    }
}
