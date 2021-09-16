using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Effect
    {
        Chorus,
        Distortion,
        Echo,
        HighPass,
        LowPass,
        Reverb
    }

    private AudioSource Music;
    private List<AudioSource> Channels;
    private bool soundInQueue = false;
    private AudioListener Listener;
    private bool fading = false;

    void Awake()
    {
        Music = gameObject.AddComponent<AudioSource>();
        Channels = new List<AudioSource>();
        Channels.Add(gameObject.AddComponent<AudioSource>());
        Listener = gameObject.AddComponent<AudioListener>();
    }

    public void PlaySound(AudioClip aSound, bool aLoop = false)
    {
        if (aSound != null)
        {
            soundInQueue = true;
            while (soundInQueue)
            {
                foreach (AudioSource channel in Channels)
                {
                    if (!channel.isPlaying)
                    {
                        soundInQueue = false;
                        channel.clip = aSound;
                        channel.loop = aLoop;
                        channel.Play();
                        break;
                    }
                }
                if (soundInQueue)
                {
                    Channels.Add(gameObject.AddComponent<AudioSource>());
                }
            }
        }
    }

    public void PlayMusic(AudioClip aSound = null, bool aLoop = false)
    {
        if (aSound != null)
        {
            Music.clip = aSound;
            Music.loop = aLoop;
        }

        if(Music.clip != null)
        {
            Music.Play();
        }
    }

    public void PauseMusic()
    {
        if (Music.clip != null)
        {
            Music.Pause();
        }
    }

    public void PauseSound(AudioClip aSound)
    {
        foreach (AudioSource channel in Channels)
        {
            if (channel.clip == aSound)
            {
                channel.Pause();
                break;
            }
        }
    }

    public void UnpauseSound(AudioClip aSound)
    {
        foreach (AudioSource channel in Channels)
        {
            if (channel.clip == aSound && channel.isPlaying == false)
            {
                channel.UnPause();
                break;
            }
        }
    }

    public void StopAll(bool unqueue = false)
    {
        Music.Stop();
        foreach (AudioSource channel in Channels)
        {
            channel.Stop();
        }

        if (unqueue)
        {
            Music.clip = null;
            foreach (AudioSource channel in Channels)
            {
                channel.clip = null;
            }
        }
    }

    public void StopMusic(bool unqueue = false)
    {
        if (Music.clip != null)
        {
            Music.Stop();

            if (unqueue)
            {
                Music.clip = null;
            }
        }
    }

    public void StopAllSounds(bool unqueue = false)
    {
        foreach (AudioSource channel in Channels)
        {
            channel.Stop();
            if (unqueue)
            {
                channel.clip = null;
            }
        }
    }

    public void StopSound(AudioClip aSound, bool unqueue = false)
    {
        foreach (AudioSource channel in Channels)
        {
            if (channel.clip == aSound)
            {
                channel.Stop();
                if (unqueue)
                {
                    channel.clip = null;
                }
                break;
            }
        }
    }

    public void ToggleMuteAll()
    {
        if (Listener.enabled)
        {
            Listener.enabled = !Listener.enabled;
        } else
        {
            Listener.enabled = Listener.enabled;
        }
    }

    public void MuteMusic(bool aMute = true)
    {
        Music.mute = aMute;
    }

    public void MuteAllSounds(bool aMute = true)
    {
        foreach (AudioSource channel in Channels)
        {
            channel.mute = aMute;
            break;
        }
    }

    public void MuteSound(AudioClip aSound, bool aMute = true)
    {
        foreach (AudioSource channel in Channels)
        {
            if (channel.clip == aSound)
            {
                channel.mute = aMute;
                break;
            }
        }
    }

    public void AdjustMusicVolume(float aVolume)
    {
        Music.volume = aVolume;
    }

    public void AdjustAllSoundsVolume(float aVolume)
    {
        foreach (AudioSource channel in Channels)
        {
            channel.volume = aVolume;
        }
    }

    public void AdjustSoundVolume(AudioClip aSound, float aVolume)
    {
        foreach (AudioSource channel in Channels)
        {
            if (channel.clip == aSound)
            {
                channel.volume = aVolume;
                break;
            }
        }
    }

    public void PitchMusic(float aPitch)
    {
        Music.pitch = aPitch;
    }

    public void AdjustAllSoundsPitch(float aPitch)
    {
        foreach (AudioSource channel in Channels)
        {
            channel.pitch = aPitch;
        }
    }

    public void AdjustSoundPitch(AudioClip aSound, float aPitch)
    {
        foreach (AudioSource channel in Channels)
        {
            if (channel.clip == aSound)
            {
                channel.pitch = aPitch;
                break;
            }
        }
    }

    public void EffectsBypassMusic(bool aBypass = true)
    {
        Music.bypassEffects = aBypass;
        Music.bypassListenerEffects = aBypass;
    }

    public void EffectsBypassAllSounds(bool aBypass = true)
    {
        foreach (AudioSource channel in Channels)
        {
            channel.bypassEffects = aBypass;
            channel.bypassListenerEffects = aBypass;
        }
    }

    public void EffectsBypassSound(AudioClip aSound, bool aBypass = true)
    {
        foreach (AudioSource channel in Channels)
        {
            if (channel.clip == aSound)
            {
                channel.bypassEffects = aBypass;
                channel.bypassListenerEffects = aBypass;
                break;
            }
        }
    }

    /// <summary>
    /// Chorus = 0
    /// Distortion = 1
    /// Echo = 2
    /// HighPass = 3
    /// LowPass = 4
    /// Reverb = 5
    /// </summary>

    public void RemoveEffect(Effect aEffect)
    {
        switch (aEffect)
        {
            case Effect.Chorus:
                if (gameObject.GetComponent<AudioChorusFilter>() != null)
                {
                    Destroy(gameObject.GetComponent<AudioChorusFilter>());
                }
                break;
            case Effect.Distortion:
                if (gameObject.GetComponent<AudioDistortionFilter>() != null)
                {
                    Destroy(gameObject.GetComponent<AudioDistortionFilter>());
                }
                break;
            case Effect.Echo:
                if (gameObject.GetComponent<AudioEchoFilter>() != null)
                {
                    Destroy(gameObject.GetComponent<AudioEchoFilter>());
                }
                break;
            case Effect.HighPass:
                if (gameObject.GetComponent<AudioHighPassFilter>() != null)
                {
                    Destroy(gameObject.GetComponent<AudioHighPassFilter>());
                }
                break;
            case Effect.LowPass:
                if (gameObject.GetComponent<AudioLowPassFilter>() != null)
                {
                    Destroy(gameObject.GetComponent<AudioLowPassFilter>());
                }
                break;
            case Effect.Reverb:
                if (gameObject.GetComponent<AudioReverbFilter>() != null)
                {
                    Destroy(gameObject.GetComponent<AudioReverbFilter>());
                }
                break;
            default:
                break;
        }
    }

    public void ResetAllEffects()
    {
        if (gameObject.GetComponent<AudioChorusFilter>() != null)
        {
            Destroy(gameObject.GetComponent<AudioChorusFilter>());
        }
        if (gameObject.GetComponent<AudioDistortionFilter>() != null)
        {
            Destroy(gameObject.GetComponent<AudioDistortionFilter>());
        }
        if (gameObject.GetComponent<AudioEchoFilter>() != null)
        {
            Destroy(gameObject.GetComponent<AudioEchoFilter>());
        }
        if (gameObject.GetComponent<AudioHighPassFilter>() != null)
        {
            Destroy(gameObject.GetComponent<AudioHighPassFilter>());
        }
        if (gameObject.GetComponent<AudioLowPassFilter>() != null)
        {
            Destroy(gameObject.GetComponent<AudioLowPassFilter>());
        }
        if (gameObject.GetComponent<AudioReverbFilter>() != null)
        {
            Destroy(gameObject.GetComponent<AudioReverbFilter>());
        }
    }

    // This is where the headache begins.
    public void Chorus(float? aDelay = null, 
        float? aDepth = null, 
        float? aDryMix = null, 
        float? aRate = null, 
        float? aWetMix1 = null, 
        float? aWetMix2 = null, 
        float? aWetMix3 = null)
    {
        if (gameObject.GetComponent<AudioChorusFilter>() == null)
        {
            gameObject.AddComponent<AudioChorusFilter>();
        }

        AudioChorusFilter ChorusComponent = gameObject.GetComponent<AudioChorusFilter>();
        if (aDelay != null)
        {
            ChorusComponent.delay = (float)aDelay;
        }
        if (aDelay != null)
        {
            ChorusComponent.depth = (float)aDepth;
        }
        if (aDelay != null)
        {
            ChorusComponent.dryMix = (float)aDryMix;
        }
        if (aDelay != null)
        {
            ChorusComponent.rate = (float)aRate;
        }
        if (aDelay != null)
        {
            ChorusComponent.wetMix1 = (float)aWetMix1;
        }
        if (aDelay != null)
        {
            ChorusComponent.wetMix2 = (float)aWetMix2;
        }
        if (aDelay != null)
        {
            ChorusComponent.wetMix3 = (float)aWetMix3;
        }
    }

    public void Distortion(float? aDistortionLevel = null)
    {
        if (gameObject.GetComponent<AudioDistortionFilter>() == null)
        {
            gameObject.AddComponent<AudioDistortionFilter>();
        }

        AudioDistortionFilter DistortionComponent = gameObject.GetComponent<AudioDistortionFilter>();
        if (aDistortionLevel != null)
        {
            DistortionComponent.distortionLevel = (float)aDistortionLevel;
        }
    }

    public void Echo(float? aDecayRatio = null,
        float? aDelay = null,
        float? aDryMix = null,
        float? aWetMix = null)
    {
        if (gameObject.GetComponent<AudioEchoFilter>() == null)
        {
            gameObject.AddComponent<AudioEchoFilter>();
        }

        AudioEchoFilter EchoComponent = gameObject.GetComponent<AudioEchoFilter>();
        if (aDecayRatio != null)
        {
            EchoComponent.decayRatio = (float)aDecayRatio;
        }
        if (aDelay != null)
        {
            EchoComponent.delay = (float)aDelay;
        }
        if (aDryMix != null)
        {
            EchoComponent.dryMix = (float)aDryMix;
        }
        if (aWetMix != null)
        {
            EchoComponent.wetMix = (float)aWetMix;
        }
    }

    public void HighPass(float? aCutoffFrequency = null,
        float? aHighpassResonanceQ = null)
    {
        if (gameObject.GetComponent<AudioHighPassFilter>() == null)
        {
            gameObject.AddComponent<AudioHighPassFilter>();
        }

        AudioHighPassFilter HighPassComponent = gameObject.GetComponent<AudioHighPassFilter>();
        if (aCutoffFrequency != null)
        {
            HighPassComponent.cutoffFrequency = (float)aCutoffFrequency;
        }
        if (aHighpassResonanceQ != null)
        {
            HighPassComponent.highpassResonanceQ = (float)aHighpassResonanceQ;
        }
    }

    public void LowPass(AnimationCurve aCustomCutoffCurve = null,
        float? aCutoffFrequency = null,
        float? aLowpassResonanceQ = null)
    {
        if (gameObject.GetComponent<AudioLowPassFilter>() == null)
        {
            gameObject.AddComponent<AudioLowPassFilter>();
        }
        AudioLowPassFilter LowPassComponent = gameObject.GetComponent<AudioLowPassFilter>();
        if(aCustomCutoffCurve != null)
        {
            LowPassComponent.customCutoffCurve = aCustomCutoffCurve;
        }
        if (aCutoffFrequency != null)
        {
            LowPassComponent.cutoffFrequency = (float)aCutoffFrequency;
        }
        if (aLowpassResonanceQ != null)
        {
            LowPassComponent.lowpassResonanceQ = (float)aLowpassResonanceQ;
        }
    }

    //This is where the REAL headache begins.
    public void Reverb(float? aDecayHFRatio = null,
        float? aDecayTime = null,
        float? aDensity = null,
        float? aDiffusion = null,
        float? aDryLevel = null,
        float? aHFReference = null,
        float? aLFReference = null,
        float? aReflectionsDelay = null,
        float? aReflectionsLevel = null,
        float? aReverbDelay = null,
        float? aRoom = null,
        float? aRoomHF = null,
        float? aRoomLF = null)
    {
        if (gameObject.GetComponent<AudioReverbFilter>() == null)
        {
            gameObject.AddComponent<AudioReverbFilter>();
        }

        AudioReverbFilter ReverbComponent = gameObject.GetComponent<AudioReverbFilter>();

        if (aDecayHFRatio != null)
        {
            ReverbComponent.decayHFRatio = (float)aDecayHFRatio;
        }
        if (aDecayTime != null)
        {
            ReverbComponent.decayTime = (float)aDecayTime;
        }
        if (aDensity != null)
        {
            ReverbComponent.density = (float)aDensity;
        }
        if (aDiffusion != null)
        {
            ReverbComponent.diffusion = (float)aDiffusion;
        }
        if (aDryLevel != null)
        {
            ReverbComponent.dryLevel = (float)aDryLevel;
        }
        if (aHFReference != null)
        {
            ReverbComponent.hfReference = (float)aHFReference;
        }
        if (aLFReference != null)
        {
            ReverbComponent.lfReference = (float)aLFReference;
        }
        if (aReflectionsDelay != null)
        {
            ReverbComponent.reflectionsDelay = (float)aReflectionsDelay;
        }
        if (aReflectionsLevel != null)
        {
            ReverbComponent.reflectionsLevel = (float)aReflectionsLevel;
        }
        if (aReverbDelay != null)
        {
            ReverbComponent.reverbDelay = (float)aReverbDelay;
        }
        if (aRoom != null)
        {
            ReverbComponent.room = (float)aRoom;
        }
        if (aRoomHF != null)
        {
            ReverbComponent.roomHF = (float)aRoomHF;
        }
        if (aRoomLF != null)
        {
            ReverbComponent.roomLF = (float)aRoomLF;
        }
    }

    public void FadeOutMusicCoroutine(float aDuration)
    {
        StartCoroutine(FadeOutMusic(aDuration));
    }

    //Coroutines
    public IEnumerator FadeOutMusic(float aDuration)
    {
        if(fading == false)
        {
            fading = true;
            float duration = 0f;
            float initialMusicVolume = Music.volume;

            while (duration < aDuration)
            {
                Music.volume = Mathf.Lerp(initialMusicVolume, 0, duration / aDuration);
                duration += Time.deltaTime;
                yield return null;
            }

            Music.volume = 0f;
            fading = false;
        }
    }

    public IEnumerator FadeInMusic(float aDuration, float aVolume, bool aLoop = false)
    {
        if (fading == false)
        {
            fading = true;
            float duration = 0f;
            if (!Music.isPlaying)
            {
                Music.loop = aLoop;
                Music.Play();
            }

            while (duration < aDuration)
            {
                Music.volume = Mathf.Lerp(0, aVolume, duration / aDuration);
                duration += Time.deltaTime;
                yield return null;
            }

            Music.volume = aVolume;
            fading = false;
        }
    }

    public IEnumerator FadeInSound(AudioClip aSound, float aDuration, float aVolume, bool aLoop = false)
    {
        float duration = 0f;
        if (aSound != null)
        {
            soundInQueue = true;
            while (soundInQueue)
            {
                foreach (AudioSource channel in Channels)
                {
                    if (!channel.isPlaying)
                    {
                        soundInQueue = false;
                        channel.clip = aSound;
                        channel.loop = aLoop;
                        channel.Play();

                        while (duration < aDuration)
                        {
                            channel.volume = Mathf.Lerp(0, aVolume, duration / aDuration);
                            duration += Time.deltaTime;
                            yield return null;
                        }
                        channel.volume = aVolume;

                        break;
                    }
                }
                if (soundInQueue)
                {
                    Channels.Add(gameObject.AddComponent<AudioSource>());
                }
            }
        }
    }

    public IEnumerator FadeOutSound(AudioClip aSound, float aDuration, bool unqueue = false)
    {
        float duration = 0f;
        float initialSoundVolume = 0f;
        foreach (AudioSource channel in Channels)
        {
            if (channel.clip == aSound)
            {
                initialSoundVolume = channel.volume;

                while (duration < aDuration)
                {
                    channel.volume = Mathf.Lerp(initialSoundVolume, 0, duration / aDuration);
                    duration += Time.deltaTime;
                    yield return null;
                }
                channel.volume = 0f;
                channel.Stop();

                if (unqueue)
                {
                    channel.clip = null;
                }
                break;
            }
        }
    }
}
