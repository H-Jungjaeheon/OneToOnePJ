using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParticle : MonoBehaviour
{
    public ParticleSystem particle;

    public float _distanceFromCamera = 5f; //ī�޶�κ����� �Ÿ�

    [Range(0.01f, 1.0f)]
    public float _ChasingSpeed = 0.1f;

    private Vector3 _mousePos;
    private Vector3 _nextPos;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    private void OnValidate()
    {
        if (_distanceFromCamera < 0f)
            _distanceFromCamera = 0f;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //print("�巡����");
            particle.Play();
            //particle.loop = true;
            _mousePos = Input.mousePosition;
            _mousePos.z = _distanceFromCamera;

            _nextPos = Camera.main.ScreenToWorldPoint(_mousePos);
            transform.position = Vector3.Lerp(transform.position, _nextPos, _ChasingSpeed);
        }
        else
        {
            //particle.Stop();
            //print("�巡��X");
            //particle.loop = false;
        }
    }
}
