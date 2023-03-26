using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private CharacterController characterController;
    private Camera cam;

    private float speed;
    private float walkingSpeed = 4.317f;
    private float sprintingSpeed = 5.612f;
    private float fallSpeed = -78.4f;
    private float jumpHeight = 1.2522f;

    private bool isGrounded;
    private bool isSprinting;

    private Vector3 velocity;

    private float lastClickTime;
    private const float DOUBLE_CLICK_TIME = 0.2f;

    private float xRotation;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
    }

    private void Start() {
        speed = walkingSpeed;
    }

    private void Update() {
        FallUpdate();
        JumpUpdate();
        MovementUpdate();

        Cursor.lockState = CursorLockMode.Locked;
        CameraUpdate();
    }

    private void FallUpdate() {
        velocity.y += fallSpeed * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        
        isGrounded = characterController.isGrounded;

        if(isGrounded && velocity.y < 0) {
            velocity.y = -2.0f;
        }
    }

    private void JumpUpdate() {
        if(isGrounded && Input.GetButton("Space")) {
            isGrounded = false;

            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * fallSpeed);
        }
    }

    private void MovementUpdate() {
        float x = Input.GetAxis("HorizontalAD");
        float z = Input.GetAxis("VerticalWS");

        Vector3 moveDirection = transform.TransformDirection(new Vector3(x, 0.0f, z));
        moveDirection *= speed;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void SprintUpdate() {
        if(Input.GetButton("LCtrl")) {
            isSprinting = true;
        }

        if(Input.GetKeyDown(KeyCode.W)) {
            float timeSinceLastClick = Time.time - lastClickTime;

            if(timeSinceLastClick <= DOUBLE_CLICK_TIME) {
                isSprinting = !isSprinting;
            }

            lastClickTime = Time.time;
        }
        if(Input.GetKeyUp(KeyCode.W)) {
            isSprinting = false;
        }

        if(isGrounded) {
            speed = isSprinting ? sprintingSpeed : walkingSpeed;
        }
    }

    private void CameraUpdate() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
