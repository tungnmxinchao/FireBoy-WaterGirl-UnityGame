using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGate : MonoBehaviour
{
    public Animator animator;
    public GameObject FB;
    public GameObject FireGateOb;
    public GameObject FireGateClosed;
    public GameObject FireGateOpening;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        Collider2D collider2 = FireGateOb.GetComponent<Collider2D>();
        Bounds bounds = FB.GetComponent<Collider2D>().bounds;
        Vector2[] corners = new Vector2[4]
        {
        new Vector2(bounds.min.x, bounds.min.y), // Góc dưới trái
        new Vector2(bounds.max.x, bounds.min.y), // Góc dưới phải
        new Vector2(bounds.min.x, bounds.max.y), // Góc trên trái
        new Vector2(bounds.max.x, bounds.max.y)  // Góc trên phải
        };
        bool isInside = true;

        foreach (var corner in corners)
        {
            if (!collider2.OverlapPoint(corner))
            {
                isInside = false;
                break;
            }
        }

        if (isInside)
        {
            FireGateClosed.SetActive(false);
            FireGateOpening.SetActive(true);
        }
        else if(FB.transform.position.x != 100){
            FireGateClosed.SetActive(true);
            FireGateOpening.SetActive(false);
        }
    }
}
