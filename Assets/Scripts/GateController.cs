using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public Animator FireGateOpen;
    public Animator WaterGateOpen;
    public GameObject FB;
    public GameObject WG;
    public GameObject WGFinish;
    public GameObject FBFinish;
    public GameObject SoundWG;
    private Sound soundManager;

    void Start()
    {
        soundManager = SoundWG.GetComponent<Sound>();

    }

    // Update is called once per frame
    void Update()
    {
        if(FireGateOpen.isActiveAndEnabled && 
            FireGateOpen.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && 
            WaterGateOpen.isActiveAndEnabled && 
            WaterGateOpen.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            WG.transform.position = new Vector3(100, 100, 0);
            WGFinish.SetActive(true);

            FB.transform.position = new Vector3(100, 100, 0);
            FBFinish.SetActive(true);
        }
    }
}
