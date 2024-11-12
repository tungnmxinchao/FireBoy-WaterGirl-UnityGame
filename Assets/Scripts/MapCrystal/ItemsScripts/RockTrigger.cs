using UnityEngine;

public class RockTrigger : MonoBehaviour
{
    public AudioSource audioSource; // Kéo thả AudioSource vào đây
    private bool isPlaying = false; // Biến để theo dõi trạng thái âm thanh

    void OnCollisionStay(Collision collision)
    {
        // Kiểm tra nếu đối tượng va chạm có tag WG hoặc FB
        if (collision.gameObject.CompareTag("WG") || collision.gameObject.CompareTag("FB"))
        {
            // Nếu âm thanh chưa phát, phát âm thanh
            if (!isPlaying)
            {
                PlaySound(); // Phát âm thanh
                isPlaying = true; // Đánh dấu là âm thanh đang phát
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Khi người chơi rời khỏi va chạm, dừng âm thanh
        if (collision.gameObject.CompareTag("WG") || collision.gameObject.CompareTag("FB"))
        {
            StopSound(); // Dừng âm thanh
            isPlaying = false; // Đánh dấu là âm thanh không còn phát
        }
    }

    void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play(); // Phát âm thanh
        }
    }

    void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // Dừng âm thanh
        }
    }
}
