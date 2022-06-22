using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LittleMermaid : MonoBehaviour
{
    [SerializeField] private Stage2Manager S2M;
    [SerializeField] private Button MermaidUpButton, MermaidDownButton;
    [SerializeField] private bool IsMoving, IsUp, IsDown, IsHit;
    [SerializeField] private float Invincibilitytime, MaxInvincibilitytime;
    [SerializeField] private GameObject CamShakeObj;
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
            if(Invincibilitytime >= MaxInvincibilitytime)
            {
                IsHit = false;
                animator.SetBool("IsHit", false);
                Invincibilitytime = 0;
            }
        }
    }
    private void StartSettings()
    {
        animator = GetComponent<Animator>();
        MermaidUpButton.onClick.AddListener(()=> MoveButtonClick(true));
        MermaidDownButton.onClick.AddListener(() => MoveButtonClick(false));
    }
    private void MoveButtonClick(bool IsUpClick)
    {
        if (!IsMoving && !IsUp && !IsDown)
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
            transform.position = Vector3.Lerp(transform.position, TargetPos, 0.1f);
        }
        else if(IsMoving && IsDown)
        {
            TargetPos = MoveTransForm - new Vector3(0, 2.15f, 0);
            transform.position = Vector3.Lerp(transform.position, TargetPos, 0.1f);
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
        if (collision.gameObject.CompareTag("Fish") && !IsHit)
        {
            CamShakeObj.GetComponent<CamShake>().VibrateForTime(0.8f);
            Stage2Manager.Instance.Hp -= 1;
            StartCoroutine(HpHit(Stage2Manager.Instance.Hp));
            animator.SetBool("IsHit", true);
            IsHit = true;
        }
    }
    public IEnumerator HpHit(int HeartIndex)
    {
        Stage2Manager.Instance.animator[HeartIndex].SetBool("IsHit", true);
        yield return new WaitForSeconds(0.8f);
        Stage2Manager.Instance.animator[HeartIndex].SetBool("IsDead", true);
    }
}
