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
        SC_PlayerController playerScr = player.gameObject.GetComponent<SC_PlayerController>();
        playerScr.DisableControls(playerScr.move);
        playerScr.DisableControls(playerScr.attack);
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
    //public void EnableDisableAllPlayerControlAction(bool enable, List<InputAction> inputActions)
    //{
    //    foreach (GameObject player in players)
    //    {
    //        EnableDisablePlayerControlAction(player.GetComponent<SC_PlayerController>(), enable, inputActions);
    //    }
    //}
    //public void EnableDisablePlayerControlAction(SC_PlayerController playerSrc, bool enable, List<InputAction> inputActions)
    //{
    //    for (int i = 0; i < inputActions.Count; i++)
    //    {
    //        playerSrc.EnableOrDisable(inputActions[i], enable);
    //    }
    //}
}
