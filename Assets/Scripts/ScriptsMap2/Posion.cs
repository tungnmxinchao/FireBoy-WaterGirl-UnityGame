﻿using System.Collections;
using UnityEngine;

public class Posion : MonoBehaviour
{
    private UIManager uiManager;
    private AudioManager audioManager;
    public GameObject deadObject;

    // Thời gian chờ sau khi phát hoạt ảnh "thua" trước khi dừng trò chơi
    public float deathAnimationDuration = 1f;
    public GameObject WG;
    public GameObject FB;
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
        if (collision.CompareTag("FB") && !isDead)
        {
            // Gọi hàm xử lý thua
            deadObject.transform.position = FB.transform.position;
            FB.SetActive(false);
            HandleGameOver();
        }
    }

    private void HandleGameOver()
    {
        // Đánh dấu là đã chết
        isDead = true;
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
