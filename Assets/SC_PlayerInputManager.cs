using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class SC_PlayerInputManager : MonoBehaviour
{
    public static SC_PlayerInputManager instance;
    private PlayerInputManager playerInputManager;
    private PlayerInputActions inputActions;

    public List<GameObject> players;
    private int readyAmmount;
    public List<Transform> spawnPoints;

    private InputActionReference move, jump, attack, customizationMove, customizationConfirm;
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
        SetSpawnPoints();
        MoveAllPlayersToSpawnPoint();
    }

    private void SetSpawnPoints()
    {
        spawnPoints.Clear();
        Transform spwawnPoints = GameObject.Find("SpawnPoints").transform;
        for (int i = 0; i < spwawnPoints.childCount; i++)
        {
            spawnPoints.Add(spwawnPoints.GetChild(i).transform);
        }
    }
    private void MoveAllPlayersToSpawnPoint()
    {
        for (int i = 0; i < players.Count; i++)
        {
            SetPlayerSpawnPositions(players[i].GetComponent<PlayerInput>(), i);
        }
    }
    private void SetPlayerSpawnPositions(PlayerInput player, int spawnPointI)
    {
        player.transform.position = spawnPoints[spawnPointI].position;
        player.transform.rotation = spawnPoints[spawnPointI].rotation;
    }
    //public void OnPlayerConnected(NetworkPlayer player)
    //{

    //}
    public void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
        playerInputManager.onPlayerLeft += RemovePlayer;

        SC_EventManager.onGameLoaded += GameLoaded;
    }
    public void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
        playerInputManager.onPlayerLeft -= RemovePlayer;

        SC_EventManager.onGameLoaded -= GameLoaded;
    }
    public void AddPlayer(PlayerInput player)
    {
        players.Add(player.gameObject);
        SetPlayerSpawnPositions(player, players.Count - 1);
        if (players.Count <= 1)
        {
        }
    }
    public void RemovePlayer(PlayerInput player)
    {
        if (players.Count <= 1)
        {
            inputActions = null;
        }
        players.Remove(player.gameObject);
        Destroy(player.gameObject);
    }

    public void AddReady(bool ready)
    {
        if (ready)
        {
            readyAmmount++;
            if (readyAmmount >= players.Count)
            {
                StartGame();
            }
        }
        else if (readyAmmount > 0)
        {
            readyAmmount--;
        }
    }
    private void StartGame()
    {
        SC_SceneManager.instance.LoadScene(1);
    }
    public void GameLoaded()
    {
        SetSpawnPoints();
        MoveAllPlayersToSpawnPoint();
    }
}