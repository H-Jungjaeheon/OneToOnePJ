using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageSelectManager : MonoBehaviour
{
    #region 스테이지 선택 씬 버튼 및 오브젝트 모음
    [Header("버튼 모음")]
    [SerializeField] private Button StagePassButtonLeft;
    [SerializeField] private Button StagePassButtonRight, SettingButton, SettingCloseButton, ReturnTitleButton;
    [SerializeField] private Button[] StageLockButton;
    [Header("오브젝트 모음")]
    [SerializeField] private GameObject ReturnTitleObj, GameSettingPopUp;
    [SerializeField] private GameObject[] BookObjs, BookPositionObj;
    [Header("씬 이동 연출 이미지")]
    [SerializeField] private Image ReturnToTitleImage;
    [Header("현재 스테이지 선택 인덱스")]
    [SerializeField] private int StageSelectCount;
    [SerializeField] private bool IsSettingUp;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartSetting();
    }

    // Update is called once per frame
    void Update()
    {
        BookAnim();
    }
    private void BookAnim()
    {
        for(int a = 0; a < 6; a++)
        {
            BookObjs[a].transform.position = new Vector3(BookObjs[a].transform.position.x, 540 + Mathf.Cos(Time.time * 1.6f) * 10, 0);
        }
    }
    private void StartSetting()
    {
        StageSelectCount = 0;
        ReturnTitleObj.SetActive(false);
        SettingButton.onClick.AddListener(SettingPopUp);
        SettingCloseButton.onClick.AddListener(SettingClose);
        ReturnTitleButton.onClick.AddListener(() => StartCoroutine(ReturnToTitle(1f)));
        StagePassButtonLeft.onClick.AddListener(StagePassL);
        StagePassButtonRight.onClick.AddListener(StagePassR);
        for(int a = 0; a < 5; a++)
        {
            StageLockButton[a].onClick.AddListener(StageLock);
        }
    }
    private void StageLock()
    {

    }
    private void SettingPopUp()
    {
        if (IsSettingUp == false)
        {
            GameSettingPopUp.transform.DOMove(new Vector3(1300, 540, 0), 1.2f).SetEase(Ease.InOutBack);
            IsSettingUp = true;
        }
    }
    private void SettingClose()
    {
        if (IsSettingUp == true)
        {
            GameSettingPopUp.transform.DOMove(new Vector3(2700, 540, 0), 1.2f).SetEase(Ease.InOutBack);
            IsSettingUp = false;
        }
    }
    private IEnumerator ReturnToTitle(float FaidTime)
    {
        if(IsSettingUp == false)
        {
            ReturnTitleObj.SetActive(true);
            Color color = ReturnToTitleImage.color;
            float NowFaidTime = 0;
            while (NowFaidTime <= FaidTime)
            {
                color.a = NowFaidTime;
                ReturnToTitleImage.color = color;
                NowFaidTime += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(0);
        }
    }
    private void StagePassL()
    {
        if((StageSelectCount - 1) >= 0 && IsSettingUp == false)
        {
            StageSelectCount--;
            BookObjs[StageSelectCount].transform.DOMove(BookPositionObj[1].transform.position, 1).SetEase(Ease.InBack);
            BookObjs[StageSelectCount + 1].transform.DOMove(BookPositionObj[2].transform.position, 1).SetEase(Ease.InBack);
        }
    }
    private void StagePassR()
    {
        if((StageSelectCount + 1) <= 5 && IsSettingUp == false)
        {
            StageSelectCount++;
            BookObjs[StageSelectCount].transform.DOMove(BookPositionObj[1].transform.position, 1).SetEase(Ease.InBack);
            BookObjs[StageSelectCount - 1].transform.DOMove(BookPositionObj[0].transform.position, 1).SetEase(Ease.InBack);
        }
    }
}
