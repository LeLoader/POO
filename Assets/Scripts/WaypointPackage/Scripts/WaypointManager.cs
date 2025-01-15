using System.Collections;
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
    [SerializeField] GameObject mapObjectsWrapper;
    [SerializeField] GameObject PFBmapWaypointUI;
    [SerializeField] float distanceFactor = 10;
    [SerializeField] GameObject playerRepresentation;
    [SerializeField] GameObject player;
    [Space]
    [SerializeField] GameObject PFBmarker;
    [SerializeField] float timeBetweenMarkers = 5;
    [SerializeField] int maxNumberOfMarkers = 10;

    public bool IsUIOpen { get; private set; }
    public bool IsMapOpen { get; private set; }

    List<Color> colors = new List<Color>()
    {
        Color.red,
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.blue,
        Color.magenta,
        Color.black,
    };
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
        foreach (var waypoint in FindObjectsByType<Waypoint>(FindObjectsSortMode.None))
        {
            OnWaypointCreated(waypoint);
        }
        StartCoroutine(PlaceMarker());
    }

    private void Update()
    {
        foreach (var waypoint in waypoints)
        {
            Vector3 newPos = new(waypoint.GetAngle(mainCamera.transform) * pixelPerDegree, waypoint.CompassRepresentation.transform.localPosition.y, 0);
            waypoint.CompassRepresentation.transform.localPosition = newPos;

            //{ // Compass
            //    Vector3 screenPos = mainCamera.WorldToScreenPoint(waypoint.transform.position);
            //    Vector3 newPos = new(screenPos.x, waypoint.CompassRepresentation.transform.position.y, 0);
            //    waypoint.CompassRepresentation.transform.position = newPos;
            //}       
        }

        { // Player on map
            playerRepresentation.transform.localPosition = new Vector2(player.transform.position.x, player.transform.position.z);
            playerRepresentation.transform.rotation = Quaternion.AngleAxis(mainCamera.transform.rotation.eulerAngles.y, Vector3.back);
        }

        if (Input.GetKeyDown(KeyCode.Z)) //W 
        {
            ToggleUI();
        }
        if (Input.GetKeyDown(KeyCode.M))// ,
        {
            ToggleMap();
        }
    }

    private IEnumerator PlaceMarker()
    {
        while (true)
        {
            var obj = Instantiate(PFBmarker, mapObjectsWrapper.transform);
            obj.transform.localPosition = playerRepresentation.transform.localPosition;
            obj.GetComponent<Marker>().CallDestroyIn(timeBetweenMarkers * maxNumberOfMarkers);
            yield return new WaitForSeconds(timeBetweenMarkers);
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
        waypoint.WaypointUI.deleteButton.onClick.AddListener(() =>
        {
            waypoint.Delete(false);
        });
        waypoint.WaypointUI.forceDeleteButton.onClick.AddListener(() =>
        {
            waypoint.Delete(true);
        });
        waypoint.WaypointUI.changeNameButton.onClick.AddListener(() =>
        {
            // CHANGE NAME
        });
        waypoint.WaypointUI.changeColorButton.onClick.AddListener(() =>
        {
            CycleThroughColor(waypoint);
        });

        waypoint.MapRepresentation = Instantiate(PFBmapWaypointUI, mapObjectsWrapper.transform);
        waypoint.MapRepresentation.transform.position = new Vector2(waypoint.transform.position.x, waypoint.transform.position.z);
        WaypointUI waypointui = waypoint.ManagerRepresentation.GetComponent<WaypointUI>();
        waypointui.colorImage.color = waypoint.Color;
    }

    private void CycleThroughColor(Waypoint waypoint)
    {
        int rdm = Random.Range(0, colors.Count);
        waypoint.ChangeColor(colors[rdm]);
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
