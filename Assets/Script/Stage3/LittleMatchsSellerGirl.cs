using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleMatchsSellerGirl : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Update()
    {
        Discrimination();
    }

    private void Discrimination()
    {
        if (Stage3Manager.Instance.IsSuccess)
        {
            animator.SetBool("IsGood", true);
            StartCoroutine(ReturnIdleAnim());
        }
        else if (Stage3Manager.Instance.IsFail)
        {
            animator.SetBool("IsBad", true);
            StartCoroutine(ReturnIdleAnim());
        }
    }
    private IEnumerator ReturnIdleAnim()
    {
        yield return new WaitForSeconds(3);
        animator.SetBool("IsBad", false);
        animator.SetBool("IsGood", false);
    }
}
