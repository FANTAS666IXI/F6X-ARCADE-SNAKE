using UnityEngine;

public class MainAudioSource : MonoBehaviour
{
    public static MainAudioSource Instance;
    private AudioSource mainAudioSource;

    private void Awake()
    {
        ManageInstance();
        InitializeReferences();
    }

    private void ManageInstance()
    {
        Instance = this;
    }

    private void InitializeReferences()
    {
        mainAudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound, float volume = 0.6f)
    {
        mainAudioSource.PlayOneShot(sound, volume);
    }
}