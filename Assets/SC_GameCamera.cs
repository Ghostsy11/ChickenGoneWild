using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_GameCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private List<GameObject> players = new();

    public SC_FocusLevel focusLevel;

    [SerializeField] private float depthUpdateSpeed = 5f;
    [SerializeField] private float angleUpdateSpeed = 7f;
    [SerializeField] private float positionUpdateSpeed = 5f;

    [SerializeField] private float depthMax = -10f;
    [SerializeField] private float depthMin = -22f;

    [SerializeField] private float angleMax = 11;
    [SerializeField] private float angleMin = 3;

    private float cameraEulerX;
    private Vector3 cameraPosition;

    [Tooltip("adjust to follow player more outside the map between 1 and 2")]
    [SerializeField] private float playerExtents = 2;
    private void Awake()
    {
        
    }
    private void Start()
    {
        cam = Camera.main;
        players = GameObject.Find("PlayerManager").GetComponent<SC_PlayerInputManager>().players;
        players.Add(focusLevel.gameObject);
    }
    private void LateUpdate()
    {
        CalculateCameraLocation();
        MoveCamera();
    }
    private void MoveCamera()
    {
        Vector3 position = transform.position;
        if (position != cameraPosition) 
        {
            Vector3 targetPosition = Vector3.zero;
            targetPosition.x = Mathf.MoveTowards(position.x, cameraPosition.x, positionUpdateSpeed * Time.deltaTime);
            targetPosition.y = Mathf.MoveTowards(position.y, cameraPosition.y, positionUpdateSpeed * Time.deltaTime);
            targetPosition.z = Mathf.MoveTowards(position.z, cameraPosition.z, positionUpdateSpeed * Time.deltaTime);
            gameObject.transform.position = targetPosition;
        }

        Vector3 localEulerAngles = transform.localEulerAngles;
        if (localEulerAngles.x != cameraEulerX)
        {
            Vector3 targetEulerAngles = new Vector3(cameraEulerX, localEulerAngles.y, localEulerAngles.z);
            gameObject.transform.localEulerAngles = Vector3.MoveTowards(localEulerAngles, targetEulerAngles, angleUpdateSpeed * Time.deltaTime);
        }
    }
    private void CalculateCameraLocation()
    {
        Vector3 averageCenter = Vector3.zero;
        Vector3 totalPosition = Vector3.zero;
        Bounds playerBounds = new Bounds();

        for (int i = 0; i < players.Count; i++)
        {
            Vector3 playerPosition = players[i].transform.position;

            if (!focusLevel.focusBounds.Contains(playerPosition))
            {
                float playerX = Mathf.Clamp(playerPosition.x, focusLevel.focusBounds.min.x, focusLevel.focusBounds.max.x);
                float playerY = Mathf.Clamp(playerPosition.y, focusLevel.focusBounds.min.y, focusLevel.focusBounds.max.y); ;
                float playerZ = Mathf.Clamp(playerPosition.z, focusLevel.focusBounds.min.z, focusLevel.focusBounds.max.z); ;
                playerPosition = new Vector3(playerX, playerY, playerZ);
            }
            totalPosition += playerPosition;
            playerBounds.Encapsulate(playerPosition);
        }
        averageCenter = (totalPosition/players.Count);

        float extents = (playerBounds.extents.x + playerBounds.extents.y);
        float lerpPercent = Mathf.InverseLerp(0, (focusLevel.halfBoundsX + focusLevel.halfBoundsY) / playerExtents, extents);
        
        float depth = Mathf.Lerp(depthMax, depthMin, lerpPercent);
        float angle = Mathf.Lerp(angleMax, angleMin, lerpPercent);

        cameraEulerX = angle;
        cameraPosition = new Vector3(averageCenter.x, averageCenter.y, depth);
    }
}
