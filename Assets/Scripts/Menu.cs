using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject tutorialPanel;

    private bool settingsIsOpen = false;
    private bool tutorialIsOpen = false;

    public void OpenCloseSettings()
    {
        settingsPanel.SetActive(settingsIsOpen ? false : true);
        settingsIsOpen = settingsIsOpen ? false : true;
    }

    public void OpenCloseTutorial()
    {
        tutorialPanel.SetActive(tutorialIsOpen ? false : true);
        tutorialIsOpen = tutorialIsOpen ? false : true;
    }

    public void Play()
    {
        SceneManager.LoadScene("Game 2");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
