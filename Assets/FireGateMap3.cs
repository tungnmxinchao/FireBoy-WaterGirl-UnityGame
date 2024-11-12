using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGateMap3 : MonoBehaviour
{
    public Animator animator;
    public GameObject FB;
    public GameObject FireGateOb;
    public GameObject FireGateClosed;
    public GameObject FireGateOpening;
    public GameObject FBFinish;

    public static bool fireGateOpened = false;

    void Start()
    {
        fireGateOpened = false;
        FBFinish.SetActive(false); // Ensure FBFinish is inactive at the start
    }

    void Update()
    {
        Collider2D collider2 = FireGateOb.GetComponent<Collider2D>();
        Bounds bounds = FB.GetComponent<Collider2D>().bounds;
        Vector2[] corners = new Vector2[4]
        {
            new Vector2(bounds.min.x, bounds.min.y),
            new Vector2(bounds.max.x, bounds.min.y),
            new Vector2(bounds.min.x, bounds.max.y),
            new Vector2(bounds.max.x, bounds.max.y)
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
            fireGateOpened = true;
        }
        else
        {
            FireGateClosed.SetActive(true);
            FireGateOpening.SetActive(false);
            fireGateOpened = false;
        }

        CheckFinishGame();
    }

    void CheckFinishGame()
    {
        if (fireGateOpened && WaterGateMap3.waterGateOpened)
        {
            FinishGameF();
        }
    }

    void FinishGameF()
    {
        FBFinish.SetActive(true); // Activate FBFinish
        FB.SetActive(false);      // Hide FB

        Debug.Log("Both gates are open - FinishGameF is called.");

        // Start the Win Panel delay coroutine if both gates are open
        if (WaterGateMap3.waterGateOpened)
        {
            FindObjectOfType<GM>().StartCoroutine("ShowWinPanelWithDelay");
        }
    }
}
