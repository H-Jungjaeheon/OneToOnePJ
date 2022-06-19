using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Guy : MonoBehaviour
{
    public int GuyIndex; //정답 모자와 연동할 인덱스값
    [SerializeField] private GameObject ResultParticle; //정답일 때의 출력할 파티클

    #region 난쟁이 오브젝트 정답 판별
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Hats") && collision.gameObject.GetComponent<Stage1Hat>().IsDraging == false && collision.gameObject.GetComponent<Stage1Hat>().HatIndex == GuyIndex)
        {
            print("정답 파티클");
            Instantiate(ResultParticle, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), ResultParticle.transform.rotation);
        }
    }
    #endregion
}
