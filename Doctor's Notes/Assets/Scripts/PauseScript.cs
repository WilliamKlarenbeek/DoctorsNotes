using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject quitMenu;

    private bool paused = false;
    private bool canPause = true;
    // Start is called before the first frame update
    void Start()
    {
        if(pauseMenu == null)
        {
            pauseMenu = transform.Find("PauseMenu").gameObject;
        }
        if (quitMenu == null)
        {
            quitMenu = transform.Find("QuitMenu").gameObject;
        }
        pauseMenu.SetActive(false);
        quitMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
            {
                pauseMenu.SetActive(true);
                paused = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
            {
                pauseMenu.SetActive(false);
                paused = false;
            }
        }

        if (paused)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
    }

    public void ToggleCanPause(bool aFlag)
    {
        canPause = aFlag;
    }

    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        paused = false;
    }

    public void QuitButton()
    {
        CloseQuitMenu();
        ResumeButton();
        StartCoroutine(SceneController.LoadScene("MainMenu", 1f));
    }

    public void OpenQuitMenu()
    {
        quitMenu.SetActive(true);
    }

    public void CloseQuitMenu()
    {
        quitMenu.SetActive(false);
    }
}
