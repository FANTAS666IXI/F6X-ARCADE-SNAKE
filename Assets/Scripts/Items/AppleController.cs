using UnityEngine;

public class AppleController : MonoBehaviour
{
    public Color classColor;
    public bool consoleLog;
    public AudioClip soundCollected;
    private GameManager gameManager;

    private void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ConsoleLog("Collected.");
            MainAudioSource.Instance.PlaySound(soundCollected);
            Destroy(gameObject);
        }
    }

    private void ConsoleLog(string message)
    {
        if (consoleLog)
            gameManager.MainConsoleLog($"{message}", classColor);
    }
}