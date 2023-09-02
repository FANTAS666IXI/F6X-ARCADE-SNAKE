﻿using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Color classColor;
    public bool consoleLogSystem;

    private void Start()
    {
        ConsoleLog("Starting Game...");
    }

    private void Update()
    {
        ExitGame();
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