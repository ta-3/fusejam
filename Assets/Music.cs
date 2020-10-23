using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {
	public List<AudioClip> peacefulTracks;
	public List<AudioClip> pirateTracks;
	public AudioClip pirateIntro;
	public AudioSource music;
	public AudioSource arr;
	//public AudioSource ambiance;
	bool flagUp;
	int index = 0;
	
	void Update() {
		if (!music.isPlaying) {
			PlayMusic ();
		}
	}

	void PlayMusic () {
		if (flagUp) {
			int index = Random.Range (0, pirateTracks.Count - 1);
			music.clip = pirateTracks [index];
			music.Play ();
		} else {
			int index = Random.Range (0, peacefulTracks.Count - 1);
			music.clip = peacefulTracks [index];
			music.Play ();
		}
	}

	public void SwitchTracks (bool flagUp) {
		index = 0;
		this.flagUp = flagUp;
		if (flagUp) {
			arr.loop = false;
			arr.clip = pirateIntro;
			arr.Play ();
			PlayMusic ();
		} else {
			PlayMusic ();
		}
	}
		
}
