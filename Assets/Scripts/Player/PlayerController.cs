using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Color classColor;
    private GameManager gameManager;

    private void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        ExitGame();
    }

    private void ExitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            gameManager.ExitGame();
    }

    private void ConsoleLog(string message)
    {
        gameManager.MainConsoleLog($"{message}", classColor);
    }
}