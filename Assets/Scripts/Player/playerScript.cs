using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class playerScript: MonoBehaviour {

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float sprint = 1.7f;
    public float gravity = 20.0f;
    public Material unlock_door;
    public cameraScript cam;
    public AudioSource cardSound;
    public AudioSource doorRefused;
    public AudioSource doorOpen;
    public AudioSource run;
    public AudioSource restart;
    public Text actionText;
    public bool action;
    public Camera endcam;



    private float time = 0.0f;
    private bool end;
    private Slider hud;
    private float mouseX = 0.0f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private bool access = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
		hud = FindObjectOfType<Slider>();
        action = false;
        end = false;
        // let the gameObject fall down
        //gameObject.transform.position = new Vector3(0, 5, 0);
    }

    void Update()
    {
        if (!end)
        {
            checkMouseX();
            checkMove();
            applyGravity();
            moveController();
            if (action)
            {
                printAction();
            }
            else
            {
                delAction();
            }
        } else {
            endGame();
            printAction();
        }
    }

    private void checkMove() {
        if (controller.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
            moveDirection.y = 0;
            if (moveDirection != Vector3.zero)
            {
                if (!run.isPlaying) {
                    run.Play();
                }
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (hud.value < 1) {
        			hud.value += ((Time.deltaTime * 10) / 100);
                }
                moveDirection *= sprint;
            }
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
    }

    private void applyGravity() {
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
    }

    private void moveController() {
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void checkMouseX() {
        mouseX += cam.sensitivity * Input.GetAxis("Mouse X");

        transform.eulerAngles = new Vector3(0.0f, mouseX, 0.0f);
    }

	private void OnTriggerStay(Collider other)
	{
        if (other.name == "prop_keycard") {
            action = true;
            if (Input.GetKeyDown(KeyCode.E)) {
                other.gameObject.SetActive(false);
                access = true;
                action = false;
                cardSound.Play();
            }
        } else if (other.name == "prop_switchUnit") {
            action = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (access)
                {
                    other.gameObject.GetComponent<Transform>().Find("prop_switchUnit_screen").GetComponent<Renderer>().material = unlock_door;
                    other.gameObject.GetComponent<Transform>().parent.Find("door_generic_slide").GetComponent<doorsScript>().openDoor();
                    action = false;
                    doorOpen.Play();
                } else {
                    doorRefused.Play();
                }
            }
        } else if (other.name == "table") {
            action = true;
            if (Input.GetKeyDown(KeyCode.E)) {
                endInit("You Win" + '\n' + "Simulation, Restarting...");
            }
        } else if (other.name == "fan") {
            action = true;
        } else {
            action = false;
        }
	}

	private void OnTriggerExit(Collider other)
	{
        action = false;
	}

	private void printAction()
    {
        if (time < 1)
        {
            time += Time.deltaTime;
            actionText.color = new Vector4(1, 1, 1, time / 1);
        }
    }

    private void delAction()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            actionText.color = new Vector4(1, 1, 1, time / 1);
        }
    }

    public void endInit(string text) {
        action = false;
        time = 0.0f;
        cam.GetComponent<Camera>().enabled = false;
        endcam.enabled = true;
        actionText.text = text;
        end = true;
    }

    private void endGame() {
        action = false;
        time += Time.deltaTime;
        if (time < 5)
        {
            if (time < 1)
            {
                if (!restart.isPlaying)
                {
                    restart.Play();
                }
            }
        } else {
            SceneManager.LoadScene("00");
        }
    }
}