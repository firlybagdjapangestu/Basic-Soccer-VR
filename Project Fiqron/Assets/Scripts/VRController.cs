using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRController : MonoBehaviour
{
    [SerializeField] private float minAngle = 20f; // Sudut minimum untuk mendeteksi menunduk
    [SerializeField] private float maxAngle = 60f; // Sudut maksimum untuk mendeteksi menunduk
    [SerializeField] private float moveSpeed = 3f; // Kecepatan gerak karakter
    [SerializeField] private Transform cameraTransform; // Transform kamera
    [SerializeField] private CharacterController characterController; // Komponen CharacterController

    private bool isMoving; // Status pergerakan

    // Update is called once per frame
    void Update()
    {
        LookDownToMove();
        MoveCharacter();
    }

    private void LookDownToMove()
    {
        // Mendapatkan sudut rotasi kamera pada sumbu X
        float cameraAngle = cameraTransform.eulerAngles.x;

        // Menentukan apakah kamera berada dalam rentang sudut yang ditentukan
        isMoving = cameraAngle > minAngle && cameraAngle < maxAngle;
    }

    private void MoveCharacter()
    {
        if (!isMoving) return; // Jika tidak bergerak, keluar dari fungsi

        Vector3 direction = cameraTransform.TransformDirection(Vector3.forward);
        characterController.Move(direction * moveSpeed * Time.deltaTime);
    }
}
