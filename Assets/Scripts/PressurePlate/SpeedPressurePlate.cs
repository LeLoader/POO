using Codice.Client.BaseCommands;
using System.Collections;
using UnityEngine;

public class SpeedPressurePlate : BasePressurePlate
{
    [SerializeField] float timer;
    [SerializeField] int _speed;
    protected override void Activate(Collider target)
    {
        if (target.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            Debug.Log("found player controller");
            StartCoroutine(AddTempMoveSpeed(playerController));   
        }
    }

    IEnumerator AddTempMoveSpeed(PlayerController playerController)
    {
        float time = 0f;
        playerController.MovementSpeed += _speed;
        while (time < timer)
        {
            time += Time.deltaTime;
            yield return null;
        }
        playerController.MovementSpeed -= _speed;
    }
}
