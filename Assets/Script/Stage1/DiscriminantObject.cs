using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CookieObjIndex
{
    MinCookieObjIndex,
    MaxCookieObjIndex = 6
}

public class DiscriminantObject : MonoBehaviour
{
    [Tooltip("���� ������Ʈ ������ �ε�����")]
    public int objIndex;

    [Tooltip("������ ���� ����� ��ƼŬ")]
    [SerializeField] private GameObject ResultParticle;

    [Tooltip("���� �Ǻ� �ִϸ��̼�")]
    [SerializeField] private Animator animator;

    [Tooltip("���� �������� �ε���")]
    [SerializeField] private int nowStageIndex;

    [Header("�ִϸ��̼� ������ �Ǻ�")]
    [Tooltip("�ִϸ��̼� ������(�ƹ� �ִϸ��̼�)")]
    [SerializeField] private bool isAnimating;

    [Tooltip("�ִϸ��̼� ������(Ŭ���� �ִϸ��̼�)")]
    [SerializeField] private bool isCompleatAnimating;

    [Tooltip("���� ��Ű ������Ʈ ��ȯ ��ġ")]
    [SerializeField] private Vector3 cookieObjSpawnVector;

    [Tooltip("���� ��Ű �Ƿ翧 ��ǳ�� ������Ʈ ��ȯ ��ġ")]
    [SerializeField] private Vector3 cookieDialogSpawnVector;

    [Tooltip("����� �θ� ������Ʈ")]
    [SerializeField] private GameObject parentObj;

    [Header("���� ����")]
    [Space(20)]
    [SerializeField] protected AudioClip GoodClip;
    [SerializeField] protected AudioClip BadClip;
    [SerializeField] private GameObject spawnCookieDialogObj;

    private void Start()
    {
        StartSetting();
    }

    private void StartSetting()
    {
        var stage4Instance = Stage4Manager.Instance;
        if (nowStageIndex == 4)
        {
            objIndex = Random.Range((int)CookieObjIndex.MinCookieObjIndex, (int)CookieObjIndex.MaxCookieObjIndex);
            //stage4Instance.cookieDialog.Add(Instantiate(stage4Instance.SpeechBubbleObjs[objIndex], cookieDialogSpawnVector, stage4Instance.SpeechBubbleObjs[objIndex].transform.rotation).transform.parent = this.transform.parent);
            Instantiate(stage4Instance.SpeechBubbleObjs[objIndex], cookieDialogSpawnVector, stage4Instance.SpeechBubbleObjs[objIndex].transform.rotation).transform.parent = parentObj.transform;
            stage4Instance.cookieDialog.Add(parentObj.transform.GetChild(0).gameObject);
            spawnCookieDialogObj = parentObj.transform.GetChild(0).gameObject;
        }
        animator = GetComponent<Animator>();
    }
    #region ������Ʈ ���� �Ǻ�
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("DragObjs") && collision.gameObject.GetComponent<DragObj>().IsDraging == false && collision.gameObject.GetComponent<DragObj>().ObjIndex == objIndex)
        {
            var stage4Instance = Stage4Manager.Instance;
            if (isCompleatAnimating == false)
            {
                Instantiate(ResultParticle, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), ResultParticle.transform.rotation);
                GoodSound();
                if (nowStageIndex == 1)
                    StartCoroutine(CompleatAnim(3));
                else if (nowStageIndex == 4) 
                {
                    spawnCookieDialogObj.transform.GetChild(1).gameObject.SetActive(true);
                    StartCoroutine(CompleatAnim(3));
                    Instantiate(stage4Instance.CookieObjs[objIndex], cookieObjSpawnVector, stage4Instance.CookieObjs[objIndex].transform.rotation);
                }
            }
        }
        else if (collision.gameObject.CompareTag("DragObjs") && collision.gameObject.GetComponent<DragObj>().IsDraging == false && collision.gameObject.GetComponent<DragObj>().ObjIndex != objIndex && collision.gameObject.GetComponent<DragObj>().IsWrong == false)
        {
            collision.gameObject.GetComponent<DragObj>().IsWrong = true;
            if (isAnimating == false && isCompleatAnimating == false)
            {
                BadSound();
                if (nowStageIndex == 1)
                    StartCoroutine(WrongAnim(3));
                else if (nowStageIndex == 4)
                    StartCoroutine(WrongAnim(2.3f));
            }
        }
    }
    #endregion

    #region ���� �ִϸ��̼�
    IEnumerator CompleatAnim(float AnimWaitTime)
    {
        isAnimating = true;
        isCompleatAnimating = true;
        animator.SetBool("IsBad", false);
        animator.SetBool("IsCompleat", true);
        yield return new WaitForSeconds(AnimWaitTime); 
        animator.SetBool("IsCompleat", false);
        if (nowStageIndex == 1)
        {
            animator.SetBool("IsHat", true);
            isCompleatAnimating = false;
            isAnimating = false;
        }
        else
            Stage4Manager.Instance.CorrectAnswerCount += 1;
        yield return null;
    }
    IEnumerator WrongAnim(float AnimWaitTime)
    {
        isAnimating = true;
        animator.SetBool("IsBad", true);
        yield return new WaitForSeconds(AnimWaitTime); 
        animator.SetBool("IsBad", false);
        if(isCompleatAnimating == false)
           isAnimating = false;
        yield return null;
    }
    #endregion

    /// <summary>
    /// �������� 4 : 2���� ���� �Ϸ�� ���� ������ �Ѿ��
    /// </summary>
    public void NextQuestion() => StartCoroutine(NextQuestionAnim());

    private IEnumerator NextQuestionAnim()
    {
        var stage4Instance = Stage4Manager.Instance;
        if (Stage4Manager.Instance.ResultCount < 3)
        {
            yield return new WaitForSeconds(2);
            isCompleatAnimating = false;
            isAnimating = false;
            objIndex = Random.Range((int)CookieObjIndex.MinCookieObjIndex, (int)CookieObjIndex.MaxCookieObjIndex);
            Instantiate(stage4Instance.SpeechBubbleObjs[objIndex], cookieDialogSpawnVector, stage4Instance.SpeechBubbleObjs[objIndex].transform.rotation).transform.parent = parentObj.transform;
            stage4Instance.cookieDialog.Add(parentObj.transform.GetChild(0).gameObject);
            spawnCookieDialogObj = parentObj.transform.GetChild(0).gameObject;
        }
    }

    protected void GoodSound() => SoundManager.Instance.SFXPlay("Good", GoodClip);
    protected void BadSound() => SoundManager.Instance.SFXPlay("Bad", BadClip);
}
