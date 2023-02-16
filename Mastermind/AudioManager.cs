using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton Pattern
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else if(Instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    public AudioSource SoundSource;

    public void PlaySound(AudioClip clip)
    {
        SoundSource.clip = clip;
        SoundSource.Play();
    }
}
