using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Range(0.0f, 1f)]
    public float musicVolume = 0.25f;

    [SerializeField] private AudioClip SceneMusic;
    private SoundManager sndManager;
    private GameObject blackCanvas;

    void Start()
    {
        if (GetComponent<SoundManager>() != null)
        {
            sndManager = GetComponent<SoundManager>();
        }

        if(sndManager != null)
        {
            sndManager.PlayMusic(SceneMusic, true);
            sndManager.AdjustMusicVolume(musicVolume);
        }

        blackCanvas = Instantiate((GameObject)Resources.Load("Prefabs/UI/BlackCanvas"), transform);
        blackCanvas.SetActive(false);
    }

    public void FadeOutCoroutine(float aDuration)
    {
        blackCanvas.SetActive(true);
        StartCoroutine(FadeOut(aDuration));
        StartCoroutine(sndManager.FadeOutMusic(aDuration));
    }

    IEnumerator FadeOut(float aDuration)
    {
        float frame = 0;
        Image blackScreen = blackCanvas.transform.GetChild(0).gameObject.GetComponent<Image>();
        Color c = blackScreen.color;
        c.a = 0;
        blackScreen.color = c;

        while (frame < aDuration)
        {
            c.a += (1/aDuration) * Time.deltaTime;
            blackScreen.color = c;

            frame += Time.deltaTime;
            yield return null;
        }
    }
}
