using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuOpening : MonoBehaviour
{
    public GameObject OpeningCanvas;
    public GameObject MenuCanvas;
    public Camera MainCamera;

    private GameObject Logo;
    private GameObject BlackPlane;
    private Quaternion TargetRotation;
    private Vector3 TargetPosition;

    void Start()
    {
        Logo = OpeningCanvas.transform.Find("Logo").gameObject;
        BlackPlane = OpeningCanvas.transform.Find("Panel").gameObject;
        TargetRotation = MainCamera.transform.rotation;
        TargetPosition = MainCamera.transform.position;
        MainCamera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, 1f);

        MenuCanvas.SetActive(false);
        OpeningCanvas.SetActive(true);

        StartCoroutine("OpeningTransition");
    }

    IEnumerator OpeningTransition()
    {
        float frame = 0;
        float currentLerpTime = 0f;
        float lerpTime = 3f;
        Image LogoImage = Logo.GetComponent<Image>();
        Color LogoAlpha = LogoImage.color;
        Image BlackImage = BlackPlane.GetComponent<Image>();
        Color BlackAlpha = BlackImage.color;
        Vector3 startPosition = MainCamera.transform.position;
        Quaternion startRotation = MainCamera.transform.rotation;

        LogoAlpha.a = 0;
        LogoImage.color = LogoAlpha;

        BlackAlpha.a = 1;
        BlackImage.color = BlackAlpha;

        //Fade In Logo
        while (frame < 6)
        {
            if(frame <= 2) {
                LogoAlpha.a += (1f / 2f) * Time.deltaTime;
                LogoImage.color = LogoAlpha;
            }

            if(frame >= 3)
            {
                LogoAlpha.a -= (1f / 2f) * Time.deltaTime;
                LogoImage.color = LogoAlpha;

                if (BlackAlpha.a > 0)
                {
                    BlackAlpha.a -= (1f / 1f) * Time.deltaTime;
                    BlackImage.color = BlackAlpha;
                } else
                {
                    BlackAlpha.a = 0;
                }

                currentLerpTime += Time.deltaTime;
                if (currentLerpTime > lerpTime)
                {
                    currentLerpTime = lerpTime;
                }

                float t = currentLerpTime / lerpTime;
                t = Mathf.Sin(t * Mathf.PI * 0.5f);
                MainCamera.transform.rotation = Quaternion.Lerp(startRotation, TargetRotation, t);
                MainCamera.transform.position = Vector3.Lerp(startPosition, TargetPosition, t);
            }

            Logo.GetComponent<RectTransform>().localScale = new Vector3(1f * (1 + (frame / 10)), 1f * (1 + (frame / 10)), 1f);
            frame += Time.deltaTime;

            yield return null;
        }

        MenuCanvas.SetActive(true);
        OpeningCanvas.SetActive(false);
        MainCamera.transform.rotation = TargetRotation;
        MainCamera.transform.position = TargetPosition;
    }
}
