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
    [SerializeField] private GameObject GameSettingPopUp, GameStartPanelObj, GameStartPanelObj2, CreditObj; //���� â ������Ʈ, ���� ���� ȭ�� ���� ������Ʈ, ���� ���� â ������Ʈ
    [SerializeField] private Image GameStartPanel, GameStartPanel2; //���� ���� ���̵� ���� �̹���
    [Header("Ÿ��Ʋ ��ư ������Ʈ")]
    [SerializeField] private Button GameStartButton; //���� ���� ��ư
    [SerializeField] private Button GameSettingButton, GameSettingExitButton, GoSettingButton, GoCreditButton; //���� â �˾� ��ư, ���� â ���� ��ư
    [SerializeField] private bool IsSettingUp; //���� �Ǻ�, ���� ���� �Ǻ�
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
    #region Ÿ��Ʋ ��ư ���� �Լ�

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