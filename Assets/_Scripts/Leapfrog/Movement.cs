using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //get player references
    private enum Players { Player1, Player2 }
    [Header("Players")]
    [SerializeField] private Players player;
    [SerializeField] private GameObject playerObject;

    [Header("Movement settings")]
    [SerializeField] private float speed, jumpHeight;

    private bool grounded;
    private bool hasScoredInAir;

    private Rigidbody rb;

    private void Start() {
        rb = playerObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        HandleInputs();
        CheckHit();
    }

    private void HandleInputs() {
        float moveX = 0f;
        float moveZ = 0f;

        if(player == Players.Player1) {
            if (Input.GetKey(KeyCode.W)) moveZ = speed;
            if (Input.GetKey(KeyCode.S)) moveZ = -speed;
            if (Input.GetKey(KeyCode.A)) moveX = -speed;
            if (Input.GetKey(KeyCode.D)) moveX = speed;

            Vector3 move = new Vector3(moveX, 0, moveZ);
            rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

            if (Input.GetKey(KeyCode.LeftShift) && grounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            }
        }

        else if(player == Players.Player2) {
            if (Input.GetKey(KeyCode.UpArrow)) moveZ = speed;
            if (Input.GetKey(KeyCode.DownArrow)) moveZ = -speed;
            if (Input.GetKey(KeyCode.LeftArrow)) moveX = -speed;
            if (Input.GetKey(KeyCode.RightArrow)) moveX = speed;

            Vector3 move = new Vector3(moveX, 0, moveZ);
            rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

            if (Input.GetKey(KeyCode.RightShift) && grounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            grounded = true;
        }
    }
    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            grounded = false;
        }
    }

    private void CheckHit() {
    RaycastHit hit;
    if (Physics.Raycast(transform.position, Vector3.down, out hit, 5f)) {
        if (hit.collider.gameObject.CompareTag("Player") && !hasScoredInAir) {
            IncreaseScore();
            hasScoredInAir = true;
        }
    }

    if (grounded) {
        hasScoredInAir = false;
    }
}

    private void IncreaseScore() {
        if(player == Players.Player1) {
            GameManager.Instance.IncreasePlayer1Score();
        }
        else if(player == Players.Player2) {
            GameManager.Instance.IncreasePlayer2Score();
        }
    }
}
