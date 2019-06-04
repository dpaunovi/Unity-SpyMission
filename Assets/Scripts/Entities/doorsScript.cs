using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorsScript : MonoBehaviour {

    private bool open;

	// Use this for initialization
	void Start () {
        open = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (open == true) {
            if (transform.position.y < 2.1f) {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
            }
        }
	}

    public void openDoor() {
        if (!open) {
            open = true;
        }
    }
}
