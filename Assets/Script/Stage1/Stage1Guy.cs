using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Guy : MonoBehaviour
{
    public int GuyIndex; //정답 모자와 연동할 인덱스값
    [SerializeField] private GameObject ResultParticle; //정답일 때의 출력할 파티클
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    #region 난쟁이 오브젝트 정답 판별
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Hats") && collision.gameObject.GetComponent<Stage1Hat>().IsDraging == false && collision.gameObject.GetComponent<Stage1Hat>().HatIndex == GuyIndex)
        {
            print("정답 소리");
            Instantiate(ResultParticle, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), ResultParticle.transform.rotation);
            StartCoroutine(HatAnim());
        }
        else if (collision.gameObject.CompareTag("Hats") && collision.gameObject.GetComponent<Stage1Hat>().IsDraging == false && collision.gameObject.GetComponent<Stage1Hat>().HatIndex != GuyIndex && collision.gameObject.GetComponent<Stage1Hat>().IsWrong == false)
        {
            collision.gameObject.GetComponent<Stage1Hat>().IsWrong = true;
            print("오답 소리");
            StartCoroutine(WrongHatAnim());
        }
    }
    #endregion
    IEnumerator HatAnim()
    {
        animator.SetBool("IsBad", false);
        animator.SetBool("IsCompleat", true);
        yield return new WaitForSeconds(3);
        animator.SetBool("IsCompleat", false);
        animator.SetBool("IsHat", true);
        yield return null;
    }
    IEnumerator WrongHatAnim()
    {
        animator.SetBool("IsBad", true);
        yield return new WaitForSeconds(3);
        animator.SetBool("IsBad", false);
        yield return null;
    }
}
