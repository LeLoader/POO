using TMPro;
using UnityEngine;

public class CameraReflect : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, 10000))
            {
                Debug.DrawRay(ray.origin, ray.direction * 10000, Color.red, 1f);
                if (hitInfo.transform.gameObject.layer == 6)
                {
                    Debug.Log("Hit reflect");
                    RaycastHit reflectedHitInfo;
                    Vector3 newDir = Vector3.Reflect(ray.direction * 10000, -hitInfo.transform.forward);
                    if (Physics.Raycast(hitInfo.point, newDir, out reflectedHitInfo, 10000))
                    {   
                        Debug.DrawRay(hitInfo.point, newDir * 10000, Color.yellow, 1f);
                        if (reflectedHitInfo.transform.gameObject.CompareTag("Player"))
                        {
                            Debug.Log("Trouvé!!!!!!");
                        }
                    }
                }
            }
        }
    }
}
