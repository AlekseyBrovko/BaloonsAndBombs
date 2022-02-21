using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [SerializeField] private float floatForce;
    
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem fireworksParticle;

    [SerializeField] private AudioClip moneySound;
    [SerializeField] private AudioClip explodeSound;
    [SerializeField] private AudioClip getUpSound;
    [SerializeField] private float coolDown = 1.0f;

    public bool _gameOver;

    private float _maxHeight = 15;
    private float _minHeight = 0.5f;
    private float _gravityModifier = 1.5f;
    private float _nextUpTime = 0;

    private Rigidbody _playerRb;
    private AudioSource _playerAudio;

    private void Start()
    {
        Physics.gravity *= _gravityModifier;
        _playerAudio = GetComponent<AudioSource>();
        _playerRb = GetComponent<Rigidbody>();
        
        _playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }
    
    private void Update()
    {
        if (Time.time > _nextUpTime) 
        {                
            if (Input.GetKeyDown(KeyCode.Space) && !_gameOver)
            {
                _playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
                _nextUpTime = Time.time + coolDown;
            }
        }

        if (transform.position.y > _maxHeight)
        {
            transform.position = new Vector3(transform.position.x, _maxHeight, transform.position.z);
        }

        if (transform.position.y < _minHeight && !_gameOver)
        {
            transform.position = new Vector3(transform.position.x, _minHeight, transform.position.z);
            _playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            _playerAudio.PlayOneShot(getUpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {        
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            _playerAudio.PlayOneShot(explodeSound, 1.0f);
            _gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }                 
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            _playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }
    }
}
