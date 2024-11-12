using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyBlueSound : MonoBehaviour
{
	public AudioSource audioSource;

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.CompareTag("rubyBlue"))
		{

			if (audioSource != null && !audioSource.isPlaying)
			{
				audioSource.Play();
				Debug.Log("Played sound on rubyBlue collision.");
			}
		}
	}
}
