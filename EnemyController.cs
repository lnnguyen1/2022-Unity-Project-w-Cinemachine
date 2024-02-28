using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(AudioSource))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _isVerticalEnemy;
    [SerializeField] private float _changeTime = 3.0f;
    [SerializeField] private ParticleSystem _smokeEffect;
    [SerializeField] private AudioClip _hitClip; 


    private float _timer;
    private int _direction = 1;
    private bool _isBroken = true;
    
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private AudioSource _audioSource;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
        _timer = _changeTime;
        _audioSource.Play();
    }

    void Update()
    {
        if(!_isBroken)
        {
            return;
        }
        
        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            _direction = -_direction;
            _timer = _changeTime;
        }
    }
    
    void FixedUpdate()
    {
        if(!_isBroken)
        {
            return;
        }
        
        Vector2 position = _rigidbody.velocity;
        
        if (_isVerticalEnemy)
        {
            position.y = _moveSpeed * _direction;
            _animator.SetFloat("MoveX", 0);
            _animator.SetFloat("MoveY", _direction);
        }
        else
        {
            position.x = _moveSpeed * _direction;
            _animator.SetFloat("MoveX", _direction);
            _animator.SetFloat("MoveY", 0);
        }
        
        _rigidbody.velocity = position;
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.TryGetComponent(out RubyController controller);
        controller.TakeDamage(1);
    }
    
    public void Fix() //what does fix mean?
    {
        _isBroken = false;
        _rigidbody.simulated = false;
        _animator.SetTrigger("Fixed");
        
        _smokeEffect.Stop();
        _audioSource.PlayOneShot(_hitClip);
        _audioSource.Stop();
    }
}