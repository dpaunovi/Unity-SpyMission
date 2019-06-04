using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    public float sensitivity = 2.0f;

    private float mouseX = 0.0f;
    private float mouseY = 0.0f;

    void Start() {
    }

    void Update() {
        checkMouse();
    }

    private void checkMouse() {
        mouseX += sensitivity * Input.GetAxis("Mouse X");
        mouseY -= sensitivity * Input.GetAxis("Mouse Y");
        if (mouseY > 60f)
        {
            mouseY = 60f;
        }
        else if (mouseY < -60f)
        {
            mouseY = -60f;
        }

        transform.eulerAngles = new Vector3(mouseY, mouseX, 0.0f);
    }
}
