using UnityEngine;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public GameObject settingsPanel;
    private bool settingsIsOpen = false;

    public AudioMixer audioMixer;

    private void Awake()
    {
        settingsPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) OpenCloseSettings();
    }
    public void OpenCloseSettings()
    {
        settingsPanel.SetActive(settingsIsOpen ? false : true);
        settingsIsOpen = settingsIsOpen ? false : true;
        Time.timeScale = settingsIsOpen ? 0 : 1;
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetMusicVolume(float music)
    {
        audioMixer.SetFloat("music", music);
    }
    public void SetVFXVolume(float vfx)
    {
        audioMixer.SetFloat("vfx", vfx);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
