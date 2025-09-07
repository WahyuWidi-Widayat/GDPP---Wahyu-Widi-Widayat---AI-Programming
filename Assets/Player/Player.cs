using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class Player : MonoBehaviour
{

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;
    private Rigidbody _rigidBody;
    private Coroutine _powerupCoroutine;
    private bool _isPoweredUp;
    [SerializeField] private float _speed = 5f;

    // Tambahan: drag Main Camera ke sini lewat Inspector
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _powerupDuration;
    [SerializeField] private int _health;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Transform _respawnPoint;




    public void PickPowerUp()
    {
        if (_powerupCoroutine != null)
        {
            StopCoroutine(_powerupCoroutine);
        }
        _powerupCoroutine = StartCoroutine(StartPowerUp());
    }
    private IEnumerator StartPowerUp()
    {
        _isPoweredUp = true;
        if (OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }
        yield return new WaitForSeconds(_powerupDuration);
        _isPoweredUp = false;
        if (OnPowerUpStop != null)
        {
            OnPowerUpStop();
        }
    }
    private void Awake()
    {
        UpdateUI();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPoweredUp)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }

    }
    private void UpdateUI()
    {

        _healthText.text = "Health: " + _health;

    }
    
   public void Dead()
{
    _health -= 1;

    if (_health > 0)
    {
        transform.position = _respawnPoint.position;
    }
    else
    {
        _health = 0;
        Debug.Log("Lose");
    }

    UpdateUI();
}

}
