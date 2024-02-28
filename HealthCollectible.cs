using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private AudioClip _collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        if (controller != null)
        {
            if(controller.GetCurrentHealth < controller.GetMaxHealth)
            {
                controller.ChangeHealth(1);
                Destroy(gameObject);
                controller.PlaySound(_collectedClip);
            }
        }
    }
}
