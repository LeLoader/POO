using UnityEngine;

public class Marker : MonoBehaviour
{
    public void CallDestroyIn(float time)
    {
        Destroy(gameObject, time);
    }
}
