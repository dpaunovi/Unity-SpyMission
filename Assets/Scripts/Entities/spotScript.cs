using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spotScript : MonoBehaviour {
	public float speed = 100;
	public Slider hud;

	public bool detect;
	// public float hud;

	// Use this for initialization
	void Start () {
		hud = FindObjectOfType<Slider>();
		detect = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Debug.Log(hud.value);
		if (!detect && hud.value >= 0 && hud.value < 1) {
			hud.value -= ((Time.deltaTime * speed / 50) / 100);
		} else if (detect && hud.value < 1) {
			hud.value += ((Time.deltaTime * speed) / 100);
		}
	}

	void OnTriggerEnter(Collider other) {
		detect = true;
    }

	void OnTriggerExit(Collider other) {
		detect = false;
    }
}
