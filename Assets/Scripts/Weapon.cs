using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField, Required] DamageSource _damageSource;

    public void ActivateFor(float time)
    {
        if(time > 0)
        {
            _damageSource.StartCoroutine(_damageSource.ActivateFor(time));
        }
        else
        {
            Debug.LogWarning("Attack time cannot be negative");
        }
    }
}
