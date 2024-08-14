using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button playButton;
    public Button exitButton;

    [Header("Audio Settings")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        InitializeButtons();
        InitializeAudioSettings();
    }

    private void InitializeButtons()
    {
        if (playButton != null)
        {
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(StartGame);
        }

        if (exitButton != null)
        {
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    private void InitializeAudioSettings()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager instance not found!");
            return;
        }

        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.RemoveAllListeners();
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
            bgmSlider.value = AudioManager.Instance.GetBGMVolume();
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
            sfxSlider.value = AudioManager.Instance.GetSFXVolume();
        }
    }

    private void StartGame()
    {
        if (GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.LoadLevel1();
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }

    private void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void SetBGMVolume(float volume)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetBGMVolume(volume);
        }
    }

    private void SetSFXVolume(float volume)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSFXVolume(volume);
        }
    }
}
