using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Sound : MonoBehaviour
{
    public AudioSource jumpSoundFB;
    public AudioSource jumpSoundWG;
    public AudioSource musicSound;
    public AudioSource swiperSound;
    public AudioSource buttonSound;
    public AudioSource blockSound;
    public AudioSource dieSound;
    public AudioSource diamondSound;
    int score  = 0;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        musicSound.Play();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumpSoundFB.Play();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                jumpSoundWG.Play();
            }
        }
        
    }

    public void SwiperSound()
    {
        swiperSound.Play();
    }

    public void ButtonSound()
    {
        buttonSound.Play();
    }

    public void BlockSound()
    {
        if (!blockSound.isPlaying)
        {
            blockSound.Play();
        }
    }

    public void StopBlockSound()
    {
        if (blockSound.isPlaying)
        {
            blockSound.Stop();
        }
    }

    public void DieSound()
    {
        dieSound.Play();
    }

    public void DiamondSound()
    {
        diamondSound.Play();
    }

    public int calculatePoint(Boolean check)
    {
        if(check)
        {
            score++;

        }
        return score;
    }


    public void GetRank(int score)
    {
        int maxScore = 6; 
        float percentage = (float)score / maxScore * 100;
        Debug.Log(percentage);
        if (percentage > 70)
        {
            scoreText.text = "Rank: A";
            scoreText.color = Color.yellow;
        }
        else if (percentage >= 35)
        {
            scoreText.text = "Rank: B";
            scoreText.color = Color.green;
        }
        else
        {
            scoreText.text = "Rank: C";
            scoreText.color = Color.black;
        }
    }

}
