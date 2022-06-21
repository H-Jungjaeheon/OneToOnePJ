using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LittleMermaid : MonoBehaviour
{
    public Stage2Manager S2M;
    [SerializeField] private int MermaidMoveIndex;
    [SerializeField] private Button MermaidUpButton, MermaidDownButton;
    [SerializeField] private Vector3 MoveTransForm, TargetPos;
    [SerializeField] private bool IsMoving, IsUp, IsDown;
    private void Awake()
    {
        StartSettings();
    }
    private void FixedUpdate()
    {
        Hp();
        MermaidMove();
    }
    private void Hp()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            S2M.Hp -= 1;
        }
    }
    private void StartSettings()
    {
        MermaidUpButton.onClick.AddListener(()=> MoveButtonClick(true));
        MermaidDownButton.onClick.AddListener(() => MoveButtonClick(false));
    }
    private void MoveButtonClick(bool IsUpClick)
    {
        MoveTransForm = transform.position;
        IsMoving = true;
        if (IsUpClick && MermaidMoveIndex < 1)
        {
            IsUp = true;
            MermaidMoveIndex++;
        }
        else if(!IsUpClick && MermaidMoveIndex > -1)
        {
            IsDown = true;
            MermaidMoveIndex--;
        }
    }
    private void MermaidMove() //받아온 현재 포지션에서 이동량만큼 가고 다 오면 false
    {
        Vector3 Pos = Vector3.zero;
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

        if (TargetPos.y == transform.position.y + 0.01f && IsUp || TargetPos.y == transform.position.y - 0.001f && IsDown)
        {
            IsMoving = false;
            IsUp = false;
            IsDown = false;
        }
        


    }
}
