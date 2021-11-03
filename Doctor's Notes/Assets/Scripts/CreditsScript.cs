using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    public float YStartingPosition = -1000;
    public float YEndingPosition = 1500;
    public float scrollDuration = 30;
    public Image endingImage;
    public Text endingText;

    private GameObject creditsObject;
    private float endingAlpha = 0f;
    private bool skipFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        if(Checker() == true)
        {
            endingImage.color = new Color(endingImage.color.r, endingImage.color.g, endingImage.color.b, endingAlpha);
            endingText.color = new Color(endingText.color.r, endingText.color.g, endingText.color.b, endingAlpha);
            endingImage.gameObject.SetActive(false);
            endingText.gameObject.SetActive(false);

            creditsObject = transform.Find("Credits").gameObject;
            StartCoroutine("CreditsSequence");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            skipFlag = true;
        } else
        {
            skipFlag = false;
        }
    }

    bool Checker()
    {
        if (endingImage != null || endingText != null || creditsObject != null)
        {
            return true;
        } 
        else
        {
            return false;
        }
    }

    IEnumerator CreditsSequence()
    {
        float duration = 0f;
        float newYValue = YStartingPosition;

        while(duration < scrollDuration)
        {
            newYValue = Mathf.Lerp(YStartingPosition, YEndingPosition, duration/scrollDuration);
            creditsObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(creditsObject.GetComponent<RectTransform>().anchoredPosition.x, newYValue);

            duration += Time.deltaTime * (skipFlag ? 10 : 1);
            yield return null;
        }
        duration = 0f;
        endingImage.gameObject.SetActive(true);
        endingText.gameObject.SetActive(true);

        while (duration < 5f)
        {
            endingAlpha = Mathf.Lerp(0f, 1f, duration / 5f);
            endingImage.color = new Color(endingImage.color.r, endingImage.color.g, endingImage.color.b, endingAlpha);
            endingText.color = new Color(endingText.color.r, endingText.color.g, endingText.color.b, endingAlpha);

            duration += Time.deltaTime;
            yield return null;
        }
        endingImage.color = new Color(endingImage.color.r, endingImage.color.g, endingImage.color.b, 1f);
        endingText.color = new Color(endingText.color.r, endingText.color.g, endingText.color.b, 1f);

        while (!Input.anyKey)
        {
            yield return null;
        }
        duration = 0f;

        while (duration < 5f)
        {
            endingAlpha = Mathf.Lerp(1f, 0f, duration / 5f);
            endingImage.color = new Color(endingImage.color.r, endingImage.color.g, endingImage.color.b, endingAlpha);
            endingText.color = new Color(endingText.color.r, endingText.color.g, endingText.color.b, endingAlpha);

            duration += Time.deltaTime;
            yield return null;
        }
        duration = 0f;
        endingImage.color = new Color(endingImage.color.r, endingImage.color.g, endingImage.color.b, 0f);
        endingText.color = new Color(endingText.color.r, endingText.color.g, endingText.color.b, 0f);

        StartCoroutine(SceneController.LoadScene("MainMenu", 1f));
    }
}