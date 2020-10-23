using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TideSwitch : MonoBehaviour {
	private AudioSource a;
	public WaterLevel wl;
	public Boat boat;
	public bool raise = true;
	Rigidbody boatRB;

	// Use this for initialization
	void Start () {
		boatRB = boat.GetComponent<Rigidbody> ();
		a = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col) {
		if (col.GetComponent<Boat>() != null) {
			a.Play ();
			if (raise) {
				wl.RaiseTide ();
			} else {
				wl.LowerTide ();
			}
		}
	}
}
