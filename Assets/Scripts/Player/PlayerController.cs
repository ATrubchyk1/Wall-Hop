using System;
using Game;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action PlayerDied;
    
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _maxJumpCount = 2;
    [SerializeField] private AudioSource _moveAudio;
    [SerializeField] private AudioSource _landingAudio;
    [SerializeField] private GameObject _playerDiedFxPrefab;

    private int _jumpCount;
    private Rigidbody2D _rigidbody2D;
    private EchoEffect _echoEffect;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _echoEffect = GetComponent<EchoEffect>();
        _jumpCount = 0;
    }

    private void Update()
    {
        if (CanJump() && CheckJumpInput())
        {
            Jump();
            _echoEffect.CanShowEcho(true);
        }
    }

    private void Jump()
    {
        _moveAudio.Play();
        _rigidbody2D.velocity = Vector2.up * _jumpForce * _rigidbody2D.gravityScale;
        _jumpCount--;
    }

    private bool CheckJumpInput()
    {
        bool isSpaceButton = Input.GetKeyDown(KeyCode.Space);
        bool isTuchInput = Input.touches.Length > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        return isSpaceButton || isTuchInput;
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.collider.CompareTag(GlobalConstants.FLOOR_TAG) && !_isGrounded)
        {
            _landingAudio.Play();
            _jumpCount = _maxJumpCount;
            _isGrounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.collider.CompareTag(GlobalConstants.FLOOR_TAG))
        {
            _isGrounded = false;
        }
    }

    private bool CanJump()
    {
        return _jumpCount > 0;
    }
    
    public void DestroyPlayer()
    {
        Instantiate(_playerDiedFxPrefab, transform.position, Quaternion.identity);
        PlayerDied?.Invoke();
        Destroy(gameObject);
    }
}
