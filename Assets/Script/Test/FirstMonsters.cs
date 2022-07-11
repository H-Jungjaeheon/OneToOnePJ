using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMonsters : MonoBehaviour
{
    [SerializeField] private MonsterData MD;
    private float Hp;
    // Start is called before the first frame update
    void Start()
    {
        Hp = MD.Damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
