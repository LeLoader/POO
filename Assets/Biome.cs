using UnityEngine;
using UnityEngine.UI;

public class Biome : MonoBehaviour
{
    [SerializeField] Outline outline;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerOnMap>(out PlayerOnMap playerOnMap))
        {
            outline.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerOnMap>(out PlayerOnMap playerOnMap))
        {
            outline.enabled = false;
        }
    }
}
