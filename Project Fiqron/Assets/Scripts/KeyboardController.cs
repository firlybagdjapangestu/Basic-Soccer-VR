using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KeyboardController : MonoBehaviour
{
    public float walkSpeed = 5f; // Kecepatan jalan biasa
    public float sprintSpeed = 10f; // Kecepatan saat sprint (Shift)
    public float jumpHeight = 2f; // Ketinggian lompat
    public float mouseSensitivity = 2f; // Sensitivitas mouse

    private CharacterController controller;
    private float verticalRotation = 0f;
    public float gravity = -9.81f;
    private Vector3 velocity;

    public Camera playerCamera;

    void Start()
    {

        // Ambil referensi ke komponen CharacterController
        controller = GetComponent<CharacterController>();

        // Sembunyikan kursor dan kunci di tengah layar
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();
        LookAround();
        Jump();
    }

    void MovePlayer()
    {
        // Input untuk pergerakan horizontal dan vertikal
        float moveX = Input.GetAxis("Horizontal"); // A dan D
        float moveZ = Input.GetAxis("Vertical");   // W dan S

        // Tentukan kecepatan berdasarkan apakah menunduk, sprint, atau berjalan biasa
        float currentSpeed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed; // Sprint
        }

        // Tentukan arah gerak berdasarkan orientasi karakter
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Terapkan gerak ke karakter dengan kecepatan sesuai mode jalan/sprint/menunduk
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Terapkan gravitasi
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Agar tetap grounded
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void LookAround()
    {
        // Input untuk rotasi mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotasi di sumbu X (left dan right)
        transform.Rotate(Vector3.up * mouseX);

        // Rotasi di sumbu Y (up dan down), dengan batasan agar tidak berputar penuh
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        // Terapkan rotasi vertikal
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void Jump()
    {
        // Jika karakter berada di tanah dan tombol space ditekan, lompat
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

}
