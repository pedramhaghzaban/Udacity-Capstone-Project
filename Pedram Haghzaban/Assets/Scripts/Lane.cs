using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lane : MonoBehaviour {
	public GameObject PinsPrefab;
	public GameObject countText;
	private GameObject pins;
	private int points;

	// Use this for initialization
	void Start () {
		points = 0;
		SetCountText ();
		NewRound ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NewRound () {
		points = 0;
		SetCountText ();
		if (pins != null) {
			Destroy (pins);
		}
		pins = Instantiate (PinsPrefab);
		pins.transform.localPosition = new Vector3 (0.369581f, -1.03936f, 11.122f);

			
	}

	public int getPoints () {
		return points;
	}

	public void setPoints (int p) {
		points = p;
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Target")) {
			other.gameObject.SetActive(false);
			points += 1;
			SetCountText ();
		}
	}

	void SetCountText () {

		countText.GetComponent<TextMesh>().text = "Pins down: " + points.ToString ();

	}

	



}
