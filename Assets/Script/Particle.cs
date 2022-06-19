using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] float LifeTime;
    private void Start() => Invoke("ObjDestroy", LifeTime);
    private void ObjDestroy() => Destroy(gameObject);
}
