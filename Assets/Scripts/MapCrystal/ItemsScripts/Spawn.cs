using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		GameObject.FindGameObjectWithTag("WG").transform.position = new Vector3(-1.36f, -0.52f, 0);
		GameObject.FindGameObjectWithTag("FB").transform.position = new Vector3(-1.36f, -0.52f, 0);

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
