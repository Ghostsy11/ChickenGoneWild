using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] const string ChickenRunning = "ChickenRun";
    [SerializeField] const string ChickenIdle = "Idle";


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void RunChicken()
    {
        animator.SetBool(ChickenRunning, true);
    }

}
