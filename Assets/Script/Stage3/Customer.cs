using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Live2D.Cubism.Rendering;

public class Customer : MonoBehaviour
{
    [Header("움직임 관련 속도 변수")]
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float UpDownSpeed;
    [SerializeField] CubismRenderController rendererController;
    private bool IsUp, IsOrder, End;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rendererController.SortingMode = CubismSortingMode.BackToFrontZ;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartMove();
        Discrimination();
    }
    private void StartMove()
    {
        if(transform.position.x < -7 && !End) // -1.9 -2.12
        {
            if (!IsUp)
            {
                transform.position += new Vector3(MoveSpeed * Time.deltaTime, -(UpDownSpeed * Time.deltaTime), 0);
                if (transform.position.y <= -2.12f)
                    IsUp = true;
            }
            else
            {
                transform.position += new Vector3(MoveSpeed * Time.deltaTime, UpDownSpeed * Time.deltaTime, 0);
                if (transform.position.y >= -1.9f)
                    IsUp = false;
            }
        }
        else if(transform.position.x >= -7 && !IsOrder) //손 원래 위치 5, 7, 0
        {
            Stage3Manager.Instance.MatchesCountObj[Stage3Manager.Instance.MaxMatchesCount - 1].transform.DOScale(1, 0.5f);
            var HandObj = Stage3Manager.Instance.HandObj;
            HandObj.transform.position = Vector3.Lerp(HandObj.transform.position, new Vector3(5, 3.05f, 0), 0.05f);
            if(HandObj.transform.position.y <= 3.1)
            {
                Stage3Manager.Instance.IsTake = true;
                Stage3Manager.Instance.CustomerArrival = true;
                animator.SetBool("IsArrival", true);
                IsOrder = true;
            }
        }
    }
    private void Discrimination()
    {
        if (Stage3Manager.Instance.IsSuccess)
        {
            Stage3Manager.Instance.CustomerArrival = false;
            Stage3Manager.Instance.IsSuccess = false;
            animator.SetBool("IsArrival", false);
            animator.SetBool("IsGood", true);
            StartCoroutine(GoBack());
            Stage3Manager.Instance.ResultCount++;
        }
        else if (Stage3Manager.Instance.IsFail)
        {
            Stage3Manager.Instance.CustomerArrival = false;
            Stage3Manager.Instance.IsFail = false;
            animator.SetBool("IsArrival", false);
            animator.SetBool("IsBad", true);
            StartCoroutine(GoBack());
        }
    }
    private IEnumerator GoBack()
    {
        float DestroyCount = 0;

        yield return new WaitForSeconds(2.5f);

        animator.SetBool("IsGood", false);
        animator.SetBool("IsBad", false);

        End = true;
        rendererController.SortingMode = CubismSortingMode.BackToFrontOrder;
        //rendererController.SortingLayerId = -5;
        transform.rotation = Quaternion.Euler(0,180,0);

        while (true)
        {
            if (!IsUp)
            {
                transform.position += new Vector3(-MoveSpeed * Time.deltaTime, -(UpDownSpeed * Time.deltaTime), 0);
                if (transform.position.y <= -2.12f)
                    IsUp = true;
            }
            else
            {
                transform.position += new Vector3(-MoveSpeed * Time.deltaTime, UpDownSpeed * Time.deltaTime, 0);
                if (transform.position.y >= -1.9f)
                    IsUp = false;
            }

            if(DestroyCount >= 6)
                Destroy(gameObject);

            DestroyCount += Time.deltaTime;
            yield return null;
        }
    }
}
