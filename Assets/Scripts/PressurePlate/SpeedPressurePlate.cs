using Codice.Client.BaseCommands;
using System.Collections;
using UnityEngine;

public class SpeedPressurePlate : BasePressurePlate
{
    [SerializeField] float _timer;
    [SerializeField] int _speed;
    protected override void Activate(Collider other)
    {
        if (other.gameObject.TryGetComponent<ColliderListener>(out ColliderListener colliderListener))
        {
            StartCoroutine(AddTempMoveSpeed(colliderListener.PlayerController));   
        }
    }

    IEnumerator AddTempMoveSpeed(PlayerController playerController)
    {
        float time = 0f;
        playerController.MovementSpeed += _speed;
        while (time < _timer)
        {
            time += Time.deltaTime;
            yield return null;
        }
        playerController.MovementSpeed -= _speed;
    }
}
