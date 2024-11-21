using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_PlayerInputManager : MonoBehaviour
{
    public static SC_PlayerInputManager instance;
    private PlayerInputManager playerInputManager;

    public List<GameObject> players;
    public List<Transform> spawnPoints;
    private void Awake()
    {
        if (instance == null || instance == this.gameObject)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerInputManager = GetComponent<PlayerInputManager>();
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Transform spwawnPoints = GameObject.Find("SpawnPoints").transform;
        for (int i = 0; i < spwawnPoints.childCount; i++) {
            spawnPoints.Add(spwawnPoints.GetChild(i).transform);
        }
    }
    //public void OnPlayerConnected(NetworkPlayer player)
    //{

    //}
    public void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
        playerInputManager.onPlayerLeft += RemovePlayer;
    }
    public void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
        playerInputManager.onPlayerLeft -= RemovePlayer;
    }
    public void AddPlayer(PlayerInput player)
    {
        players.Add(player.gameObject);
        player.transform.position = spawnPoints[players.Count - 1].position;
    }
    public void RemovePlayer(PlayerInput player)
    {
        players.Remove(player.gameObject);
        Destroy(player.gameObject);
    }
    public void EnableDisableControls(bool enable)
    {
        foreach (GameObject player in players)
        {
            SC_PlayerController playerCT = player.GetComponent<SC_PlayerController>();
            playerCT.EnableOrDisable(playerCT.move, enable);
        }
    }
}
