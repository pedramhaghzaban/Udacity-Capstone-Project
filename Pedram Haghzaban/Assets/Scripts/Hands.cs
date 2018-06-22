using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour {

	public OVRInput.Controller controller;
	public GameObject ballPrefab;
	public GameObject boundary;
	public float scale;
	Rigidbody rb;

	private GameObject ball;

	// let go to throw button
	private float indexTriggerState;
	private float prevIndexTriggerState;
	private Lane lane;

	// respawn button 
	private bool prevAState;
	private bool AState;
	private Queue<Vector3> positions;
	private int size;

	private int spawned;

	// Use this for initialization
	void Start () {
		lane = boundary.GetComponent<Lane> ();
		indexTriggerState = 0.0f;
		prevIndexTriggerState = 0.0f;
		AState = false;
		prevAState = false;
		spawned = 0;
		size = 10;
		positions = new Queue<Vector3>();
		for (int i = 0; i < 10; i++) {
			positions.Enqueue (new Vector3(0.0f, 0.0f, 0.0f));
		}
	}

	// Update is called once per frame
	void Update () {

		// update the queue
		prevIndexTriggerState = indexTriggerState;
		indexTriggerState = OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, controller);

		if (ball != null) {
			positions.Dequeue ();
			positions.Enqueue (ball.transform.position);
		}

		prevAState = AState;
		AState = OVRInput.Get (OVRInput.Button.One);

		// respawn
		if (AState == true && prevAState == false) {
			Respawn ();
		}

		// let go of ball and throw
	
		if (indexTriggerState < 0.9 && prevIndexTriggerState >= 0.9) {
			ThrowBall ();
		}


		if (lane.getPoints () == 10) {
			if (spawned == 1) {
				lane.countText.GetComponent<TextMesh> ().text = "Pins down: " + lane.getPoints().ToString () + "\nSTRIKE! wow";
				//lane.setPoints (0);
				spawned = 3;
			} else if (spawned == 2) {
				lane.countText.GetComponent<TextMesh> ().text = "Pins down: " + lane.getPoints().ToString () + "\ns p a r e ! w o w";
			}
		}
	}

	private int addOne(int index) {
		return (index + 1) % size;
	}

	void Respawn () {
		if (ball != null) {
			Destroy (ball);
		}
		spawned = spawned + 1;

		if (spawned > 2) {
			spawned = 1;
			lane.NewRound ();
		}

		ball = Instantiate (ballPrefab);
		rb = ball.GetComponent<Rigidbody> ();
		rb.useGravity = false;
		ball.transform.position = this.transform.position - new Vector3(0.10f, 0.1f, 0);
		ball.transform.SetParent(this.transform);
		ball.transform.localScale = Vector3.one * scale;
	}

	void ThrowBall () {
		// rb.isKinematic = false;

		Vector3 velocity = Vector3.Scale((ball.transform.position - positions.Peek ()), new Vector3(20, 5, 20));
		Debug.Log (velocity);
		Debug.Log (rb.position);
		rb.velocity = velocity;
		rb.useGravity = true;
		ball.transform.parent = null;
	}
}
