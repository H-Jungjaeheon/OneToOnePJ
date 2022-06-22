using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private float Speed, HitSpeed;
    private Rigidbody2D rigid;
    private bool IsHit;
    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (!IsHit)
        {
            transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TheLittleMermaid") && !IsHit)
        {
            IsHit = true;
            StartCoroutine(FishHitAnim());
        }
    }
    private IEnumerator FishHitAnim()
    {
        transform.rotation = Quaternion.Euler(0, 0, 180);
        rigid.AddForce(Vector3.up * HitSpeed);
        yield return new WaitForSeconds(0.3f);
        rigid.velocity = Vector3.zero;
        rigid.AddForce(Vector3.down * HitSpeed * 3);
    }
}
