using UnityEngine;

public class BackgroundAudioSource : MonoBehaviour
{
    public Color classColor;
    public bool consoleLog;
    public float stepVolumeSize;
    private float volumeRounded;
    private GameManager gameManager;
    private AudioSource backgroundAudioSource;

    private void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        gameManager = FindObjectOfType<GameManager>();
        backgroundAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ModifyVolume();
    }

    private void ModifyVolume()
    {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            backgroundAudioSource.volume += stepVolumeSize;
            ConsoleLog($"Volume Increased By {stepVolumeSize:F2}.");
            ManageVolume();
        }
        else if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            backgroundAudioSource.volume -= stepVolumeSize;
            ConsoleLog($"Volume Decreased By {stepVolumeSize:F2}.");
            ManageVolume();
        }
    }

    private void ManageVolume()
    {
        backgroundAudioSource.volume = Mathf.Clamp01(backgroundAudioSource.volume);
        volumeRounded = Mathf.Round(backgroundAudioSource.volume * 100f) / 100f;
        ConsoleLog($"Current Volume = {volumeRounded:F2}.");
    }

    private void ConsoleLog(string message)
    {
        if (consoleLog)
            gameManager.MainConsoleLog($"{message}", classColor);
    }
}