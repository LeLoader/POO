using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpawner : MonoBehaviour
{
    [SerializeField] public float timerBetweenSound;
    [SerializeField] List<Transform> spawners;
    [SerializeField] GameObject soundPrefab;
    public static SoundSpawner instance;

    private void Start()
    {
        instance = this;
        StartCoroutine(SpawnASound());
    }

    IEnumerator SpawnASound() 
    {
        while (true)
        {
            int randomNumber = Random.Range(0, spawners.Count);
            Instantiate(soundPrefab, spawners[randomNumber]);
            yield return new WaitForSeconds(timerBetweenSound);
        }
    }
}
