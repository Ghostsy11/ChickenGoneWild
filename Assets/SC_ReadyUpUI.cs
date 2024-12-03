using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ReadyUpUI : MonoBehaviour
{
    public static SC_ReadyUpUI instance;

    [SerializeField] private GameObject readyPrefab;
    private List<GameObject> readyObjects = new();

    [SerializeField] private Color baseColor;
    [SerializeField] private Color readyColor;

    private List<GameObject> players = new();
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    public void SetReady(bool ready, GameObject player)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == player)
            {
                if (ready)
                {
                    readyObjects[i].GetComponentInChildren<Renderer>().material.color = readyColor;
                }
                else
                {
                    readyObjects[i].GetComponentInChildren<Renderer>().material.color = baseColor;
                }
            }
        }
    }
    public void SetPlayers(List<GameObject> list)
    {
        players = list;
        if (players.Count > readyObjects.Count)
        {
            GameObject obj = Instantiate(readyPrefab, gameObject.transform);
            readyObjects.Add(obj);
        }
        if (players.Count < readyObjects.Count)
        {
            Destroy(readyObjects[^1]);
            readyObjects.RemoveAt(readyObjects.Count);
        }
    }
}
