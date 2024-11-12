using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyRedSound : MonoBehaviour
{
	public AudioSource audioSource; 

	void OnTriggerEnter2D(Collider2D other)
	{
	
		if (other.CompareTag("rubyRed"))
		{
			
			if (audioSource != null && !audioSource.isPlaying)
			{
				audioSource.Play();
				Debug.Log("Played sound on rubyRed collision.");
			}
		}
	}
}
