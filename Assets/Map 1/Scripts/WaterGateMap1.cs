using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterGateMap1 : MonoBehaviour
{
    public Animator animator;
    public GameObject WG;
    public GameObject WaterGateOb;
    public GameObject WaterGateClosed;
    public GameObject WaterGateOpening;
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

        Collider2D collider2 = WaterGateOb.GetComponent<Collider2D>();
        Bounds bounds = WG.GetComponent<Collider2D>().bounds;
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
            WaterGateClosed.SetActive(false);
            WaterGateOpening.SetActive(true);
            if (!wasInside)
            {
                soundManager.BlockSound(); 
                wasInside = true; 
            }
        }
        else if (WG.transform.position.x != 100)
        {


            WaterGateClosed.SetActive(true);
            WaterGateOpening.SetActive(false);
            wasInside = false;
        }
    }
}
