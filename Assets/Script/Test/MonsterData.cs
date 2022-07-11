using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster", order = int.MaxValue)]

public class MonsterData : ScriptableObject
{
    [SerializeField] private float hp;
    public float Hp => hp;

    [SerializeField] private float damage;
    public float Damage => damage;
}
