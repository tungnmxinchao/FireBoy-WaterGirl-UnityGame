using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] BTN b;
    Vector3 newPosition;
    Vector3 oldPosition;

    void Start()
    {
        newPosition = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
        oldPosition = transform.position;
    }

    void Update()
    {
        // Move the elevator based on the MoveELe state in BTN
        if (b.MoveELe)
        {
            if (transform.position != newPosition)
            {
                transform.position = Vector3.Lerp(transform.position, newPosition, 0.01f);
            }
        }
        else
        {
            if (transform.position != oldPosition)
            {
                transform.position = Vector3.Lerp(transform.position, oldPosition, 0.01f);
            }
        }
    }
}
