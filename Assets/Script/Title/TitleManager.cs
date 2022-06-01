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
    [SerializeField] private GameObject TitleNameObj; //���� ���� ������Ʈ
    [SerializeField] private GameObject GameSettingPopUp, GameStartPanelObj, GameExitPanelObj; //���� â ������Ʈ, ���� ���� ȭ�� ���� ������Ʈ, ���� ���� â ������Ʈ
    [SerializeField] private Image GameStartPanel; //���� ���� ���̵� ���� �̹���
    [Header("Ÿ��Ʋ ��ư ������Ʈ")]
    [SerializeField] private Button GameStartButton; //���� ���� ��ư
    [SerializeField] private Button GameExitPanelOnButton, GameExitPanelOffButton, GameSettingButton, GameSettingExitButton, GameExitButton; //���� ���� �˾� ��ư, ���� ���� ��� ��ư, ���� â �˾� ��ư, ���� â ���� ��ư, ���� ���� ��ư
    [SerializeField] private bool IsSettingUp, IsStarting; //���� �Ǻ�, ���� ���� �Ǻ�
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
    #region Ÿ��Ʋ ��ư ���� �Լ�

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