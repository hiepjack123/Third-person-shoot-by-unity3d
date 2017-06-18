using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance = null;
    private AudioSource soundEffectAudio;
  
	// Use this for initialization
    void Start()
    {
        if (Instance == null)
        { Instance = this; }
        else if (Instance != this)
        { 
            Destroy(gameObject);
        }//automatically destroys itself to make sure that one and only one SoundManager exists.
        AudioSource[] audioSources = GetComponents<AudioSource>();//Get all component in AudioSource
        foreach (AudioSource source in audioSources)
        {
            if (source.clip == null)//this checks for the audio source that has no music before setting soundEffectAudio.

                soundEffectAudio = source;//Add clip to source in audioSouces like shoot, death ,...
        }
	
	}
    public AudioClip gunFire;
    public AudioClip upgradedGunFire;
    public AudioClip hurt;
    public AudioClip alienDeath;
    public AudioClip marineDeath;
    public AudioClip victory;
    public AudioClip elevatorArrived;
    public AudioClip powerUpPickup;
    public AudioClip powerUpAppear;
	// Update is called once per frame
	void Update () {
       


		
	}
   public void PlayOneShot(AudioClip clip){
        soundEffectAudio.PlayOneShot(clip);}//create method which play one shot clip we choose like shoot,alien death,..
}
