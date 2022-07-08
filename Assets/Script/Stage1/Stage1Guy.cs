using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Guy : MonoBehaviour
{
    public int GuyIndex; //정답 모자와 연동할 인덱스값
    [SerializeField] private GameObject ResultParticle; //정답일 때의 출력할 파티클
    [SerializeField] private Animator animator;

    [Header("사운드 모음")]
    [Space(20)]
    [SerializeField] protected AudioClip GoodClip;
    [SerializeField] protected AudioClip BadClip;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    #region 난쟁이 오브젝트 정답 판별
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Hats") && collision.gameObject.GetComponent<DragObj>().IsDraging == false && collision.gameObject.GetComponent<DragObj>().ObjIndex == GuyIndex)
        {
            GoodSound();
            Instantiate(ResultParticle, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), ResultParticle.transform.rotation);
            StartCoroutine(HatAnim());
        }
        else if (collision.gameObject.CompareTag("Hats") && collision.gameObject.GetComponent<DragObj>().IsDraging == false && collision.gameObject.GetComponent<DragObj>().ObjIndex != GuyIndex && collision.gameObject.GetComponent<DragObj>().IsWrong == false)
        {
            collision.gameObject.GetComponent<DragObj>().IsWrong = true;
            BadSound();
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

    protected void GoodSound() => SoundManager.Instance.SFXPlay("Good", GoodClip);
    protected void BadSound() => SoundManager.Instance.SFXPlay("Bad", BadClip);
}
