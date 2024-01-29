using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject recipePanel;

    public Slider masterSlider;
    public Slider musiclider;
    public Slider vfxSlider;

    public Animator recipePanelAnimator;

    private bool settingsIsOpen = false;
    private bool recipeIsOpen = false;

    public AudioMixer audioMixer;

    private void Awake()
    {
        settingsPanel.SetActive(false);
        print(recipePanel.transform.position.x);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) OpenCloseSettings();
        if (Input.GetKeyDown(KeyCode.Tab)) OpenCloseRecipe();
    }
    public void OpenCloseSettings()
    {
        settingsPanel.SetActive(settingsIsOpen ? false : true);
        settingsIsOpen = settingsIsOpen ? false : true;
        Time.timeScale = settingsIsOpen ? 0 : 1;
    }

    public void SetMasterVolume(float volume)
    {
        float dB = masterSlider.value;

        if (volume != 0)
            dB = 20.0f * Mathf.Log10(volume);
        else
            dB = -144.0f;
        audioMixer.SetFloat("volume", dB);
    }
    public void SetMusicVolume(float music)
    {
        float dB = musiclider.value;

        if (music != 0)
            dB = 20.0f * Mathf.Log10(music);
        else
            dB = -144.0f;
        audioMixer.SetFloat("music", dB);
    }
    public void SetVFXVolume(float vfx)
    {
        float dB = vfxSlider.value;

        if (vfx != 0)
            dB = 20.0f * Mathf.Log10(vfx);
        else
            dB = -144.0f;
        audioMixer.SetFloat("vfx", dB);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void OpenCloseRecipe()
    {
        if(StaticVariable.canUseShortkey) 
        {
            recipeIsOpen = !recipeIsOpen;
            recipePanelAnimator.SetBool("IsOpen", recipeIsOpen);

        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
