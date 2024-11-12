using System.Collections;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private UIManager uiManager; 
    public GameObject deadObject;
    private AudioManager audioManager;
    // Thời gian chờ sau khi phát hoạt ảnh "thua" trước khi dừng trò chơi
    public float deathAnimationDuration = 1f;
    public GameObject WG;
    // Trạng thái để xác định xem đã chết hay chưa
    private bool isDead = false;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        audioManager = FindObjectOfType<AudioManager>();
        deadObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WG") && !isDead)
        {
            // Gọi hàm xử lý thua
            deadObject.transform.position = WG.transform.position;
            WG.SetActive(false);
            HandleGameOver();
        }
    }

    private void HandleGameOver()
    {
        // Đánh dấu là đã chết
        isDead = true;

        // Phát hoạt ảnh thua
        //if (playerAnimator != null)
        //{
        //    playerAnimator.SetTrigger("Die"); // "Die" là tên trigger trong Animator
        //}

        // Gọi coroutine để chờ cho đến khi hoạt ảnh kết thúc
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        // Chờ một khoảng thời gian để hoạt ảnh hoàn thành
        audioManager.PlaySFX(audioManager.death);
        deadObject.SetActive(true);
        yield return new WaitForSeconds(deathAnimationDuration);

        uiManager.GameOver();

    }
}
