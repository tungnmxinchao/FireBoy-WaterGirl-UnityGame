using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WGMove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject WG;
    public float moveSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xWG = 0;
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            xWG = 1;

        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            xWG = -1;

        }
        float moveStep1 = moveSpeed * xWG * Time.deltaTime;
        WG.transform.position += new Vector3(moveStep1, 0, 0);

    }
}
