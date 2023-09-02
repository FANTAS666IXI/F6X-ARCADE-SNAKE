using UnityEngine;

public class WallController : MonoBehaviour
{
    public AudioClip soundGameOver;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            MainAudioSource.Instance.PlaySound(soundGameOver, 0.4f);
    }
}