using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Color classColor;
    public float moveSpeed;
    private bool isMoving;
    private float lastXDir;
    private float lastYDir;
    private Vector2 input;
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
        Movement();
    }

    private void Movement()
    {
        if (!isMoving)
        {
            GetMovementInputs();
            StartCoroutine(Move(ObtainTargetPos()));
        }
    }

    private void GetMovementInputs()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        if (input.x != 0) input.y = 0;
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private Vector3 ObtainTargetPos()
    {
        var targetPos = transform.position;
        if (input.x == 0 && input.y == 0)
        {
            input.x = lastXDir;
            input.y = lastYDir;
        }
        targetPos.x += input.x;
        targetPos.y += input.y;
        lastXDir = input.x;
        lastYDir = input.y;
        return targetPos;
    }

    private void ConsoleLog(string message)
    {
        gameManager.MainConsoleLog($"{message}", classColor);
    }
}