using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RA_MusicScript : MonoBehaviour {

	public AudioClip[] musics;
	// Use this for initialization
	int currentMusicIndex = 6;
	AudioSource audioSource;
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!audioSource.isPlaying)
        {
            // auto next track
			audioSource.clip = musics[(currentMusicIndex++ % 7)];
            audioSource.Play();
        }
	}

	public void onMasticoClick() {
		audioSource.clip = musics[(currentMusicIndex++ % 7)];
		audioSource.Play();

	}
}
