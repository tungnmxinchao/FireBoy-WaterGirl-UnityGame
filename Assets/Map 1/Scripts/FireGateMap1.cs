using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGateMap1 : MonoBehaviour
{
    public Animator animator;
    public GameObject FB;
    public GameObject FireGateOb;
    public GameObject FireGateClosed;
    public GameObject FireGateOpening;
    public GameObject SoundWG;
    private Sound soundManager;
    private bool wasInside = false;

    void Start()
    {
        soundManager = SoundWG.GetComponent<Sound>();

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
            if (!wasInside)
            {
                soundManager.BlockSound();
                wasInside = true;
            }
        }
        else if (FB.transform.position.x != 100)
        {

            FireGateClosed.SetActive(true);
            FireGateOpening.SetActive(false);
            wasInside = false;

        }


    }
}
