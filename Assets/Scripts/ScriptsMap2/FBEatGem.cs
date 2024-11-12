using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBEatGem : MonoBehaviour
{
    private UIManager uiManager;
    private AudioManager audioManager;
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();  // Tìm UIManager trong scene
        audioManager = FindObjectOfType<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu gem có tag là FireGem
        if (other.CompareTag("FireGem"))
        {
            uiManager.CollectGem("FireGem");
            audioManager.PlaySFX(audioManager.eatGem);
            other.gameObject.SetActive(false);  
        }
    }
}
