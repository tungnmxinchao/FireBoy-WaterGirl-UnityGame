using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rubyRed : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.CompareTag("FB"))
		{

			Destroy(gameObject);
		}
	}
}
