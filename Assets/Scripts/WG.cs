using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG : MonoBehaviour
{
    public GameObject WGMoveLeft;
    public GameObject WGMoveRight;
    public GameObject WGStand;
    public GameObject WGHDrop;
    public GameObject WGHStand;
    float xDir;
    void Start()
    {
        WGStand.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
        if ((!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)))
        {
            WGMoveLeft.SetActive(false);
            WGMoveRight.SetActive(false);
            WGStand.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            WGMoveLeft.SetActive(true);
            WGMoveRight.SetActive(false);
            WGStand.SetActive(false);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            WGMoveLeft.SetActive(false);
            WGMoveRight.SetActive(true);
            WGStand.SetActive(false);
        }

    }
    
}
