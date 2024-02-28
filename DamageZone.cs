using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        other.TryGetComponent(out RubyController controller);
        controller.TakeDamage(1);
    }
}
