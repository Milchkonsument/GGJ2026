using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private AudioMixer audioMixer;

    public void AdjustMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    public void AdjustMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void AdjustSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);
    }
}
