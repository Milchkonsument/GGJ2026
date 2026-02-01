using System;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private GameObject audio1;
    [SerializeField] private GameObject audio2;

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

    private void OnEnable()
    {
        CombatController.Instance.OnCombatStart.AddListener(() =>
        {
            audio1.SetActive(false);
            audio2.SetActive(true);
        });

        CombatController.Instance.OnCombatEnd.AddListener((e) =>
        {
            audio1.SetActive(true);
            audio2.SetActive(false);
        });
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
