using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private AudioClip _collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        TryGetComponent(out RubyController controller);
        if(controller.GetCurrentHealth < controller.GetMaxHealth)
        {
            controller.RestoreHealth(1);
            controller.PlaySound(_collectedClip);
            Destroy(gameObject);
        }
    }
}
