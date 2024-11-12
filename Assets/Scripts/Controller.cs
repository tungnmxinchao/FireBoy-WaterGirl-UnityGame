using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public GameObject panelSetting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchSceneMap1()
    {
        Time.timeScale = 1f;
		SceneManager.LoadScene("Map 1");
    }

    public void SwitchSceneMap2()
    {
        SceneManager.LoadScene("Map2");
    }

    public void SwitchSceneMap3()
    {
        SceneManager.LoadScene("Map 3");
    }
    public void SwitchSceneMap4()
    {
		SceneManager.LoadScene("CrystalMap");
    }

    public void SwitchSceneHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Home");
    }

    public void SwitchSceneNextLV2()
    {
        SceneManager.LoadScene("Map 2");
    }

    public void ClickSetting()
    {
            Time.timeScale = 0f;
            panelSetting.SetActive(true);
    }
    public void ClickCountinue()
    {
    Time.timeScale = 1f;
        panelSetting.SetActive(false);

    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
