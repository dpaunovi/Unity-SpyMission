using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {

	public AudioSource normal;
	public AudioSource alarm;
    public AudioSource panic;
    public playerScript player;

	private Slider hud;
    private Image hudFill;
    private bool end = true;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		hud = FindObjectOfType<Slider>();
		normal.Play();
        hudFill = hud.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (hud.value < 0.5)
        {
            if (!normal.isPlaying)
            {
                normal.Play();
                hudFill.color = new Vector4(1f, 1f, 1f, 0.4f);
            }
            if (alarm.isPlaying || panic.isPlaying)
            {
                alarm.Stop();
                panic.Stop();
            }
        }
        else if (hud.value >= 0.5 && hud.value < 0.75) {
            if (normal.isPlaying)
            {
                normal.Stop();
                hudFill.color = new Vector4(1f,0.3f,0.3f,0.5f);
            }
            if (alarm.isPlaying)
            {
                alarm.Stop();
            }
            if (!panic.isPlaying)
            {
                panic.Play();
            }
        }
        else if (hud.value >= 0.75 && hud.value < 1)
        {
            if (normal.isPlaying)
            {
                normal.Stop();
            }
            if (!alarm.isPlaying)
            {
                alarm.Play();
                hudFill.color = new Vector4(1f, 0f, 0f, 0.5f);
            }
            if (!panic.isPlaying)
            {
                panic.Play();
            }
        } else {
            if (end)
                player.endInit("You Loose" + '\n' + "Simulation, Restarting...");
            end = false;
        }

	}
}
