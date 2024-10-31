using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class DamageSource : MonoBehaviour
{
    [SerializeField, Min(0)] int _damage;
    [SerializeField] Collider _collider;

    public IEnumerator ActivateFor(float time)
    {
        _collider.enabled = true;
        float timer = 0.0f;
        while (timer < time)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _collider.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DamageListener>(out DamageListener listener)){
            listener.TakeDamage(_damage);
        }
    }
}
