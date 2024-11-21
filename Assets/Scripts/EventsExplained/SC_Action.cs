using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Action : MonoBehaviour
{

    // Delegates allow us to store a function in variable.
    public delegate void WorkingWithDelegate();
    public delegate bool ActionWithDelegate(int n);
    public delegate void AnonymousMethod();

    public WorkingWithDelegate workingWithDelegate;
    public ActionWithDelegate actionWithDelegate;
    public AnonymousMethod anonymousMethod;

    public Action doubleAction;
    public Action<int, float, bool> testingAction;
    public Func<bool> testFunc;
    public Func<int, bool> retrunTypeIslastParameter;

    void Start()

    {
        workingWithDelegate += DelegateFunc;
        workingWithDelegate += SecoundDelegateFunc;
        workingWithDelegate();
        workingWithDelegate -= SecoundDelegateFunc;
        workingWithDelegate();

        actionWithDelegate = ThirdDelegateFunc;
        Debug.Log(actionWithDelegate(6));

        // Lamba epressions.
        actionWithDelegate = (int i) => { return i < 4; };
        Debug.Log(actionWithDelegate(3));

        anonymousMethod = delegate () { Debug.Log("AnonmousMethod"); };
        anonymousMethod();
        anonymousMethod = () => { Debug.Log("Lamda expression"); };
        anonymousMethod();

        testingAction = (int i, float f, bool b) => { Debug.Log(i + " " + f + " " + b); };
        testingAction(1, 2, true);

        testFunc = () => false;
        retrunTypeIslastParameter = (int i) => i < 4;
        Debug.Log(retrunTypeIslastParameter(3));
        //testingAction = ActionTesting;
        //testingAction(1, 4, true);



    }

    // Update is called once per frame
    void Update()
    {

    }


    private void DelegateFunc()
    {
        Debug.Log("This funcion stored inside delegate varable");
    }

    private void SecoundDelegateFunc()
    {
        Debug.Log("Ok");

    }

    private bool ThirdDelegateFunc(int n)
    {
        return n < 5;
    }


    private void ActionTesting(int n, float f, bool b)
    {
        Debug.Log("Here you go" + " " + n + " " + f + " " + b);
    }

}
