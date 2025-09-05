using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidBody;
    [SerializeField] private float _speed = 5f;

    // Tambahan: drag Main Camera ke sini lewat Inspector
    [SerializeField] private Transform _cameraTransform;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        HideAndLockCursor();
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // arah gerakan relatif kamera
        Vector3 horizontalDirection = horizontal * _cameraTransform.right;
        Vector3 verticalDirection = vertical * _cameraTransform.forward;

        // buang komponen Y biar tidak miring
        horizontalDirection.y = 0;
        verticalDirection.y = 0;

        // gabungkan
        Vector3 movementDirection = (horizontalDirection + verticalDirection).normalized;

        // apply ke rigidbody
        _rigidBody.linearVelocity = movementDirection * _speed;
    }
}
