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

    [Header("�ִϸ��̼� ���� �Ǻ�")]
    [Tooltip("���� �� �ִϸ��̼��� ������ �ִ���")]
    [SerializeField] private bool hasCompleatAnimate;

    [Header("�ִϸ��̼� ������ �Ǻ�")]
    [Tooltip("�ִϸ��̼� ������(�ƹ� �ִϸ��̼�)")]
    [SerializeField] private bool isAnimating;

    [Tooltip("�ִϸ��̼� ������(Ŭ���� �ִϸ��̼�)")]
    [SerializeField] private bool isCompleatAnimating;

    [Tooltip("���� ��Ű ������Ʈ ��ȯ ��ġ")]
    [SerializeField] private Vector3 cookieObjSpawnVector;

    [Tooltip("���� ��Ű �Ƿ翧 ��ǳ�� ������Ʈ ��ȯ ��ġ")]
    [SerializeField] private Vector3 cookieDialogSpawnVector;

    [Tooltip("����� ������Ʈ")]
    [SerializeField] private GameObject forProductionObj;

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
        if (nowStageIndex == 4)
        {
            var stage4Instance = Stage4Manager.Instance;
            objIndex = Random.Range((int)CookieObjIndex.MinCookieObjIndex, (int)CookieObjIndex.MaxCookieObjIndex);
            Stage4ReadyProduction();
        }
        animator = GetComponent<Animator>();
    }
    #region ������Ʈ ���� �Ǻ�
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DragObj>().IsDraging == false
        && collision.gameObject.CompareTag("DragObjs") && isCompleatAnimating == false)
        {
            var DragObjGetComponent = collision.gameObject.GetComponent<DragObj>();
            if (DragObjGetComponent.ObjIndex == objIndex && nowStageIndex != 5)
            {
                CorrectAwnser();
                switch (nowStageIndex)
                {
                    case 1:
                        StartCoroutine(CompleatAnim(3, true));
                        break;
                    case 4:
                        var stage4Instance = Stage4Manager.Instance;
                        spawnCookieDialogObj.transform.GetChild(1).gameObject.SetActive(true);
                        StartCoroutine(CompleatAnim(3, false));
                        Instantiate(stage4Instance.CookieObjs[objIndex], cookieObjSpawnVector, stage4Instance.CookieObjs[objIndex].transform.rotation);
                        break;
                    case 6:
                        if (hasCompleatAnimate)
                            StartCoroutine(CompleatAnim(4.3f, true));
                        else
                            StartCoroutine(CompleatAnim2(2.7f));
                        break;
                }
            }
            else if (DragObjGetComponent.ObjIndex != objIndex && DragObjGetComponent.IsWrong == false)
            {
                DragObjGetComponent.IsWrong = true;
                if (isAnimating == false)
                {
                    BadSound();
                    switch (nowStageIndex)
                    {
                        case 1:
                            StartCoroutine(WrongAnim(3));
                            break;
                        case 4:
                            StartCoroutine(WrongAnim(2.3f));
                            break;
                    }
                }
            }
        }
    }
    public void CorrectAwnser() 
    {
        Vector3 ResultParticleSpawnVector = new Vector2(transform.position.x, transform.position.y + 0.5f);
        Instantiate(ResultParticle, ResultParticleSpawnVector, ResultParticle.transform.rotation);
        GoodSound();
    }
    #endregion

    #region ĳ������ ���� �ִϸ��̼�
    IEnumerator CompleatAnim(float AnimWaitTime, bool needMoreAnim)
    {
        isAnimating = true;
        isCompleatAnimating = true;
        animator.SetBool("IsBad", false);
        animator.SetBool("IsCompleat", true);
        yield return new WaitForSeconds(AnimWaitTime); 
        animator.SetBool("IsCompleat", false);
        if (nowStageIndex == 1 || nowStageIndex == 6)
        {
            animator.SetBool("IsGood", true);
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

    #region ��������6 ���� ����
    IEnumerator CompleatAnim2(float AnimWaitTime) 
    {
        forProductionObj.SetActive(true);
        float nowAlpha = 1;
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        while (nowAlpha > 0)
        {
            sr.color = new Color(1, 1, 1, nowAlpha -= (Time.deltaTime / AnimWaitTime));
            yield return null;
        }
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
            Stage4ReadyProduction();
        }
    }

    public void Stage4ReadyProduction()
    {
        var stage4Instance = Stage4Manager.Instance;
        Instantiate(stage4Instance.SpeechBubbleObjs[objIndex], cookieDialogSpawnVector, stage4Instance.SpeechBubbleObjs[objIndex].transform.rotation).transform.parent = forProductionObj.transform;
        stage4Instance.cookieDialog.Add(forProductionObj.transform.GetChild(0).gameObject);
        spawnCookieDialogObj = forProductionObj.transform.GetChild(0).gameObject;
    }

    protected void GoodSound() => SoundManager.Instance.SFXPlay("Good", GoodClip);
    protected void BadSound() => SoundManager.Instance.SFXPlay("Bad", BadClip);
}
