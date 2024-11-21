using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SC_Event : MonoBehaviour
{
    public event EventHandler OnSpacePressed;



    public event EventHandler<OnAlreadyGotHitted> OnGettingHit;
    public class OnAlreadyGotHitted : EventArgs
    {
        public int takeDamage;
    }
    public int takeDamage;


    // making eventfor delegate
    public event TryingDelegates OnFloatEvent;
    public delegate void TryingDelegates(float f);

    public event Action<bool, int, float, string> OnActionEvent;


    public UnityEvent OnUnityEvent;

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

        if (Input.GetKeyDown(KeyCode.H))
        {

            //Firing the event.
            OnGettingHit?.Invoke(this, new OnAlreadyGotHitted { takeDamage = takeDamage });

        }


        if (Input.GetKeyDown(KeyCode.G))
        {

            //Firing the event.
            OnFloatEvent?.Invoke(-4f);

        }


        if (Input.GetKeyDown(KeyCode.J))
        {

            //Firing the event.
            OnActionEvent?.Invoke(true, 5, 3f, "Action are difficlty");

        }

        if (Input.GetKeyDown(KeyCode.U))
        {

            //Firing the event.
            OnUnityEvent?.Invoke();

        }
    }





}
