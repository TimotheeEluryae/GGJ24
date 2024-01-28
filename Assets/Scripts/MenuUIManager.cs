using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public GameObject tutorialPanel;
    public AudioClip buttonSound;

    private bool tutorialIsOpen = false;

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
