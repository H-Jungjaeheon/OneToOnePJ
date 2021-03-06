using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    #region 이동 관련 변수
    [Header("이동 속도")]
    [SerializeField] private float Speed;
    [Header("충돌 애니메이션 속도, 최소 / 최대 랜덤 움직임 실행 시간")]
    [SerializeField] private float HitSpeed;
    [SerializeField] private float MinMoveIndex, MaxMoveIndex;
    [Header("랜덤 움직임 실행 판별")]
    [SerializeField] private bool IsMoveCompleat;
    [SerializeField] private bool IsUping, IsDowning, IsMoving;
    private bool IsEnd, IsHalfTimes;
    private Vector3 TargetPos;
    private int RandMove;
    private float MoveIndex, NowMoveIndex, NowYPos;
    #endregion

    private Rigidbody2D rigid;
    public bool IsHit;
    private bool SoundOut;

    [Header("사운드 모음")]
    [Space(20)]
    [SerializeField] private AudioClip FishRandMoveClip;

    private void Start()
    {
        StartSetting();
    }
    private void FixedUpdate()
    {
        Move();
        GameEnd();
    }
    private void StartSetting()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        MoveIndex = Random.Range(MinMoveIndex, MaxMoveIndex);
        NowYPos = gameObject.transform.position.y;
        int IsRandMove = Random.Range(0, 2);
        if(IsRandMove == 1)
        {
            RandMove = Random.Range(0, 2);
            IsMoving = true;
        }
        if(Stage2Manager.Instance.ResultCount >= Stage2Manager.Instance.MaxResultCount / 2)
        {
            IsHalfTimes = true;
        }
    }
    private void Move()
    {
        NowMoveIndex += Time.deltaTime;
        if (!IsHit)
            transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
        if(NowMoveIndex >= MoveIndex && IsMoveCompleat == false && IsHalfTimes && IsMoving && IsEnd == false)
        {
            if (!SoundOut)
            {
                FishRandMoveClipSound();
                SoundOut = true;
            }
            if (RandMove == 0)
            {
                if(NowYPos + 2.15f <= 2.15f)
                    RandMoves(true);
                else
                    RandMoves(false);
            }
            else
            {
                if(NowYPos - 2.15f >= -2.15f)
                    RandMoves(false);
                else
                    RandMoves(true);
            }
            if (TargetPos.y <= transform.position.y + 0.005f && IsUping || TargetPos.y >= transform.position.y - 0.005f && IsDowning)
            {
                IsMoveCompleat = true;                                  
                IsDowning = false;
                IsUping = false;
            }
        }
    }
    private void RandMoves(bool IsUp)
    {
        if (IsUp)
        {
            IsUping = true;
            TargetPos = new Vector3(transform.position.x, NowYPos + 2.15f, 0);
            transform.position = Vector3.Lerp(transform.position, TargetPos, 0.1f);
        }
        else
        {
            IsDowning = true;
            TargetPos = new Vector3(transform.position.x, NowYPos - 2.15f, 0);
            transform.position = Vector3.Lerp(transform.position, TargetPos, 0.1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TheLittleMermaid") && !IsHit)
            StartCoroutine(FishHitAnim());
        else if(collision.gameObject.CompareTag("ObjDestroy"))
            Destroy(gameObject);
    }
    private void GameEnd()
    {
        if (Stage2Manager.Instance.GameEnd && !IsHit)
        {
            StartCoroutine(FishHitAnim());
            IsEnd = true;
        }
    }
    private IEnumerator FishHitAnim()
    {
        IsHit = true;
        transform.rotation = Quaternion.Euler(0, 0, 180);
        rigid.AddForce(Vector3.up * HitSpeed);
        yield return new WaitForSeconds(0.3f);
        rigid.velocity = Vector3.zero;
        rigid.AddForce(Vector3.down * HitSpeed * 3);
    }
    private void FishRandMoveClipSound() => SoundManager.Instance.SFXPlay("FishRandMove", FishRandMoveClip);
}
