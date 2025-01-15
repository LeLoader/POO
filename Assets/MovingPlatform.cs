using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    [SerializeField] Transform platform;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float duration;

    private void Update()
    {
        platform.position = Vector3.Lerp(start.position, end.position, curve.Evaluate(Mathf.PingPong(Time.time, duration) / duration));
    }
}
