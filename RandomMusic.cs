using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusic : MonoBehaviour
{

    public AudioClip[] Music = new AudioClip[5];
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = Music[Random.Range(0, Music.Length)];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
