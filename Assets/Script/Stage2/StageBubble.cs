using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBubble : MonoBehaviour
{
    [Header("�������� ����� ȿ�� ������Ʈ")]
    [SerializeField] private GameObject[] StageBackBubble;
    [Header("����� ȿ�� �̵� ���� ����")]
    [SerializeField] private float BubbleRightSpeed;
    [SerializeField] private float BubbleUpSpeed, BubbleHorizontal;

    void Update()
    {
        BubbleMove();
    }

    private void BubbleMove()
    {
        if (!Stage2Manager.Instance.IsClickHelp)
        {
            for (int BubbleIndex = 0; BubbleIndex < 2; BubbleIndex++)
            {
                StageBackBubble[BubbleIndex].transform.position += new Vector3(BubbleRightSpeed * Time.unscaledDeltaTime, BubbleUpSpeed * Time.unscaledDeltaTime, 0);
                StageBackBubble[BubbleIndex].transform.position += new Vector3(Mathf.Cos(Time.unscaledTime * BubbleHorizontal) * 0.03f, 0, 0);
                if (StageBackBubble[BubbleIndex].transform.position.y >= 6.8f)
                {
                    StageBackBubble[BubbleIndex].transform.position = new Vector3(-20.6f, -10, 0);
                }
            }
        }
    }
}
