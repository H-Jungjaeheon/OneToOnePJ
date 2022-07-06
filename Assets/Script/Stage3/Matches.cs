using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matches : MonoBehaviour
{
    [SerializeField] private GameObject ParticleObj;
    private GameObject Handobj = Stage3Manager.Instance.HandObj;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand") && Stage3Manager.Instance.IsTake && Stage3Manager.Instance.IsHandIn)
        {
            Instantiate(ParticleObj, transform.position, ParticleObj.transform.rotation);
            Stage3Manager.Instance.IsHandIn = false;
            Stage3Manager.Instance.HandAnimStart();
            Stage3Manager.Instance.MatchesCountPlus();
        }
    }
}
