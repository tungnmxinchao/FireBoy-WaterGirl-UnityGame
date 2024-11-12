using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetWhenWin : MonoBehaviour
{
	public void ResetWhenWinScene()
	{
		Time.timeScale = 1;

		Debug.Log("Da set time scale");
		string currentSceneName = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene(currentSceneName);
	}
}
