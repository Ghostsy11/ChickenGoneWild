using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_ReadyUpUI : MonoBehaviour
{
    public static SC_ReadyUpUI instance;

    [SerializeField] private GameObject readyPrefab;
    private List<GameObject> readyObjects = new();

    [SerializeField] private Sprite readySprite;
    [SerializeField] private Sprite readyUpSprite;

    [SerializeField] private List<Color> baseColors = new();
    [SerializeField] private Color readyColor;

    private List<GameObject> players = new();

    [SerializeField]private GameObject timer;
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
                    readyObjects[i].GetComponent<Image>().color = readyColor;
                    readyObjects[i].transform.GetChild(0).GetComponent<Image>().sprite = readySprite;
                }
                else
                {
                    readyObjects[i].GetComponent<Image>().color = baseColors[i];
                    readyObjects[i].transform.GetChild(0).GetComponent<Image>().sprite = readyUpSprite;
                }
            }
        }
    }
    public void SetColor(Color color, GameObject player)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == player)
            {
                baseColors[i] = color;
                readyObjects[i].GetComponent<Image>().color = baseColors[i];
            }
        }
    }
    public void SetPlayers(List<GameObject> list)
    {
        players = list;
        if (players.Count > readyObjects.Count)
        {
            GameObject obj = Instantiate(readyPrefab, gameObject.transform);
            baseColors.Add(Color.white);
            readyObjects.Add(obj);
            readyObjects[^1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "P" + readyObjects.Count;
        }
        if (players.Count < readyObjects.Count)
        {
            Destroy(readyObjects[readyObjects.Count]);
            readyObjects.RemoveAt(readyObjects.Count);
            baseColors.RemoveAt(baseColors.Count);
        }
    }
    public void SetTimer(int i)
    {
        if (i <= 0)
        {
            timer.SetActive(false);
        }
        else
        {
            timer.SetActive(true);
            timer.GetComponent<TextMeshProUGUI>().text = i.ToString();
        }
    }
}
