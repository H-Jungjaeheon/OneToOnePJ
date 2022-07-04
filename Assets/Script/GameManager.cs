using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    public int StageClearCount;
    public StageData[] SD;
    [SerializeField] private GameObject ClickParticle;
    [SerializeField] private Camera MainCam;
    [SerializeField] private Vector3 MousePos;

    private void Awake()
    {
        var obj = FindObjectsOfType<GameManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
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
            Instantiate(ClickParticle, MousePos, ClickParticle.transform.rotation);
        }
    }
}
