using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    public float stoppingDrag = 10.0f;
    private Rigidbody rb;
    private Vector2 rotation = Vector2.zero;
    public float sensitivity = 3.0f;
    private bool isGrounded = false;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = transform.TransformDirection(movement);

        if (isGrounded) {
            rb.AddForce(movement * speed);

            if (moveHorizontal == 0.0f && moveVertical == 0.0f) {
                rb.drag = stoppingDrag;
                rb.angularDrag = stoppingDrag;
            } else {
                rb.drag = 0.0f;
                rb.angularDrag = 0.0f;
            }
        } else {
            rb.AddForce(movement * speed * 0.5f);
            rb.drag = 0.0f;
            rb.angularDrag = 0.0f;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Get the mouse input for rotation
        rotation.y += Input.GetAxis("Mouse X") * sensitivity;
        rotation.x += Input.GetAxis("Mouse Y") * sensitivity;
        rotation.x = Mathf.Clamp(rotation.x, -90, 90);

        // Apply rotation to the player
        transform.eulerAngles = new Vector2(0, rotation.y);
        Camera.main.transform.eulerAngles = new Vector2(-rotation.x, rotation.y);
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }
}
