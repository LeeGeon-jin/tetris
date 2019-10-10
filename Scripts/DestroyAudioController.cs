using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAudioController : MonoBehaviour
{
    AudioSource destroyAudio;
    // Start is called before the first frame update
    void Start()
    {
        destroyAudio = GetComponent<AudioSource>();
            
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio()
    {

        destroyAudio.Play();
    }
}
