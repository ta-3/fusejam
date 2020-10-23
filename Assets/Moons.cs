using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moons : MonoBehaviour {
	//public Vector3 rotation;
	public Vector3 rotationAxis = Vector3.left;
	public float rotationSpeed = 1.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.RotateAround (Vector3.zero, Vector3.left, rotationSpeed);
	}
}
