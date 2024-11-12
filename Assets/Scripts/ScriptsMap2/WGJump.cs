﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WGJump : MonoBehaviour
{
    private AudioManager audioManager;
    float rayLength = 0.6f;
    public LayerMask hitLayers;
    private Rigidbody2D rd;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        rd = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && CheckBelow())
        {
            rd.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
        }
    }

    bool CheckBelow()
    {
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.down;
        Debug.DrawRay(origin, direction * rayLength, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength, hitLayers);
        if (hit)
        {
            audioManager.PlaySFX(audioManager.jumpWG);
            return true;
        }
        return false;
    }
}