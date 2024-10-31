using UnityEngine;

public class DamageListener : MonoBehaviour
{
    [SerializeField] Health _health;

    public void TakeDamage(int value)
    {
        _health.TakeDamage(value);
    }
}
