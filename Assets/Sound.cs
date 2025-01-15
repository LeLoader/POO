using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] float timerOffset;
    [SerializeField] float speed = 50;

    private void Start()
    {
        Destroy(gameObject, SoundSpawner.instance.timerBetweenSound + timerOffset);    
    }

    void Update()
    {
        transform.position -= new Vector3(0, speed) * Time.deltaTime;
    }
}
