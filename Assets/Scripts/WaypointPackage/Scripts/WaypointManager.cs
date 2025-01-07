using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointManager : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] List<Waypoint> waypoints;
    [SerializeField] GameObject PFBwaypoint;
    [SerializeField] Transform whereToSpawn;
    
    [Header("Compass")]
    [SerializeField] Transform pov;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject compassBackground;
    [SerializeField] GameObject PFBwaypointCompassUI;

    float pixelPerDegree = (float)1920 / 60;

    [Header("WaypointScreen")]
    [SerializeField] GameObject screen;
    [SerializeField] GameObject waypointsWrapper;
    [SerializeField] GameObject PFBwaypointUI;

    [Header("MapScreen")]
    [SerializeField] GameObject mapScreen;
    [SerializeField] GameObject mapWaypointsWrapper;
    [SerializeField] GameObject PFBmapWaypointUI;
    public bool IsUIOpen { get; private set; }
    public bool IsMapOpen { get; private set; }


    private void Awake()
    {
        Waypoint.OnWaypointDeleted += OnWaypointDeleted;
        mainCamera = Camera.main;
        if (pov == null)
        {
            pov = mainCamera.transform;
        }
        if (whereToSpawn == null)
        {
            whereToSpawn = pov;
        }
        if (mainCamera != null)
        {
            pixelPerDegree = Screen.width / mainCamera.fieldOfView / 2;
        }
    }

    private void Start()
    {
        foreach (var waypoint in FindObjectsByType<Waypoint>(FindObjectsSortMode.None)){
            OnWaypointCreated(waypoint);
        }
    }

    private void Update()
    {
        foreach (var waypoint in waypoints)
        {
            //Vector3 newPos = new(waypoint.GetAngle(pov) * pixelPerDegree, waypoint.CompassRepresentation.transform.localPosition.y, 0);
            //waypoint.CompassRepresentation.transform.localPosition = newPos;

            Vector3 screenPos = mainCamera.WorldToScreenPoint(waypoint.transform.position);
            Vector3 newPos = new(screenPos.x, waypoint.CompassRepresentation.transform.position.y, 0);
            waypoint.CompassRepresentation.transform.position = newPos;

            Vector3 newMapPos = waypoint.transform.position - mainCamera.transform.position;
            Vector3 newRot = new(0, 0, mainCamera.transform.rotation.y);
            Vector3 finalPosition = Quaternion.Euler(newRot) * (newMapPos * 10);
            waypoint.MapRepresentation.transform.localPosition = finalPosition;
        }

        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            ToggleUI();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }
    }

    public void AddWaypoint()
    {
        Waypoint waypoint = Instantiate(PFBwaypoint, whereToSpawn.position, Quaternion.identity).GetComponent<Waypoint>();
        OnWaypointCreated(waypoint);
    }

    private void OnWaypointCreated(Waypoint waypoint)
    {
        waypoints.Add(waypoint);
        waypoint.CompassRepresentation = Instantiate(PFBwaypointCompassUI, compassBackground.transform);
        waypoint.CompassRepresentation.GetComponent<Image>().color = waypoint.Color;

        waypoint.ManagerRepresentation = Instantiate(PFBwaypointUI, waypointsWrapper.transform);
        waypoint.WaypointUI = waypoint.ManagerRepresentation.GetComponent<WaypointUI>();
        waypoint.WaypointUI.colorImage.color = waypoint.Color;
        waypoint.WaypointUI.labelText.text = waypoint.Label;
        waypoint.WaypointUI.deleteButton.onClick.AddListener(() => { 
            waypoint.Delete(false); 
        });
        waypoint.WaypointUI.forceDeleteButton.onClick.AddListener(() => {
            waypoint.Delete(true);
        });
        waypoint.WaypointUI.changeNameButton.onClick.AddListener(() => {
            // CHANGE NAME
        });
        waypoint.WaypointUI.changeColorButton.onClick.AddListener(() => {
            // CHANGE COLOR
        });

        waypoint.MapRepresentation = Instantiate(PFBmapWaypointUI, mapWaypointsWrapper.transform);
        WaypointUI waypointui = waypoint.ManagerRepresentation.GetComponent<WaypointUI>();
        waypointui.colorImage.color = waypoint.Color;
    }

    private void OnWaypointDeleted(Waypoint waypoint)
    {
        waypoints.Remove(waypoint);
    }

    public void ToggleUI()
    {
        if (IsUIOpen)
        {
            CloseUI();
        }
        else
        {
            OpenUI();
        }
    }

    public void OpenUI()
    {
        IsUIOpen = true;
        screen.SetActive(true);
        if (IsMapOpen)
        {
            CloseMap();
        }
    }

    public void CloseUI()
    {
        IsUIOpen = false;
        screen.SetActive(false);
    }

    public void ToggleMap()
    {
        if (IsMapOpen)
        {
            CloseMap();
        }
        else
        {
            OpenMap();
        }
    }

    public void OpenMap()
    {
        IsMapOpen = true;
        mapScreen.SetActive(true);
        if (IsUIOpen)
        {
            CloseUI();
        }
    }

    public void CloseMap()
    {
        IsMapOpen = false;
        mapScreen.SetActive(false);
    }
}
