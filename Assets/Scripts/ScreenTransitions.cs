using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTransitions : MonoBehaviour
{

    public static ScreenTransitions instance;

    // Start is called before the first frame update
    void Start()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public void NextLevel()
    {
        // Play fade in animation
        // wait duration of animation
        //LoadScence
    }

    // Update is called once per frame
    void Update()
    {

    }
}
