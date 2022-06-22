using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Live2D.Cubism.Rendering;

public class LittleMermaid : MonoBehaviour
{
    [SerializeField] Stage2Manager S2M;
    [SerializeField] Button MermaidUpButton, MermaidDownButton;
    [SerializeField] bool IsMoving, IsUp, IsDown, IsHit;
    [SerializeField] float Invincibilitytime, MaxInvincibilitytime;
    [SerializeField] GameObject CamShakeObj;
    [SerializeField] CubismRenderController rendererController;
    private Vector3 MoveTransForm, TargetPos;
    private int MermaidMoveIndex;
    private Animator animator;

    private void Awake()
    {

        StartSettings();
    }
    private void FixedUpdate()
    {
        MermaidMove();
        InvincibilityTime();
    }
    private void InvincibilityTime()
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
        if (!IsMoving && !IsUp && !IsDown && !Stage2Manager.Instance.GameClear)
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish") && !IsHit && !Stage2Manager.Instance.GameClear)
        {
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
}
