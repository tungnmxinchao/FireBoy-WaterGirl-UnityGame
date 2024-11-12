using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterGateMap3 : MonoBehaviour
{
    public Animator animator;
    public GameObject WG;
    public GameObject WaterGateOb;
    public GameObject WaterGateClosed;
    public GameObject WaterGateOpening;
    public GameObject WGFinish;

    public static bool waterGateOpened = false;

    void Start()
    {
        waterGateOpened = false;
        WGFinish.SetActive(false); // Ensure WGFinish is inactive at the start
    }

    void Update()
    {
        Collider2D collider2 = WaterGateOb.GetComponent<Collider2D>();
        Bounds bounds = WG.GetComponent<Collider2D>().bounds;
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
            WaterGateClosed.SetActive(false);
            WaterGateOpening.SetActive(true);
            waterGateOpened = true;
        }
        else
        {
            WaterGateClosed.SetActive(true);
            WaterGateOpening.SetActive(false);
            waterGateOpened = false;
        }

        CheckFinishGame();
    }

    void CheckFinishGame()
    {
        if (waterGateOpened && FireGateMap3.fireGateOpened)
        {
            FinishGameW();
        }
    }

    void FinishGameW()
    {
        WGFinish.SetActive(true); // Activate WGFinish
        WG.SetActive(false);      // Hide WG

        Debug.Log("Both gates are open - FinishGameW is called.");

        // Start the Win Panel delay coroutine if both gates are open
        if (FireGateMap3.fireGateOpened)
        {
            FindObjectOfType<GM>().StartCoroutine("ShowWinPanelWithDelay");
        }
    }
}
