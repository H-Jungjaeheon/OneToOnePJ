using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBubble : MonoBehaviour
{
    [Header("�������� ����� ȿ�� ������Ʈ")]
    [SerializeField] private GameObject[] StageBackBubble;
    [Header("����� ȿ�� �̵� ���� ����")]
    [SerializeField] private float BubbleRightSpeed;
    [SerializeField] private float BubbleUpSpeed;
    private RectTransform BGRT;

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
                StageBackBubble[BubbleIndex].transform.position += new Vector3(BubbleRightSpeed * Time.deltaTime, BubbleUpSpeed * Time.deltaTime, 0);
                if (StageBackBubble[BubbleIndex].transform.position.y >= 6.8f)
                {
                    StageBackBubble[BubbleIndex].transform.position = new Vector3(-20.6f, -10, 0);
                }
            }
        }
    }
}
