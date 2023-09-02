using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Color classColor;
    public bool consoleLog;
    public float moveSpeed;
    private int score;
    private bool isMoving;
    private float lastTargetDirectionX;
    private float lastTargetDirectionY;
    private Vector2 inputDirection;
    private GameManager gameManager;

    public int GetScore()
    {
        return score;
    }

    private void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        lastTargetDirectionX = 1;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (!isMoving && !gameManager.GetGameOver())
        {
            GetMovementInputs();
            StartCoroutine(Move(ObtainTargetPos()));
        }
    }

    private void GetMovementInputs()
    {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");
        if (inputDirection.x != 0) inputDirection.y = 0;
    }

    IEnumerator Move(Vector3 targetPosition)
    {
        ConsoleLog($"Player Move By ({inputDirection.x},{inputDirection.y}).");
        isMoving = true;
        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
        isMoving = false;
    }

    private Vector3 ObtainTargetPos()
    {
        var targetPosition = transform.position;
        if (WrongTargetDirection())
        {
            ObtainLastTargetDirection();
            ConsoleLog("Target Last Direction.");
        }
        else
        {
            SetLastTargetDirection();
            ConsoleLog("Store Last Direction.");
        }
        targetPosition.x += inputDirection.x;
        targetPosition.y += inputDirection.y;
        return targetPosition;
    }

    private bool WrongTargetDirection()
    {
        return (EmptyInputTargetDirection() || ConflictLastActualTargetDirection());
    }

    private bool EmptyInputTargetDirection()
    {
        return (inputDirection.x == 0 && inputDirection.y == 0);
    }

    private bool ConflictLastActualTargetDirection()
    {
        return (inputDirection.x + lastTargetDirectionX == 0) && (inputDirection.y + lastTargetDirectionY == 0);
    }

    private void ObtainLastTargetDirection()
    {
        inputDirection.x = lastTargetDirectionX;
        inputDirection.y = lastTargetDirectionY;
    }

    private void SetLastTargetDirection()
    {
        lastTargetDirectionX = inputDirection.x;
        lastTargetDirectionY = inputDirection.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Apple"))
        {
            moveSpeed += 0.3f;
            score++;
            ConsoleLog($"Current Move Speed = {moveSpeed}");
            ConsoleLog($"Current Score = {score}");
        }
        else if (collision.CompareTag("Wall"))
        {
            gameManager.GameOver();
        }
    }

    private void ConsoleLog(string message)
    {
        if (consoleLog)
            gameManager.MainConsoleLog($"{message}", classColor);
    }
}