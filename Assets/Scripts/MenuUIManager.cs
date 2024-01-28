using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public GameObject tutorialPanel;
    public AudioClip buttonSound;

    private bool tutorialIsOpen = false;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    public void OpenCloseTutorial()
    {
        AudioManager2.instance.PlayClipAt(buttonSound);
        tutorialIsOpen = !tutorialIsOpen;
        tutorialPanel.SetActive(tutorialIsOpen);
    }

    public void Play()
    {
        AudioManager2.instance.PlayClipAt(buttonSound);
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
