using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBubble : MonoBehaviour
{
    [Header("스테이지 물방울 효과 오브젝트")]
    [SerializeField] private GameObject[] StageBackBubble;
    [Header("물방울 효과 이동 관련 변수")]
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
