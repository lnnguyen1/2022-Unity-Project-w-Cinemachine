using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(AudioSource))]
public class RubyController : MonoBehaviour
{
    public int GetCurrentHealth => currentHealth;
    public int GetMaxHealth => _maxHealth;
    [Header("Player Stats")]
    [SerializeField] private float _moveSpeed = 3.0f;
    [SerializeField] private float _timeInvincible = 2.0f;
    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private float _throwForce;
    
    [Header("Other")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private ParticleSystem _healthEffect;
    [SerializeField] private AudioClip _cogThrowClip;

    private int currentHealth;
    private bool _isInvincible;
    private float _invincibleTimer;
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private AudioSource _audioSource;
    private Vector2 _lookDirection = new Vector2(0,-1);

    private Vector2 _input;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource= GetComponent<AudioSource>();
        _healthEffect.Stop();
        currentHealth = _maxHealth;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        _input = new Vector2(horizontalInput, verticalInput);
        
        if(!Mathf.Approximately(_input.x, 0.0f) || !Mathf.Approximately(_input.y, 0.0f))
        {
            _lookDirection.Set(_input.x, _input.y);
            _lookDirection.Normalize();
        }
        
        _animator.SetFloat("Look X", _lookDirection.x);
        _animator.SetFloat("Look Y", _lookDirection.y);
        _animator.SetFloat("Speed", _input.magnitude);
        
        if (_isInvincible)
        {
            _invincibleTimer -= Time.deltaTime;
            if (_invincibleTimer < 0)
                _isInvincible = false;
        }
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
            _healthEffect.Play();
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(_rigidbody.position + Vector2.up * 0.2f, _lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                hit.collider.TryGetComponent(out NonPlayerCharacter npc);
                npc.DisplayDialog();
            }
        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = _rigidbody.position;
        position.x =  _moveSpeed * _input.x;
        position.y = _moveSpeed * _input.y;

        _rigidbody.velocity = position;    
    }
    
    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (_isInvincible) return;
            _isInvincible = true;
            _invincibleTimer = _timeInvincible;
            _animator.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, _maxHealth);
        
        UIHealthBar.instance.SetValue(currentHealth / (float)_maxHealth);
    }

    private void Launch()
    {
        GameObject projectileObject = Instantiate(_projectilePrefab, (Vector2)transform.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(_lookDirection, _throwForce);

        _animator.SetTrigger("Launch");
        _audioSource.PlayOneShot(_cogThrowClip);
    }
}