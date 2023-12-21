using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	public AudioSource clickSound;
	public AudioSource buildSound;
	public AudioSource sellSound;
	public AudioSource projectileSound;
	public AudioSource damageSound;
	public AudioSource bearSpawnSound;
	public AudioSource waveSpawnSound;
	
    // Start is called before the first frame update
    void Start()
    {
	    instance = this;
	    AudioSource[] allAudio = GetComponents<AudioSource>();
	    try
	    {
		    clickSound = allAudio[0];
		    buildSound = allAudio[1];
		    sellSound = allAudio[2];
		    projectileSound = allAudio[3];
		    damageSound = allAudio[4];
		    bearSpawnSound = allAudio[5];
		    waveSpawnSound = allAudio[6];
	    }
	    catch (Exception ex)
	    {
		    //Ignore unassigned sound files (other scenes do not need all audio sources)
	    }
    }

    public void PlayClickSound()
    {
	    clickSound.Play();
	}
    
    public void PlayBuildSound()
    { 
	    buildSound.Play();
    }
    
    public void PlaySellSound()
    { 
	    sellSound.Play();
    }
    
    public void PlayProjectileSound()
    { 
	    projectileSound.Play();
    }
    public void PlayDamageSound()
    { 
	    damageSound.Play();
    }
    
    public void PlayBearSpawnSound()
    { 
	    bearSpawnSound.Play();
    }

    public void PlayWaveSpawnSound()
    {
	    waveSpawnSound.Play();
    }
}
