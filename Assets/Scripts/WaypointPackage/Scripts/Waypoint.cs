using System;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private string label = "DefaultWaypointName";
    public string Label
    {
        get { return label; }
        set { label = value; }
    }

    [SerializeField] private Color color = Color.white;
    public Color Color
    {
        get { return color; }
        set { color = value; }
    }

    public Vector3 Position { get; set; } = Vector3.zero;

    //  VISUAL

    private GameObject compassRepresentation;
    public GameObject CompassRepresentation
    {
        get { return compassRepresentation; }
        set
        {
            if (compassRepresentation == null)
            {
                compassRepresentation = value;
            }
        }
    }

    private GameObject managerRepresentation;
    public GameObject ManagerRepresentation
    {
        get { return managerRepresentation; }
        set
        {
            if (managerRepresentation == null)
            {
                managerRepresentation = value;
            }
        }
    }

    private GameObject mapRepresentation;
    public GameObject MapRepresentation
    {
        get { return mapRepresentation; }
        set
        {
            if (mapRepresentation == null)
            {
                mapRepresentation = value;
            }
        }
    }

    [SerializeField] public WaypointUI WaypointUI { get; set; }

    //Events
    public static event Action<Waypoint> OnWaypointDeleted;

    private void Awake()
    {
        Position = transform.position;
    }

    public float GetAngle(Transform from)
    {
        Vector3 forwardVector = new(from.transform.forward.x, 0, from.transform.forward.z);
        Vector3 toWaypointVectorTemp = transform.position - from.transform.position;
        Vector3 toWaypointVector = new(toWaypointVectorTemp.x, 0, toWaypointVectorTemp.z);
        float finalAngle = Mathf.Acos(
            Vector3.Dot(forwardVector, toWaypointVector)
            /
            (forwardVector.magnitude * toWaypointVector.magnitude));
        if (Vector3.Dot(Vector3.Cross(forwardVector, toWaypointVector), Vector3.up) < 0)
        {
            finalAngle = -finalAngle;
        }
        return Mathf.Rad2Deg * finalAngle;
    }
    
    public void ChangeName(string value)
    {
        name = value;
    }

    public void Delete(bool destroyGameObject)
    {
        Destroy(compassRepresentation);
        Destroy(managerRepresentation);
        Destroy(mapRepresentation);

        OnWaypointDeleted?.Invoke(this);

        if (destroyGameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
}
