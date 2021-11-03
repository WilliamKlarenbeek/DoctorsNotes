using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningSceneHandler : MonoBehaviour
{
    [System.Serializable]
    public struct OpeningDialogue
    {
        public string Dialogue { get { return _Dialogue; } }
        public float Duration { get { return _Duration; } }
        public float CharTransition { get { return _CharTransition; } }
        public AudioClip voiceLine { get { return _voiceLine; } }
        public Image fadeObject { get { return _fadeObject; } }

        [SerializeField] private string _Dialogue;
        [SerializeField] private float _Duration;
        [SerializeField] private float _CharTransition;
        [SerializeField] private AudioClip _voiceLine;
        [SerializeField] private Image _fadeObject;
    }

    public List<GameObject> Lights;
    public GameObject disintegratingMap;
    public GameObject Map;
    public GameObject CameraObject;
    public GameObject DialoguePanel;
    public Text DialogueText;

    private SoundManager sndManager;
    private bool skipFlag;

    [SerializeField] private List<OpeningDialogue> DialogueSequence;

    // Start is called before the first frame update
    void Start()
    {
        Initialise();

        StartCoroutine("OpeningCoroutine");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            skipFlag = true;
        }
        else
        {
            skipFlag = false;
        }
    }

    void Initialise()
    {
        sndManager = GetComponent<SoundManager>();

        foreach (GameObject i in Lights)
        {
            i.SetActive(false);
        }
        disintegratingMap.SetActive(false);
        CameraObject.GetComponent<Animator>().speed = 0;

        Color transparent = new Color(0, 0, 0, 0);
        DialoguePanel.GetComponent<Image>().color = transparent;
        DialogueText.text = "";
        DialoguePanel.SetActive(false);
        DialogueText.gameObject.SetActive(false);
    }

    public IEnumerator OpeningCoroutine()
    {
        float preLoad = 2f;
        float duration = 0f;
        bool dialogueInitiated = false;

        while(duration < preLoad)
        {
            duration += Time.deltaTime;

            yield return null;
        }

        foreach (GameObject i in Lights)
        {
            i.SetActive(true);
        }
        disintegratingMap.SetActive(true);

        CameraObject.GetComponent<Animator>().speed = 1;

        while (duration < 60f + preLoad)
        {
            if(dialogueInitiated == false && duration > 2f + preLoad)
            {
                StartCoroutine("ReadDialogue");
                dialogueInitiated = true;
            }
            duration += Time.deltaTime;

            yield return null;
        }
        disintegratingMap.SetActive(false);
    }

    IEnumerator ReadDialogue()
    {
        float duration = 0f;

        DialoguePanel.SetActive(true);

        while(duration < 2f)
        {
            DialoguePanel.GetComponent<Image>().color = new Color(0,0,0, Mathf.Lerp(0,1f,duration / 2f));
            duration += Time.deltaTime;
            yield return null;
        }
        duration = 0f;
        DialoguePanel.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        DialogueText.gameObject.SetActive(true);

        foreach (OpeningDialogue i in DialogueSequence)
        {
            if (i.voiceLine != null)
            {
                sndManager.PlaySound(i.voiceLine);
            }
            if (i.fadeObject != null)
            {

            }
            StartCoroutine(PrintByCharacter(i));

            while (duration < i.Duration)
            {
                duration += Time.deltaTime;
                if (skipFlag)
                {
                    break;
                }
                yield return null;
            }
            duration = 0f;
            //yield return new WaitForSeconds(i.Duration);

            DialogueText.text = "";
        }
        DialogueText.text = "";
        DialogueText.gameObject.SetActive(false);

        while (duration < 2f)
        {
            DialoguePanel.GetComponent<Image>().color = new Color(0, 0, 0, Mathf.Lerp(1f, 0f, duration / 2f));
            duration += Time.deltaTime;
            yield return null;
        }
        duration = 0f;
        DialoguePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0f);

        StartCoroutine(SceneController.LoadScene("MapScene", 4f));
    }

    IEnumerator PrintByCharacter(OpeningDialogue aDialogue)
    {
        string outStream = "";

        foreach (char i in aDialogue.Dialogue)
        {
            outStream += i;
            DialogueText.text = outStream;

            yield return new WaitForSeconds((aDialogue.CharTransition / aDialogue.Dialogue.Length));
        }
        DialogueText.text = aDialogue.Dialogue;
    }

    IEnumerator FadeInObject(Image aImage, float aDuration)
    {
        float duration = 0f;

        while(duration < aDuration)
        {
            aImage.color = new Color(1f, 1f, 1f, Mathf.Lerp(0, 1f, duration / aDuration));
            duration += Time.deltaTime;
            yield return null;
        }
        aImage.color = new Color(1f, 1f, 1f, 1f);
    }
}
