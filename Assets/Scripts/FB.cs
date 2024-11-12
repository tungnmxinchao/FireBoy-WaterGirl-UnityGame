using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB : MonoBehaviour
{
    public GameObject FBMoveLeft;
    public GameObject FBMoveRight;
    public GameObject FBStand;

    float xDir;
    void Start()
    {
        FBStand.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)))
        {
            FBMoveLeft.SetActive(false);
            FBMoveRight.SetActive(false);
            FBStand.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            FBMoveLeft.SetActive(true);
            FBMoveRight.SetActive(false);
            FBStand.SetActive(false);

        }
        else if(Input.GetKey(KeyCode.D))
        {
            FBMoveLeft.SetActive(false);
            FBMoveRight.SetActive(true);
            FBStand.SetActive(false);
        }
    }

}
