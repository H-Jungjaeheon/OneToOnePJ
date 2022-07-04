using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjs : MonoBehaviour
{
    [Header("�� ��������")]
    [SerializeField] private int NowStage;
    [Header("�������� ����� ȿ�� ������Ʈ")]
    [SerializeField] private GameObject[] MovingObj;
    [Header("����� ȿ�� �̵� ���� ����")]
    [SerializeField] private float MovingObjRightSpeed;
    [SerializeField] private float MovingObjUpSpeed;
    [Header("�������� ������Ʈ �Ǻ�")]
    [SerializeField] private bool IsBubble, IsSnow;
    [Header("�� ������Ʈ �¿� �Ǻ�")]
    [SerializeField] private bool IsLeft;
    private RectTransform BGRT;

    void Update()
    {
        if(NowStage == 2 && !Stage2Manager.Instance.IsClickHelp)
        {
            ObjMove();
        }
        else if(NowStage == 3 && !Stage3Manager.Instance.IsClickHelp)
        {
            ObjMove();
        }
    }
    private void ObjMove()
    {
        if (IsBubble)
        {
            for (int BubbleIndex = 0; BubbleIndex < 2; BubbleIndex++)
            {
                MovingObj[BubbleIndex].transform.position += new Vector3(MovingObjRightSpeed * Time.deltaTime, MovingObjUpSpeed * Time.deltaTime, 0);
                if (MovingObj[BubbleIndex].transform.position.y >= 6.8f)
                {
                    MovingObj[BubbleIndex].transform.position = new Vector3(-20.6f, -10, 0);
                }
            }
        }
        if(IsSnow)
        {
            for (int BubbleIndex = 0; BubbleIndex < 2; BubbleIndex++)
            {
                if (MovingObj[BubbleIndex].transform.position.x <= -1 && IsLeft)
                {
                    MovingObj[BubbleIndex].transform.position += new Vector3(-MovingObjRightSpeed * Time.deltaTime, -MovingObjUpSpeed * Time.deltaTime, 0);
                    IsLeft = false;
                }
                else if (MovingObj[BubbleIndex].transform.position.x >= 0 && !IsLeft)
                {
                    MovingObj[BubbleIndex].transform.position += new Vector3(MovingObjRightSpeed * Time.deltaTime, -MovingObjUpSpeed * Time.deltaTime, 0);
                    IsLeft = true;
                }
                if (MovingObj[BubbleIndex].transform.position.y <= -10.16f)
                {
                    MovingObj[BubbleIndex].transform.position = new Vector3(0, 10.3f, 0);
                }
            }
        }
    }
}
