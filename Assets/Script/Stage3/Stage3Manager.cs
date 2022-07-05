using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3Manager : Stage1Manager
{
    public static Stage3Manager Instance { get; set; }
    [SerializeField] private int MaxRandCount;

    private void FixedUpdate()
    {
        IsStageClear();
        StartPanelAnims();
    }

    /// <summary>
    /// GameOver 연출 함수
    /// </summary>
    /// <param name="FaidTime"></param>
    /// <returns></returns>
    private IEnumerator GameOver(float FaidTime)
    {
        GameEnd = true;
        WaitForSeconds WFS = new WaitForSeconds(0.5f);
        float NowFaidTime = 0;
        yield return WFS;
        Color color = StartFaidBackGround.color;
        GameStartPanelObj.SetActive(true);
        while (NowFaidTime < FaidTime)
        {
            color.a = NowFaidTime;
            StartFaidBackGround.color = color;
            NowFaidTime += Time.deltaTime;
            yield return null;
        }
        yield return WFS;
        SceneManager.LoadScene("Stage3");
    }

    /// <summary>
    /// 스테이지 시작 세팅 함수
    /// </summary>
    protected override void StartSetting()
    {
        base.StartSetting();
        Instance = this;
        MaxRandCount = Random.Range(1, 6);
    }

    private void IsStageClear()
    {
        ProgressImage.fillAmount = ResultCount / 4;
    }
    /// <summary>
    /// 스테이지 버튼 모음
    /// </summary>
    protected override void ClickStageExitButton() => SceneMove(1);
    protected override void ClickStageRestartButton() => SceneMove(4);
    protected override void GoToNextStage() => SceneMove(5);
}
