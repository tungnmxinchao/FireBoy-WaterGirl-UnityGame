using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
	public GameObject WGFinish;  
	public GameObject winPanel;
	public Animator animator;
	void Start()
	{
		
		winPanel.SetActive(false);
	}

	void Update()
	{
		
		if (WGFinish != null && WGFinish.activeSelf && 
			(animator.isActiveAndEnabled &&
			animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
		{
		
			WinGame();
		}
	}

	void WinGame()
	{

		Time.timeScale = 0;

		
		winPanel.SetActive(true);
	}
}
