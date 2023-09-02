using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Color classColor;
    public bool consoleLog;
    public bool consoleLogSystem;
    private bool gameOver;
    private PlayerController playerController;

    public bool GetGameOver()
    {
        return gameOver;
    }

    private void Awake()
    {
        ConsoleLog("Starting Game...");
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        ExitGame();
    }

    private void Start()
    {
        gameOver = false;
    }

    public void GameOver()
    {
        gameOver = true;
        ConsoleLog("Game Over.");
        ConsoleLog($"Final Score = {playerController.GetScore()}.");
    }

    private void ExitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ConsoleLog("Closing Game...");
            Application.Quit();
        }
    }

    private void ConsoleLog(string message)
    {
        if (consoleLog)
            MainConsoleLog($"{message}", classColor);
    }

    public void MainConsoleLog(string message, Color classColor, int frame = 1)
    {
        if (consoleLogSystem)
        {
            StackTrace stackTrace = new();
            StackFrame stackFrame = stackTrace.GetFrame(frame);
            string callingScript = stackFrame.GetMethod().DeclaringType.Name;
            string stringClassColor = ("#" + ColorUtility.ToHtmlStringRGBA(classColor));
            UnityEngine.Debug.Log($"<b>[<color={stringClassColor}>{callingScript}</color>]: {message}</b>");
        }
    }
}