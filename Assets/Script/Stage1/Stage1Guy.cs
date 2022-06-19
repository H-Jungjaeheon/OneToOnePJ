using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Guy : MonoBehaviour
{
    public int GuyIndex; //���� ���ڿ� ������ �ε�����
    [SerializeField] private GameObject ResultParticle; //������ ���� ����� ��ƼŬ

    #region ������ ������Ʈ ���� �Ǻ�
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Hats") && collision.gameObject.GetComponent<Stage1Hat>().IsDraging == false && collision.gameObject.GetComponent<Stage1Hat>().HatIndex == GuyIndex)
        {
            print("���� ��ƼŬ");
            Instantiate(ResultParticle, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), ResultParticle.transform.rotation);
        }
    }
    #endregion
}
