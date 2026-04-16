using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float mouseSensitivity = 120f;
    public Transform cameraPivot;
    public float gravity = -9.81f;

    private CharacterController cc;
    private float verticalVel = 0f;
    private float camPitch = 0f;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        if (cameraPivot == null)
        {
            // Create a camera if not assigned
            var camGO = new GameObject("PlayerCamera");
            Camera cam = camGO.AddComponent<Camera>();
            camGO.transform.SetParent(transform);
            camGO.transform.localPosition = new Vector3(0, 1.6f, 0);
            cameraPivot = camGO.transform;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse look
        float mx = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float my = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mx);
        camPitch = Mathf.Clamp(camPitch - my, -80f, 80f);
        cameraPivot.localEulerAngles = new Vector3(camPitch, 0, 0);

        // Move
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * h + transform.forward * v) * moveSpeed;

        // Gravity
        if (cc.isGrounded && verticalVel < 0) verticalVel = -2f;
        verticalVel += gravity * Time.deltaTime;
        move.y = verticalVel;

        cc.Move(move * Time.deltaTime);

        // Unlock cursor with Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
