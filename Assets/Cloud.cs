using UnityEngine;

public class Cloud : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerOnMap>(out PlayerOnMap player))
        {
            Destroy(gameObject);
        }
    }
}