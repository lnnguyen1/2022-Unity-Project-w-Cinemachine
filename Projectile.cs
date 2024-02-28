using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _maxRange = 100f;
    
    private Rigidbody2D _rigidbody;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    public void Launch(Vector2 direction, float force)
    {
        _rigidbody.AddForce(direction * force);
    }
    
    void Update()
    {
        if(transform.position.magnitude > _maxRange)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        other.collider.TryGetComponent(out EnemyController enemyController);
        enemyController.Fix();
        Destroy(gameObject);
    }
}
