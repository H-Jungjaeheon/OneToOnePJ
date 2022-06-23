using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBubble : MonoBehaviour
{
    [Header("�������� ����� ȿ�� ������Ʈ")]
    [SerializeField] private GameObject[] StageBackBubble;
    [Header("����� ȿ�� �̵� ���� ����")]
    [SerializeField] private float BubbleRightSpeed, BubbleUpSpeed;

    void Update()
    {
        BubbleMove();
    }

    private void BubbleMove()
    {
        for (int BubbleIndex = 0; BubbleIndex < 2; BubbleIndex++)
        {
            StageBackBubble[BubbleIndex].transform.position += new Vector3(BubbleRightSpeed * Time.deltaTime, BubbleUpSpeed * Time.deltaTime, 0);
            //if(StageBackBubble[BubbleIndex].transform.position >= new Vector3(20.5f, 10, 0))
            //{

            //}
        }
    }
}
