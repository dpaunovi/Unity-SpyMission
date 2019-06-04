using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fanScript : MonoBehaviour {

    public ParticleSystem smoke;
    public spotScript cam;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerStay(Collider other)
	{
        if (Input.GetKeyDown(KeyCode.E)) {
            smoke.Play();
            if (cam)
            {
                cam.GetComponent<Light>().enabled = false;
                cam.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
	}
}
