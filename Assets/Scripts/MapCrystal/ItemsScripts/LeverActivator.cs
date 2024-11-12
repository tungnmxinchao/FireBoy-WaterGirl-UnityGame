using System.Collections;
using UnityEngine;

public class LeverActivator : MonoBehaviour
{
    public GameObject leverHeadOn;  // Đối tượng sẽ được kích hoạt khi gạt cần
    public GameObject paddle;       // Paddle sẽ di chuyển lên khi gạt cần
    public AudioSource leverSound;  // Tham chiếu đến AudioSource cho âm thanh

    private Vector3 originalPaddlePosition;  // Vị trí ban đầu của paddle
    public float liftDepthPaddle = 0.3f;     // Khoảng cách di chuyển của paddle
    public float liftSpeed = 2.0f;           // Tốc độ di chuyển của paddle

    private Renderer[] leverRenderers;       // Lưu trữ các renderers của lever và các object con
    private bool isActive = false;            // Biến để theo dõi trạng thái của lever

    void Start()
    {
        // Lấy vị trí ban đầu của paddle
        if (paddle != null)
        {
            originalPaddlePosition = paddle.transform.position;
        }
        else
        {
            Debug.LogError("Paddle object not assigned!");
        }

        // Đảm bảo leverHeadOn được tắt ban đầu
        if (leverHeadOn != null)
        {
            leverHeadOn.SetActive(false);
        }

        // Lấy tất cả Renderers của lever và các object con
        leverRenderers = GetComponentsInChildren<Renderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive && (collision.CompareTag("WG") || collision.CompareTag("FB")))
        {
            isActive = true; // Đánh dấu lever là đã được kích hoạt

            if (leverHeadOn != null)
            {
                leverHeadOn.SetActive(true); // Kích hoạt đối tượng leverHeadOn
            }

            // Ẩn lever cùng tất cả các object con của nó
            foreach (Renderer renderer in leverRenderers)
            {
                renderer.enabled = false;
            }

            // Phát âm thanh khi lever được kích hoạt
            if (leverSound != null)
            {
                leverSound.Play(); // Phát âm thanh
            }

            // Bắt đầu coroutine để di chuyển paddle
            StartCoroutine(MovePaddle());
        }
    }

    IEnumerator MovePaddle()
    {
        Vector3 liftedPaddlePosition = new Vector3(originalPaddlePosition.x, originalPaddlePosition.y + liftDepthPaddle, originalPaddlePosition.z);

        // Di chuyển paddle lên trên
        while (Vector3.Distance(paddle.transform.position, liftedPaddlePosition) > 0.01f)
        {
            paddle.transform.position = Vector3.Lerp(paddle.transform.position, liftedPaddlePosition, Time.deltaTime * liftSpeed);
            yield return null;
        }

        // Đảm bảo paddle đạt vị trí cuối cùng
        paddle.transform.position = liftedPaddlePosition;
    }
}
