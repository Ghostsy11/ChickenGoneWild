using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SC_Event : MonoBehaviour
{

    public event EventHandler OnSpacePressed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            //Firing the event.

            // option 1 
            if (OnSpacePressed != null)
            {

                OnSpacePressed(this, EventArgs.Empty);
            }

            // OnSpacePressed?.Invoke(this, EventArgs.Empty);

        }

    }

    //private void TestingOnSpacePressed(object sender, EventArgs e)
    //{
    //    Debug.Log("SpacePressed");

    //}

}
