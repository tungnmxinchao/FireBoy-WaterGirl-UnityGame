using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----Audio Source----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("----Audio Clip----")]
    public AudioClip menu;
    public AudioClip background;
    public AudioClip eatGem;
    public AudioClip jumpFB;
    public AudioClip jumpWG;
    public AudioClip clickButton;
    public AudioClip hitLever;
    public AudioClip gateMove;
    public AudioClip goInGate;
    public AudioClip death;
    public AudioClip click;
    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        musicSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySFX(AudioClip sFX)
    {
        SFXSource.PlayOneShot(sFX);
    }
    public void PlayMenu()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();  // You could use .Pause() if you want to resume later
        }
        musicSource.clip = menu;
        musicSource.Play();
        musicSource.loop = true;
    }
    public void Continue()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();  
        }
        musicSource.clip = background;
        musicSource.Play();
        musicSource.loop = true;
    }
    public void PlayGoInGate()
    {
        StartCoroutine(PlayGoInGateCoroutine());
    }
    private IEnumerator PlayGoInGateCoroutine()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }

        SFXSource.PlayOneShot(goInGate);

        yield return new WaitForSeconds(goInGate.length);

        if (!musicSource.isPlaying)
        {
            musicSource.Play();  
            musicSource.loop=true;
        }
    }
}
