using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToRightPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(0, -90, 0);
    }

}
