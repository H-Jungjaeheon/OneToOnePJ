using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matches : MonoBehaviour
{
    [SerializeField] private GameObject ParticleObj;
    private GameObject Handobj;

    [Header("사운드 모음")]
    [Space(20)]
    [SerializeField] protected AudioClip GiveHandClip;

    private void Start()
    {
        StartSetting();
    }

    private void StartSetting() 
    {
        Handobj = Stage3Manager.Instance.HandObj;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand") && Stage3Manager.Instance.IsTake && Stage3Manager.Instance.IsHandIn)
        {
            Instantiate(ParticleObj, transform.position, ParticleObj.transform.rotation);
            Stage3Manager.Instance.IsHandIn = false;
            Stage3Manager.Instance.HandAnimStart();
            Stage3Manager.Instance.MatchesCountPlus();
            GiveHandSound();
        }
    }
    protected void GiveHandSound() => SoundManager.Instance.SFXPlay("GiveHand", GiveHandClip);
}
