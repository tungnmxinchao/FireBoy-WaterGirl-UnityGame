using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBMove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject FB;
    public float moveSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xFB = 0;
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            xFB = -1;

        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            xFB = 1;

        }
        float moveStep2 = moveSpeed * xFB * Time.deltaTime;
        FB.transform.position += new Vector3(moveStep2, 0, 0);
    }
}
