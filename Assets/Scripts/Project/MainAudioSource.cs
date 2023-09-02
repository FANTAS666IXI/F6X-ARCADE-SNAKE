using UnityEngine;

public class MainAudioSource : MonoBehaviour
{
    public Color classColor;
    public float stepVolumeSize;
    private float volumeRounded;
    private GameManager gameManager;
    private AudioSource audioSource;

    private void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ModifyVolume();
    }

    private void ModifyVolume()
    {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            audioSource.volume += stepVolumeSize;
            ConsoleLog($"Volume Increased By {stepVolumeSize:F2}.");
            ClampVolume();
            ShowVolume();
        }
        else if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            audioSource.volume -= stepVolumeSize;
            ConsoleLog($"Volume Decreased By {stepVolumeSize:F2}.");
            ClampVolume();
            ShowVolume();
        }
    }

    private void ClampVolume()
    {
        audioSource.volume = Mathf.Clamp01(audioSource.volume);
    }

    private void ShowVolume()
    {
        volumeRounded = Mathf.Round(audioSource.volume * 100f) / 100f;
        ConsoleLog($"Current Volume = {volumeRounded:F2} .");
    }

    private void ConsoleLog(string message)
    {
        gameManager.MainConsoleLog($"{message}", classColor);
    }
}