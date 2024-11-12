using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject FB;
    public GameObject WG;
    public float moveSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xWG = 0;
        float xFB = 0;
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            xWG = 1;
            
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            xWG = -1;

        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            xFB = -1;

        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            xFB = 1;

        }
        float moveStep1 = moveSpeed * xWG * Time.deltaTime;
        WG.transform.position += new Vector3(moveStep1, 0, 0);
        float moveStep2 = moveSpeed * xFB * Time.deltaTime;
        FB.transform.position += new Vector3(moveStep2, 0, 0);
    }
}
