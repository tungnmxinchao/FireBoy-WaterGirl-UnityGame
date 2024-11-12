using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GateMap1 : MonoBehaviour
{
    public Animator FireGateOpen;
    public Animator WaterGateOpen;
    public GameObject FB;
    public GameObject WG;
    public GameObject WGFinish;
    public GameObject FBFinish;
    public GameObject SoundWG;
    private Sound soundManager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeFinal;
    public TextMeshProUGUI result;
    int score = 0;
    public GameObject panelWin;
    string timeStringFinal = "";
    private float timeElapsed = 0f;
    public TextMeshProUGUI time;
    public GameObject WaterGateClosed;
    public GameObject FireGateClosed;
    void Start()
    {
        soundManager = SoundWG.GetComponent<Sound>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeStringFinal = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (FireGateOpen.isActiveAndEnabled &&
            FireGateOpen.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f &&
            WaterGateOpen.isActiveAndEnabled &&
            WaterGateOpen.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {

            WG.transform.position = new Vector3(100, 100, 0);
            WGFinish.SetActive(true);

            FB.transform.position = new Vector3(100, 100, 0);
            FBFinish.SetActive(true);

            StartCoroutine(ShowPanelAfterDelay(4f));
        }
    }

    private IEnumerator ShowPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        panelWin.SetActive(true);
        timeFinal.text = "Time:" + timeStringFinal.ToString();
        soundManager.GetRank(soundManager.calculatePoint(false));
        soundManager.StopBlockSound();

        Time.timeScale = 0f;
    }
}
