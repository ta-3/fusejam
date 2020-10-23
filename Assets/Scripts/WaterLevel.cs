using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour {
	bool busy = false;
	public List<AudioClip> fillSounds;
	public List<AudioClip> drainSounds;
	int fillSoundIndex = 0;
	int drainSoundIndex = 0;

	void Start () {
		Debug.Log (JsonUtility.ToJson (Quaternion.identity));
	}

	/*
	// Update is called once per frame
	void Update () {
		
		bool uKey = Input.GetKeyUp (KeyCode.Alpha1);
		bool dKey = Input.GetKeyUp (KeyCode.Alpha2);

		if (!busy && uKey) {
			RaiseTide ();
		} else if (!busy && dKey) {
			LowerTide ();
		}
	}

	*/
	IEnumerator WaterLevelShift (float distance, List<AudioClip> sounds, int soundIndex) {
		AudioSource a = GetComponent<AudioSource> ();
		a.clip = sounds [soundIndex];
		a.pitch = Random.Range (0.8f, 1.2f);
		a.Play ();

		busy = true;
		int ticks = 30;
		float distancePerTick = distance / (float) ticks;
		Vector3 movementPerTick = new Vector3(0.0f, distancePerTick, 0.0f);
		for (int i = 0; i < ticks; i++) {
			transform.Translate (movementPerTick);
			yield return null;
		}
		busy = false;
	}

	public bool RaiseTide () {
		if (!busy) {
			fillSoundIndex += 1;
			if (fillSoundIndex >= fillSounds.Count) {
				fillSoundIndex = 0;
			}
			StartCoroutine (WaterLevelShift (2.0f, fillSounds, fillSoundIndex));
		}
		return (!busy);
	}

	public bool LowerTide () {
		if (!busy) {
			drainSoundIndex += 1;
			if (drainSoundIndex >= drainSounds.Count) {
				drainSoundIndex = 0;
			}
			StartCoroutine (WaterLevelShift (-2.0f, drainSounds, drainSoundIndex));
		}
		return (!busy);
	}
}
