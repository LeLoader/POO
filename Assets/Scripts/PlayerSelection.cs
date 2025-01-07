using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] float velocityThresholdToMagnetise = 0.1f;
    bool isMagnetized;
    const int MIN_PLAYER = 2;
    const int MAX_PLAYER = 6;

    void Start()
    {
        
    }

    void Update()
    {
        if (Mathf.Abs(scrollRect.velocity.y) < velocityThresholdToMagnetise && !isMagnetized)
        {
            Debug.Log($"Magnetize: current velocity {Mathf.Abs(scrollRect.velocity.y)}");
            isMagnetized = true;
            Debug.Log(GetStep());
        }
    }

    public void ScrollRectOnValueChanged()
    {
        Debug.Log("OnValueChanged");
        isMagnetized = false;
    }

    void SelectPlayerCount()
    {
        Debug.Log($"Game start with {GetStep()} players");
    }

    int GetStep()
    {
        // Value: 1 = min
        // Value: 0 = max
        // Should ignore negative value, otherwise gives a negative step (same with max) when scroll bar goes out of clamp due to inertia
        int step = (int)Mathf.Floor(scrollRect.verticalScrollbar.value * (MAX_PLAYER - MIN_PLAYER + 1));
        Debug.Log(scrollRect.verticalScrollbar.value * (MAX_PLAYER - MIN_PLAYER + 1));
        return step;
    }
}
