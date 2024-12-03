using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FocusLevel : MonoBehaviour
{
    public float halfBoundsX = 20f;
    public float halfBoundsY = 15f;
    public float halfBoundsZ = 15f;

    public Bounds focusBounds;

    // Update is called once per frame
    void Update()
    {
        Vector3 position = gameObject.transform.position;
        Bounds bounds = new Bounds();
        bounds.Encapsulate(new Vector3(position.x - halfBoundsX, position.y - halfBoundsY, position.z - halfBoundsZ));
        bounds.Encapsulate(new Vector3(position.x + halfBoundsX, position.y + halfBoundsY, position.z + halfBoundsZ));
        focusBounds = bounds;
    }
}
