using UnityEngine;

public class TopDownController : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] GameObject playerObject;

    [SerializeField] Vector3 targetPosition;

    [SerializeField] float speed;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, 10000)){
                Debug.Log(hitInfo.transform.gameObject.name);
                targetPosition = hitInfo.point;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(Vector3.Distance(playerObject.transform.position, targetPosition)) > 2f)
        {
            playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, targetPosition, speed);    
        }
    }
}
