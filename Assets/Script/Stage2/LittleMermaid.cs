using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Live2D.Cubism.Rendering;

public class LittleMermaid : MonoBehaviour
{
    [SerializeField] Button MermaidUpButton, MermaidDownButton;
    [SerializeField] bool IsMoving, IsUp, IsDown, IsHit, IsEndAnim;
    [SerializeField] float Invincibilitytime, MaxInvincibilitytime, LastAnimSpeed;
    [SerializeField] GameObject CamShakeObj;
    [SerializeField] CubismRenderController rendererController;
    private Vector3 MoveTransForm, TargetPos;
    private int MermaidMoveIndex;
    private Animator animator;
    private Rigidbody2D rigid;

    [Header("사운드 모음")]
    [Space(20)]
    [SerializeField] private AudioClip UpDownButtonClickClip;
    [SerializeField] private AudioClip FishHitClip;

    private void Awake()
    {
        StartSettings();
    }
    private void FixedUpdate()
    {
        MermaidMove();
        InvincibilityTime();
    }
    private void InvincibilityTime() //맞은 후 무적 함수
    {
        if (IsHit)
        {
            Invincibilitytime += Time.deltaTime;
            if (Invincibilitytime >= MaxInvincibilitytime)
            {
                rendererController.Opacity = 1f;
                IsHit = false;
                animator.SetBool("IsHit", false);
                Invincibilitytime = 0;
            }
        }
    }
    private void StartSettings()
    {
        animator = GetComponent<Animator>();
        MermaidUpButton.onClick.AddListener(() => MoveButtonClick(true));
        MermaidDownButton.onClick.AddListener(() => MoveButtonClick(false));
    }
    private void MoveButtonClick(bool IsUpClick)
    {
        UpDownButtonClickSound();
        if (!IsMoving && !IsUp && !IsDown && !Stage2Manager.Instance.GameEnd)
        {
            MoveTransForm = transform.position;
            if (IsUpClick && MermaidMoveIndex < 1 || !IsUpClick && MermaidMoveIndex > -1)
            {
                IsMoving = true;
                if (IsUpClick)
                {
                    IsUp = true;
                    MermaidMoveIndex++;
                }
                else
                {
                    IsDown = true;
                    MermaidMoveIndex--;
                }
            }
        }
    }
    private void MermaidMove()
    {
        if (IsMoving && IsUp)
        {
            TargetPos = MoveTransForm + new Vector3(0, 2.15f, 0);
            transform.position = Vector3.Lerp(transform.position, TargetPos, 0.2f);
        }
        else if (IsMoving && IsDown)
        {
            TargetPos = MoveTransForm - new Vector3(0, 2.15f, 0);
            transform.position = Vector3.Lerp(transform.position, TargetPos, 0.2f);
        }
        if (TargetPos.y <= transform.position.y + 0.005f && IsUp || TargetPos.y >= transform.position.y - 0.005f && IsDown)
        {
            IsMoving = false;
            IsUp = false;
            IsDown = false;
        }
        if(Stage2Manager.Instance.ResultCount >= Stage2Manager.Instance.MaxResultCount && !IsEndAnim)
        {
            IsEndAnim = true;
            StartCoroutine(StageClearAnim());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish") && !IsHit && !Stage2Manager.Instance.GameEnd)
        {
            FishHitSound();
            IsHit = true;
            rendererController.Opacity = 0.8f;
            CamShakeObj.GetComponent<CamShake>().VibrateForTime(0.4f);
            Stage2Manager.Instance.Hp -= 1;
            StartCoroutine(HpHit(Stage2Manager.Instance.Hp));
            animator.SetBool("IsHit", true);
        }
    }
    public IEnumerator HpHit(int HeartIndex)
    { 
        Stage2Manager.Instance.animator[HeartIndex].SetBool("IsHit", true);
        yield return new WaitForSeconds(0.8f);
        Stage2Manager.Instance.animator[HeartIndex].SetBool("IsDead", true);
    }
    private IEnumerator StageClearAnim()
    {
        yield return new WaitForSeconds(1f);
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector3.right * LastAnimSpeed);
        yield return new WaitForSeconds(0.7f);
        rigid.velocity = Vector3.zero;
        rigid.AddForce(Vector3.left * LastAnimSpeed * 3);
        yield return new WaitForSeconds(2f);
        rigid.velocity = Vector3.zero;
    }
    private void UpDownButtonClickSound() => SoundManager.Instance.SFXPlay("UpDownButtonClick", UpDownButtonClickClip);
    private void FishHitSound() => SoundManager.Instance.SFXPlay("FishHit", FishHitClip);
}
