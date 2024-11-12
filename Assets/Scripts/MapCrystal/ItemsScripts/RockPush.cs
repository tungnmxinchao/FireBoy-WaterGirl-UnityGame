using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPush : MonoBehaviour
{
    // Tốc độ đẩy của đá
    public float pushForce = 5f;

    // Tham chiếu đến Rigidbody2D
    private Rigidbody2D rb;

    // Tham chiếu đến AudioSource (public để có thể gán từ Inspector)
    public AudioSource audioSource;

    // Kiểm tra trạng thái di chuyển
    private bool isMoving = false;

    void Start()
    {
        // Lấy thành phần Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing on this object.");
        }

        // Kiểm tra nếu audioSource đã được gán từ Inspector
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing or not assigned in the Inspector.");
        }
    }

    void Update()
    {
        // Kiểm tra nếu rock đang di chuyển
        if (rb.velocity.magnitude > 0.1f && !isMoving)
        {
            // Chơi âm thanh
            if (audioSource != null) // Kiểm tra nếu audioSource không phải là null
            {
                audioSource.Play();
            }
            isMoving = true;
        }
        else if (rb.velocity.magnitude <= 0.1f && isMoving)
        {
            // Dừng âm thanh khi rock dừng lại
            if (audioSource != null) // Kiểm tra nếu audioSource không phải là null
            {
                audioSource.Stop();
            }
            isMoving = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem có phải nhân vật WG hoặc FB không
        if (collision.CompareTag("WG") || collision.CompareTag("FB"))
        {
            // Lấy hướng đẩy từ vị trí của nhân vật đến vị trí của Rock
            Vector2 pushDirection = (transform.position - collision.transform.position).normalized;

            // Đẩy Rock
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        }
    }
}
