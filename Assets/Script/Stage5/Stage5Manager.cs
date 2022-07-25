using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage5Manager : Stage1Manager
{
    private static Stage5Manager instance = null;

    public static Stage5Manager Instance 
    {
        get 
        {
            if (null == instance)
            {
                return null;
            }
            else 
                return instance;
        }
    }
    
    [Tooltip("끼워넣은 조각 리스트")]
    public List<GameObject> jarPiece = new List<GameObject>();

    [Space(20)]
    [Tooltip("큰 항아리 오브젝트")]
    [SerializeField] private GameObject toBeCompletedJarObj;
    [SerializeField] private Sprite clearCompletedJarSprite;

    [Space(20)]
    [Tooltip("게임 클리어 연출용 이미지")]
    [SerializeField] private Image jarCompletedAnimImage;
    [SerializeField] private float jarCompletedAnimSpeed;
    private RectTransform rectTransform;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        rectTransform = jarCompletedAnimImage.GetComponent<RectTransform>();
    }
    private void FixedUpdate()
    {
        IsStageClear();
        StartPanelAnims();
    }
    private void IsStageClear()
    {
        ProgressImage.fillAmount = ResultCount / 4;
        if (ResultCount == 4)
        {
            GameEnd = true;
            ResultCount++;
            StartCoroutine(jarClearAnim());
        }
    }
    private IEnumerator jarClearAnim()
    {
        yield return new WaitForSeconds(2f);
        rectTransform.sizeDelta = new Vector2(5,0);
        while (rectTransform.sizeDelta.y < 2000)
        {
            rectTransform.sizeDelta += new Vector2(0, jarCompletedAnimSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while (rectTransform.sizeDelta.x < 4000)
        {
            rectTransform.sizeDelta += new Vector2(jarCompletedAnimSpeed * 3, 0);
            yield return null;
        }
        for (int nowJarListIndex = 0; nowJarListIndex < jarPiece.Count; nowJarListIndex++) 
        {
            Destroy(jarPiece[nowJarListIndex]);
        }
        jarPiece.Clear();
        toBeCompletedJarObj.GetComponent<SpriteRenderer>().sprite = clearCompletedJarSprite;
        yield return new WaitForSeconds(0.5f);
        Color color = jarCompletedAnimImage.color;
        while (color.a > 0)
        {
            color.a -= Time.deltaTime;
            jarCompletedAnimImage.color = color;
            yield return null;
        }
        StartCoroutine(StageClear(3));
    }

    protected override void ClickStageRestartButton() => SceneMove(6);
    protected override void GoToNextStage() => SceneMove(7);
}
