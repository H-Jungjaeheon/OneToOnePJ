using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonObj : MonoBehaviour
{
    [SerializeField] private float Speed, RepeatSpeed, RepeatCount, UpCount;
    private float NowRepeatCount;
    [SerializeField] private bool IsUp = true, IsMaxUp;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAnim());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(UpCount);
        IsUp = false;
        yield return null;
    }
    private void Move()
    {
        if (IsUp)
        {
            transform.position += new Vector3(0, Speed * Time.deltaTime, 0);
        }
        else
        {
            if (IsMaxUp)
            {
                transform.position += new Vector3(0, RepeatSpeed * Time.deltaTime, 0);
                if (NowRepeatCount >= RepeatCount)
                {
                    IsMaxUp = false;
                    NowRepeatCount = 0;
                }
            }
            else
            {
                transform.position -= new Vector3(0, RepeatSpeed * Time.deltaTime, 0);
                if (NowRepeatCount >= RepeatCount)
                {
                    IsMaxUp = true;
                    NowRepeatCount = 0;
                }
            }
            NowRepeatCount += Time.deltaTime;
        }
    }
}
