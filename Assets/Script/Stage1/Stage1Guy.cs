using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Guy : MonoBehaviour
{
    public int GuyIndex; //���� ���ڿ� ������ �ε�����
    [SerializeField] private GameObject ResultParticle; //������ ���� ����� ��ƼŬ
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    #region ������ ������Ʈ ���� �Ǻ�
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Hats") && collision.gameObject.GetComponent<Stage1Hat>().IsDraging == false && collision.gameObject.GetComponent<Stage1Hat>().HatIndex == GuyIndex)
        {
            print("���� �Ҹ�");
            Instantiate(ResultParticle, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), ResultParticle.transform.rotation);
            StartCoroutine(HatAnim());
        }
        else if (collision.gameObject.CompareTag("Hats") && collision.gameObject.GetComponent<Stage1Hat>().IsDraging == false && collision.gameObject.GetComponent<Stage1Hat>().HatIndex != GuyIndex && collision.gameObject.GetComponent<Stage1Hat>().IsWrong == false)
        {
            collision.gameObject.GetComponent<Stage1Hat>().IsWrong = true;
            print("���� �Ҹ�");
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
