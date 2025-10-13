using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip choque;
    [SerializeField] private AudioClip ganar;
    [SerializeField] private AudioClip encender;

    private new AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        this.audio = GetComponent<AudioSource>();
    }
     
    // Update is called once per frame
    void Update()
    {
        
    }

    public void startCar()
    {
        this.audio.PlayOneShot(this.encender);
    }

    public void gameOver()
    {
        this.audio.PlayOneShot(this.choque);
        this.audio.volume = 0.3f;
    }

    public void winGame()
    {
        this.audio.PlayOneShot(this.ganar);
    }

}
