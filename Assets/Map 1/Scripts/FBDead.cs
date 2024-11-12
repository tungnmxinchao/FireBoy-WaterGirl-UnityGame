using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FBDead : MonoBehaviour
{
    public GameObject WaterTouch;
    public GameObject Dead;
    public GameObject Sound;
    private Sound soundManager;

    public GameObject panelLose;
    public TextMeshProUGUI timeFinal;
    public TextMeshProUGUI result;
    public TextMeshProUGUI time;
    private float timeElapsed = 0f;
    string timeStringFinal = "";
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = Sound.GetComponent<Sound>();
        player = GameObject.FindGameObjectWithTag("FB");
    }

    // Update is called once per frame
    void Update()
    {

        timeElapsed += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeStringFinal = string.Format("{0:00}:{1:00}", minutes, seconds);
        Collider2D collider2 = WaterTouch.GetComponent<Collider2D>();
        Bounds FBbounds = GameObject.FindGameObjectWithTag("FB").GetComponent<Collider2D>().bounds;
        Vector2[] FBcorners = new Vector2[4]
        {
        new Vector2(FBbounds.min.x, FBbounds.min.y), // Góc dưới trái
        new Vector2(FBbounds.max.x, FBbounds.min.y), // Góc dưới phải
        new Vector2(FBbounds.min.x, FBbounds.max.y), // Góc trên trái
        new Vector2(FBbounds.max.x, FBbounds.max.y)  // Góc trên phải
        };
        bool FBIsTouching = false;
      
        foreach (var corner in FBcorners)
        {
            if (collider2.OverlapPoint(corner))
            {
                FBIsTouching = true;
                break;
            }
        }
        if (FBIsTouching)
        {
            soundManager.DieSound();
            Dead.transform.position = GameObject.FindGameObjectWithTag("FB").transform.position;
            if (player != null)
            {
                player.transform.position = new Vector3(100f, 100f, 0f);
            }
            Dead.SetActive(true);
            StartCoroutine(ShowPanelAfterDelay(2f));
        }
    }

    private IEnumerator ShowPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        panelLose.SetActive(true);
        timeFinal.text = "Time:" + timeStringFinal.ToString();
        Time.timeScale = 0f;
    }
}
