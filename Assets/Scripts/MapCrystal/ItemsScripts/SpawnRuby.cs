using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRuby : MonoBehaviour
{
	public GameObject objectToSpawn; 
	public Vector2[] spawnPositions; 

	void Start()
	{
		SpawnObjects();
	}

	void SpawnObjects()
	{
		foreach (Vector2 position in spawnPositions)
		{
			Instantiate(objectToSpawn, position, Quaternion.identity);
		}
	}
}
