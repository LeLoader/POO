using Codice.Client.BaseCommands;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePressurePlate : MonoBehaviour
{
    List<Collider> collidersOnPressurePlate = new();
    public UnityEvent OnPress;
    Vector3 targetPos;

    private void Awake()
    {
        targetPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Activate(other);
        collidersOnPressurePlate.Add(other);
        if(collidersOnPressurePlate.Count == 1)
        {
            targetPos = transform.position - new Vector3(0f, 0.04f, 0f);
        }
    }

    private void Update()
    {
        if (!(transform.position - targetPos).AlmostZero())
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.01f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        collidersOnPressurePlate.Remove(other);
        if (collidersOnPressurePlate.Count == 0)
        {
            targetPos = transform.position + new Vector3(0f, 0.04f, 0f);
        }
    }

    protected virtual void Activate(Collider target)
    {
        OnPress?.Invoke();
    }
}
