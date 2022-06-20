using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleMermaid : MonoBehaviour
{
    public Stage2Manager S2M;
    private void FixedUpdate()
    {
        Hp();
    }
    private void Hp()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            S2M.Hp -= 1;
        }
    }
}
