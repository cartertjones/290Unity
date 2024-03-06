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

    [Header("Audio settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip ribbit;
    [SerializeField] private AudioClip point;
    [SerializeField] private AudioClip[] squish;
    [SerializeField] private AudioClip slam;

    [Header("Effects")]
    [SerializeField] private ParticleSystem squishEffect;

    private bool grounded;
    private bool slamming;
    private bool beingSquished;
    private bool squishing;
    private float squishDuration = 0.1f;
    private bool hasScoredInAir;
    private bool hasScoredOnPlayer;

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
                audioSource.PlayOneShot(ribbit);
            }
            if(Input.GetKey(KeyCode.LeftControl) && !grounded && !slamming && !beingSquished) {
                rb.velocity = new Vector3(rb.velocity.x, -jumpHeight * 2, rb.velocity.z);
                slamming = true;
                audioSource.PlayOneShot(slam);
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
                audioSource.PlayOneShot(ribbit);
            }
            if(Input.GetKey(KeyCode.RightControl) && !grounded && !slamming && !beingSquished) {
                rb.velocity = new Vector3(rb.velocity.x, -jumpHeight * 2, rb.velocity.z);
                slamming = true;
                audioSource.PlayOneShot(slam);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            grounded = true;
            slamming = false;
        }

        if (other.gameObject.CompareTag("Player")) {
            // rb.AddForce(new Vector3(other.contacts[0].normal.x, 0, other.contacts[0].normal.z) * 100, ForceMode.Impulse);
        }
    }
    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            grounded = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Collectable")) {
            IncreaseScore();
            audioSource.PlayOneShot(point);
            Destroy(other.gameObject);
        }
    }

    private void CheckHit() {
    RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.5f))
        {
            if (hit.collider.gameObject.CompareTag("Player") && slamming && !squishing)
            {
                if(!hasScoredOnPlayer) {
                    IncreaseScore();
                    hasScoredOnPlayer = true;
                    audioSource.PlayOneShot(squish[Random.Range(0, squish.Length)]);
                    squishEffect.Play();
                    StartCoroutine(Squish(hit));
                }
            }
        }

    // if (Physics.Raycast(transform.position, Vector3.down, out hit, 5f)) {
    //     if (hit.collider.gameObject.CompareTag("Player") && !hasScoredInAir) {
    //         IncreaseScore();
    //         hasScoredInAir = true;
    //         audioSource.PlayOneShot(point);
    //     }
    // }
}

private IEnumerator Squish(RaycastHit hit)
    {
        squishing = true; // Set squishing flag to true to prevent multiple coroutine instances

        Movement hitMovement = hit.collider.gameObject.GetComponent<Movement>();

        if(hitMovement) hitMovement.beingSquished = true;

        float elapsedTime = 0f;
        Vector3 startScale = hit.collider.gameObject.transform.localScale;
        Vector3 targetScale = new Vector3(1, 0.2f, 1);

        while (elapsedTime < squishDuration)
        {
            float scaleFactor = Mathf.Clamp01(elapsedTime / squishDuration);
            hit.collider.gameObject.transform.localScale = Vector3.Lerp(startScale, targetScale, scaleFactor);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the scale reaches the exact target scale
        hit.collider.gameObject.transform.localScale = targetScale;

        squishing = false; // Reset squishing flag

        yield return new WaitForSeconds(2f);

        hit.collider.gameObject.transform.localScale = new Vector3(1, 1, 1);
        hasScoredOnPlayer = false;
        slamming = false;
        if(hitMovement) hitMovement.beingSquished = false;
    }

    private void IncreaseScore() {
        if(player == Players.Player1) {
            Managers.Inventory.AddItem("points", 0);
        }
        else if(player == Players.Player2) {
            Managers.Inventory.AddItem("points", 1);
        }
    }
}
