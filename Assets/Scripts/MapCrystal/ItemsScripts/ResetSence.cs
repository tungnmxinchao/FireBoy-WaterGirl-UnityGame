using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSence : MonoBehaviour
{

	public void ResetScene()
	{		
		Time.timeScale = 1;
		string currentSceneName = SceneManager.GetActiveScene().name;

		SceneManager.LoadScene(currentSceneName);
	}
}
