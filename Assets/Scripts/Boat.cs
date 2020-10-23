using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {
	public Transform engine;
	public float maxSpeed = 10.0f;
	public float acceleration = 10.0f;
	public Vector3 startPos;
	public WaterLevel waterLevel;
	public Transform waterLine;
	bool moving = false;
	Vector3 thrust = Vector3.forward;
	Rigidbody rb;

	[SerializeField]
	float buoyancyCoefficient = 1.0f;

	public AudioSource src;
	public AudioSource collisionAudio;
	public AudioClip stop;
	public AudioClip go;

	public Music music;
	public bool flagUp;
	public Transform flag;

	// Use this for initialization
	void Start () {
		
		startPos = transform.position;
		rb = GetComponent<Rigidbody> ();
		rb.maxAngularVelocity = 1.0f;
	}

	void OnCollisionEnter (Collision col) {
		collisionAudio.pitch = Random.Range (0.8f, 1.2f);
		collisionAudio.Play ();
	}


	void Update () {
		bool flagKey = Input.GetKeyUp (KeyCode.F);

		if (flagKey) {
			flagUp = !flagUp;
			flag.gameObject.SetActive (flagUp);
			music.SwitchTracks (flagUp);
		}
	}
	// Update is called once per frame
	void FixedUpdate () {

		float hAxis = Input.GetAxis ("Horizontal");
		float vAxis = Input.GetAxis ("Vertical");

		/*
		if (Input.GetKeyDown (KeyCode.R)) {
			transform.position = startPos;
		}
		*/

	

		if (vAxis > 0) {
			if (!moving) {
				moving = true;
				src.clip = go;
				src.loop = true;
				src.pitch = Random.Range (0.8f, 1.2f);
				src.Play ();
			}
			rb.AddForceAtPosition (acceleration * engine.forward, engine.position);
		
			//rb.drag = 1.0f;
		} else if (moving) {
			moving = false;
			src.clip = stop;
			src.loop = false;
			src.pitch = Random.Range (0.8f, 1.2f);
			src.Play ();
			//rb.drag = 1.0f;
		}

		transform.Rotate (0.0f, hAxis, 0.0f);


		rb.AddTorque (hAxis  * Vector3.up);

	

		rb.AddForce (buoyancyCoefficient * (Vector3.up * (waterLevel.transform.position.y - waterLine.position.y)));
	

		

		if (rb.velocity.magnitude > maxSpeed) {
			rb.velocity = maxSpeed * rb.velocity.normalized;
		}

		//Debug.Log ("engine.forward: " + engine.forward.ToString ());
	}


}
