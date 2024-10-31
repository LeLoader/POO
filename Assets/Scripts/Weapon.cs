using UnityEngine;

public class Weapon : MonoBehaviour
{
    // [SerializeField] Health _targetHealthComponent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // _targetHealthComponent.TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(this);
        }
    }
}
