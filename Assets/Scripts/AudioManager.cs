using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioMixer audioMixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAudioSettings();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(Mathf.Max(0.0001f, volume)) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(0.0001f, volume)) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public float GetBGMVolume()
    {
        float volume = 1f;
        audioMixer.GetFloat("BGMVolume", out float mixerVolume);
        volume = Mathf.Pow(10, mixerVolume / 20);
        return volume;
    }

    public float GetSFXVolume()
    {
        float volume = 1f;
        audioMixer.GetFloat("SFXVolume", out float mixerVolume);
        volume = Mathf.Pow(10, mixerVolume / 20);
        return volume;
    }

    private void LoadAudioSettings()
    {
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetBGMVolume(bgmVolume);
        SetSFXVolume(sfxVolume);
    }
}
