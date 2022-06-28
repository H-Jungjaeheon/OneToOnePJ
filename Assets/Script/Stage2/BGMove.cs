using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    [SerializeField] private GameObject FrontBGs;
    [SerializeField] private GameObject MiddleBGs;
    [SerializeField] private float FrontBGMoveSpeed, MiddleBGMoveSpeed;
    private RectTransform FrontBGRT, MiddleBGRT;

    // Start is called before the first frame update
    void Start()
    {
        StartSetting();
    }

    void Update()
    {
        BGMoves();
    }
    private void StartSetting()
    {
        FrontBGRT = FrontBGs.GetComponent<RectTransform>();
        MiddleBGRT = MiddleBGs.GetComponent<RectTransform>();
    }
    private void BGMoves()
    {
        FrontBGRT.anchoredPosition += new Vector2(FrontBGMoveSpeed * Time.deltaTime, 0f);
        MiddleBGRT.anchoredPosition += new Vector2(MiddleBGMoveSpeed * Time.deltaTime, 0f);
        if (FrontBGRT.anchoredPosition.x >= 2960)
        {
            FrontBGRT.anchoredPosition = new Vector3(0, 0, 0);
        }
        if (MiddleBGRT.anchoredPosition.x >= 2960)
        {
            MiddleBGRT.anchoredPosition = new Vector3(0, 0, 0);
        }
    }
}
