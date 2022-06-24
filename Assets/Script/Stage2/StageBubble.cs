using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBubble : MonoBehaviour
{
    [Header("�������� ����� ȿ�� ������Ʈ")]
    [SerializeField] private GameObject[] StageBackBubble;
    [SerializeField] private GameObject BGs;
    [Header("����� ȿ�� �̵� ���� ����")]
    [SerializeField] private float BubbleRightSpeed;
    [SerializeField] private float BubbleUpSpeed, BGMoveSpeed;

    private RectTransform BGRT;

    private void Start()
    {
        StartSetting();
    }

    void Update()
    {
        BubbleMove();
        BGMove();
    }
    private void StartSetting() => BGRT = BGs.GetComponent<RectTransform>();
    private void BGMove()
    {
        if (!Stage2Manager.Instance.IsClickHelp)
        {
            BGRT.anchoredPosition += new Vector2(BGMoveSpeed, 0f);
            if (BGRT.anchoredPosition.x >= 2960)
            {
                BGRT.anchoredPosition = new Vector3(0, 0, 0);
            }
        }
    }
    private void BubbleMove()
    {
        if (!Stage2Manager.Instance.IsClickHelp)
        {
            for (int BubbleIndex = 0; BubbleIndex < 2; BubbleIndex++)
            {
                StageBackBubble[BubbleIndex].transform.position += new Vector3(BubbleRightSpeed * Time.deltaTime, BubbleUpSpeed * Time.deltaTime, 0);
                if (StageBackBubble[BubbleIndex].transform.position.y >= 6.8f)
                {
                    StageBackBubble[BubbleIndex].transform.position = new Vector3(-20.6f, -10, 0);
                }
            }
        }
    }
}
