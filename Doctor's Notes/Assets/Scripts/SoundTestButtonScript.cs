using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTestButtonScript : MonoBehaviour
{
    public bool PlayButton;
    public bool PauseButton;
    public bool UnPauseButton;
    public bool StopButton;
    public bool Chorus;
    public bool Distortion;
    public bool Echo;
    public bool Highpass;
    public bool Lowpass;
    public bool Reverb;
    public bool FadeIn;
    public bool FadeOut;
    public bool Reset;
    public AudioClip Audio;
    public GameObject Controller;

    private SoundManager sndManager;

    void Start()
    {
        sndManager = Controller.GetComponent<SoundManager>();
    }

    public void ButtonClick()
    {
        if (PlayButton)
        {
            sndManager.PlaySound(Audio);
        }
        if (PauseButton)
        {
            sndManager.PauseSound(Audio);
        }
        if (StopButton)
        {
            sndManager.StopSound(Audio);
        }
        if (Chorus)
        {
            sndManager.Chorus();
        }
        if (Distortion)
        {
            sndManager.Distortion();
        }
        if (Echo)
        {
            sndManager.Echo();
        }
        if (Highpass)
        {
            sndManager.HighPass();
        }
        if (Lowpass)
        {
            sndManager.LowPass();
        }
        if (Reverb)
        {
            sndManager.Reverb();
        }
        if (FadeIn)
        {
            sndManager.PlayMusic(Audio);
            StartCoroutine(sndManager.FadeInMusic(4, 1));
        }
        if (FadeOut)
        {
            StartCoroutine(sndManager.FadeOutMusic(4));
        }
        if (Reset)
        {
            sndManager.ResetAllEffects();
        }
    }
}
