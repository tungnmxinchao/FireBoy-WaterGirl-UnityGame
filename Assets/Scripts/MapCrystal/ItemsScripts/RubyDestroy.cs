using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class RubyDestroy : MonoBehaviour
{
	public GameObject rubyBlue; 
	public GameObject rubyRed;  
	public TextMeshProUGUI rankText; 

	private static int totalDestroyed = 0;
	private int totalRubies = 10;

	void OnTriggerEnter2D(Collider2D other)
	{
		
		Debug.Log("Collider entered: " + other.name + " with tag: " + other.tag);

		if (other.CompareTag("WG")) 
		{
			if (rubyBlue != null)
			{
				Destroy(rubyBlue);
				totalDestroyed += 1;
				Debug.Log("Total Destroyed (WG): " + totalDestroyed);
			}
			else
			{
				Debug.Log("rubyBlue already destroyed or not set.");
			}
		}
		else if (other.CompareTag("FB")) 
		{
			if (rubyRed != null)
			{
				Destroy(rubyRed);
				totalDestroyed += 1;
				Debug.Log("Total Destroyed (FB): " + totalDestroyed);
			}
			else
			{
				Debug.Log("rubyRed already destroyed or not set.");
			}
		}

	
		UpdateRank();
	}

	void UpdateRank()
	{
		float percentageDestroyed = (float)totalDestroyed / totalRubies * 100f;

		if (percentageDestroyed > 70f)
		{
			rankText.text = "Rank: A";
		}
		else if (percentageDestroyed >= 35f)
		{
			rankText.text = "Rank: B";
		}
		else
		{
			rankText.text = "Rank: C";
		}

		Debug.Log("Percentage Destroyed: " + percentageDestroyed + "%, Rank: " + rankText.text);
	}
}
