using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGMusic;
    public AudioClip[] BGList;
    [SerializeField] private int[] SceneCount;
    public static SoundManager Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
           // SoundManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int a = 0; a < BGList.Length; a++)
        {
            if (SceneManager.GetActiveScene().buildIndex == SceneCount[a])
                BGMusicPlay(BGList[a]);
        }
    }
    public void SFXPlay(string SFXName, AudioClip Clip)
    {
        GameObject GO = new GameObject($"{SFXName}Sound");
        AudioSource audiosource = GO.AddComponent<AudioSource>();
        audiosource.clip = Clip;
        audiosource.Play();

        Destroy(GO, Clip.length);
    }
    public void BGMusicPlay(AudioClip clip)
    {
        BGMusic.clip = clip;
        BGMusic.loop = true;
        BGMusic.volume = 0.1f;
        BGMusic.Play();
    }
}
