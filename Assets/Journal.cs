using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

public class Journal : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Vector3 hiddenPos;
    [SerializeField] Vector3 shownPos;
    Vector3 oldPos;
    Vector3 targetPos;

    [SerializeField] float duration = 1f;
    float timer = 1f;

    bool inTransition;
    bool isShown = false;

    private void Update()
    {
        if (timer < duration)
        {
            timer += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(oldPos, targetPos, timer / duration);
        }
        else
        {
            inTransition = false;
        }
    }

    [Button]
    public void SetCurrentPositionToHiddenPosition()
    {
        transform.localPosition = hiddenPos;
    }

    [Button]
    public void SetCurrentPositionToShownPosition()
    {
        transform.localPosition = shownPos;
    }

    [Button]
    public void SetHiddenPositionToCurrentPosition()
    {
        hiddenPos = transform.localPosition;
    }

    [Button]
    public void SetShownPositionToCurrentPosition()
    {
        shownPos = transform.localPosition;
    }

    void NewTargetPos(Vector3 targetPos)
    {
        inTransition = true;
        timer = 0;
        oldPos = transform.localPosition;
        this.targetPos = targetPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!inTransition)
        {
            if (isShown)
            {
                NewTargetPos(hiddenPos);
            }
            else
            {
                NewTargetPos(shownPos);
            }
            
        }
        isShown = !isShown;
    }
}
