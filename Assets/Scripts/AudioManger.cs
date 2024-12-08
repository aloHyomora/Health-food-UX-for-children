using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger : MonoBehaviour
{
    public static AudioManger Instance;

    public AudioSource sfxSource;

    public AudioClip countDownClip;
    public AudioClip gameOverClip;
    public AudioClip levelUpClip;
    public AudioClip gameClearClip;
    public AudioClip startClip;
    public AudioClip goodFoodClip;
    public AudioClip badFoodClip;   

    private void Awake()
    {
        if(Instance == null) Instance = this;

        sfxSource = GetComponent<AudioSource>();
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
