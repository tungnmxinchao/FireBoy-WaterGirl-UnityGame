using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rubyBlue : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.CompareTag("WG"))
		{

			Destroy(gameObject);
		}
	}
}
