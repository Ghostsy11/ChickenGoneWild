using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpChicken : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject ChickenPickUpPointLocation;
    [SerializeField] Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // triangle to pick up
        if (Input.GetKeyDown(KeyCode.W))
        {
            PickingUpChicken();

        }

    }

    public void PickingUpChicken()
    {
        gameObject.transform.parent = ChickenPickUpPointLocation.transform;
        rigidbody.useGravity = false;
        transform.position = ChickenPickUpPointLocation.transform.position;
    }

    public void ThorwAfterPickUp()
    {
        gameObject.transform.parent = null;
        rigidbody.useGravity = true;
    }

}
