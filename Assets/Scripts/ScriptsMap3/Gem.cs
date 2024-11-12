using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    GM gm;
    float initialPosition;
    bool flag;

    private void Start()
    {
        gm = GameObject.FindObjectOfType<   GM>();
        flag = false;
        initialPosition = transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((gameObject.CompareTag("Red") && collision.CompareTag("FB")) ||
            (gameObject.CompareTag("Blue") && collision.CompareTag("WG")))
        {
            gm.gemsCollected++;
            // Phát âm thanh trước khi destroy gem
            gm.PlayGemCollectSound();
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (initialPosition - transform.position.y <= 0.2f && flag == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
        }
        else
        {
            flag = true;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        }
        if (transform.position.y - initialPosition >= 0.2f && flag == true)
        {
            flag = false;
        }
    }
}