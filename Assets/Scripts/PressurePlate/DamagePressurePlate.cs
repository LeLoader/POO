using UnityEngine;

public class DamagePressurePlate : BasePressurePlate
{
    [SerializeField] int _damage;
    protected override void Activate(Collider other)
    {
        if (other.gameObject.TryGetComponent<DamageListener>(out DamageListener damageListener))
        {
            damageListener.TakeDamage(_damage);
        }
    }
}
