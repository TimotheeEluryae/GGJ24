using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager2 : MonoBehaviour
{
    public AudioClip[] playlist;
    int currentMusicIndex;
    AudioSource audioSource;

    public AudioMixerGroup soundMixerGroup;

    public AudioMixer audioMixer;

    public static AudioManager2 instance;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        instance = this;
    }

    private void Start()
    {
        currentMusicIndex = 0;
        audioSource.clip = playlist[currentMusicIndex];
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying && playlist.Length != 0)
        {
            currentMusicIndex = (currentMusicIndex + 1) % playlist.Length;
            audioSource.clip = playlist[currentMusicIndex];
            audioSource.Play();
        }
    }

    public void PlayClipAt(AudioClip clip)
    {
        AudioSource tmpAudioClip = new GameObject("tmp Audio Go").AddComponent<AudioSource>();
        tmpAudioClip.spatialBlend = 0;
        tmpAudioClip.outputAudioMixerGroup = soundMixerGroup;
        tmpAudioClip.clip = clip;
        tmpAudioClip.Play();
        Destroy(tmpAudioClip.gameObject, clip.length);
    }
}