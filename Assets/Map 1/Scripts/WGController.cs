using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WGController : MonoBehaviour
{
    public GameObject BarBot;
    public GameObject WiperLeft;
    public GameObject WiperRight;
    public bool isWiperRightActive = true;
    private bool isMoving;
    public float pushForce = 2f; 
    private bool hasTriggered = false;

    public GameObject SoundWG;
    public GameObject WDiamond;
    private Sound soundManager;
    private Rigidbody2D blockRigidbody;
    public TextMeshProUGUI time;
    public TextMeshProUGUI guildMoveWG;
    public TextMeshProUGUI guildMoveFB;
    public TextMeshProUGUI guildWiper;
    public TextMeshProUGUI guildButton;
    public TextMeshProUGUI guildPush;
    public TextMeshProUGUI guildGate;
    public TextMeshProUGUI mix;
    public TextMeshProUGUI greenGoo;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeFinal;
    public TextMeshProUGUI result;
    int score = 0;
    public GameObject panelLose;
    public GameObject panelWin;
    private float timeElapsed = 0f;
    string timeStringFinal = "";
    public Button retryButton;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = SoundWG.GetComponent<Sound>();
        if (WiperLeft != null)
        {
            WiperLeft.SetActive(false);
        }

        retryButton.onClick.AddListener(RestartGame);
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeStringFinal = string.Format("{0:00}:{1:00}", minutes, seconds);
        GameObject btnUpObject = GameObject.FindWithTag("BarUp");
        float BarUpYPosition = btnUpObject.transform.position.y;
        if (transform.position.x > 3.8f && transform.position.x < 4.2f && transform.position.y < 1.9f && transform.position.y > 1.55f)
        {
            if (!hasTriggered && BarUpYPosition == 0.5f)
            {
                soundManager.ButtonSound();
                StartCoroutine(ReduceHeight("BarUp", -0.5f, 2f));
                StartCoroutine(ReduceHeight("BtnUp", 1.1f, 2f));
                hasTriggered = true;
            }
        }
        else
        {
            if (hasTriggered && BarUpYPosition == -0.5f)
            {
                soundManager.ButtonSound();
                StartCoroutine(ReduceHeight("BtnUp", 1.2f, 2f));
                StartCoroutine(ReduceHeight("BarUp", 0.5f, 2f));
                hasTriggered = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TriggerAreaMix"))
        {
            mix.gameObject.SetActive(true); 
        }

        if (other.CompareTag("TriggerGreenGoo"))
        {
            greenGoo.gameObject.SetActive(true); 
        }

        if (other.CompareTag("WDiamond")) 
        {
            soundManager.DiamondSound();
            other.gameObject.SetActive(false);
            soundManager.calculatePoint(true);
        }

        if (other.CompareTag("TriggerSwiper"))
        {
            guildWiper.gameObject.SetActive(true);
        }

        if (other.CompareTag("TriggerButton"))
        {
            guildButton.gameObject.SetActive(true);
        }

        if (other.CompareTag("TriggerBlock"))
        {
            guildPush.gameObject.SetActive(true);
        }

        if (other.CompareTag("TriggerGate"))
        {
            guildGate.gameObject.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TriggerAreaMix"))
        {
            mix.gameObject.SetActive(false); 
        }

        if (other.CompareTag("TriggerGreenGoo")) 
        {
            greenGoo.gameObject.SetActive(false); 
        }

        if (other.CompareTag("TriggerStart"))
        {
            guildMoveWG.gameObject.SetActive(false);
            guildMoveFB.gameObject.SetActive(false);
        }

        if (other.CompareTag("TriggerSwiper"))
        {
            guildWiper.gameObject.SetActive(false);
        }

        if (other.CompareTag("TriggerButton"))
        {
            guildButton.gameObject.SetActive(false);
        }

        if (other.CompareTag("TriggerBlock"))
        {
            guildPush.gameObject.SetActive(false);
        }

        if (other.CompareTag("TriggerGate"))
        {
            guildGate.gameObject.SetActive(false);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WiperRight"))
        {
            if (isWiperRightActive && !isMoving)
            {
                soundManager.SwiperSound();
                WiperRight.SetActive(false);
                WiperLeft.SetActive(true);
                StartCoroutine(ReduceHeight("BarBot", -1.45f, 2f));
                isWiperRightActive = false; // Cập nhật trạng thái
                isMoving = true;

            }
        }
        else if (collision.gameObject.CompareTag("WiperLeft"))
        {
            if (!isWiperRightActive && !isMoving)
            {
                soundManager.SwiperSound();
                WiperLeft.SetActive(false);
                WiperRight.SetActive(true);
                StartCoroutine(ReduceHeight("BarBot", -0.5f, 2f));
                isWiperRightActive = true; // Cập nhật trạng thái
                isMoving = true;

            }
        }

        if (collision.gameObject.CompareTag("Block"))
        {
            blockRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (blockRigidbody != null)
                {
                    soundManager.BlockSound();

                    Vector2 pushDirection = Vector2.left; // Đẩy sang trái
                    blockRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                }

            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (blockRigidbody != null)
                {
                    soundManager.BlockSound();
                    Vector2 pushDirection = Vector2.right; // Đẩy sang phải
                    blockRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                }
            }
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            blockRigidbody = null;
            soundManager.StopBlockSound();
        }
    }
    private IEnumerator ReduceHeight(string tag, float targetY, float duration)
    {
        // Tìm đối tượng có tag "BarBot"
        GameObject barBot = GameObject.FindWithTag(tag);
        if (barBot != null)
        {
            Vector3 startPosition = barBot.transform.position;
            float startY = startPosition.y; // Vị trí Y hiện tại
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float newY = Mathf.Lerp(startY, targetY, elapsedTime / duration);
                barBot.transform.position = new Vector3(startPosition.x, newY, startPosition.z);

                elapsedTime += Time.deltaTime; // Cập nhật thời gian đã trôi qua
                yield return null; // Chờ đến frame tiếp theo
            }

            // Đảm bảo vị trí Y đạt đến mục tiêu
            barBot.transform.position = new Vector3(startPosition.x, targetY, startPosition.z);
        }
        else
        {
            Debug.LogWarning("Không tìm thấy đối tượng có tag " + tag);
        }

        isMoving = false;
    }
}
