using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
                return null;
            else
                return instance;
        }
    }

    public int StageClearCount;
    public StageData[] SD;
    [SerializeField] private GameObject ClickParticle;
    [SerializeField] private Camera MainCam;
    [SerializeField] private Vector3 MousePos;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void FixedUpdate()
    {
        MouseClickParticle();
    }
    private void MouseClickParticle()
    {
        MainCam = Camera.main;
        MousePos = Input.mousePosition;
        MousePos = MainCam.ScreenToWorldPoint(MousePos) + new Vector3(0, 0, 10);
        if (Input.GetMouseButtonDown(0))
        {
            var par = Instantiate(ClickParticle, MousePos, ClickParticle.transform.rotation);
            Destroy(par, 2f);
        }
    }
}
