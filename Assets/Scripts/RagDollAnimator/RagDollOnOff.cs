using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollOnOff : MonoBehaviour
{

    [SerializeField] CapsuleCollider playerCollider;
    [SerializeField] Rigidbody playerRigidbody;
    [SerializeField] Animator playerAnimator;
    [SerializeField] List<Collider> ragDollParts = new List<Collider>();
    private void Awake()
    {

        playerCollider = GetComponent<CapsuleCollider>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        SetRagDollParts();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(TestRagDoll());
        }
    }

    private void SetRagDollParts()
    {

        Collider[] ragdollColliders = this.gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider colliders in ragdollColliders)
        {
            if (colliders.gameObject != this.gameObject)
            {

                colliders.isTrigger = true;
                ragDollParts.Add(colliders);
            }
        }
    }

    public void TurnOnRagDoll()
    {
        playerRigidbody.useGravity = false;
        playerCollider.enabled = false;
        playerAnimator.enabled = false;
        playerAnimator.avatar = null;


        foreach (Collider coliders in ragDollParts)
        {
            coliders.isTrigger = false;
            coliders.attachedRigidbody.velocity = Vector3.zero;
        }
    }


    private IEnumerator TestRagDoll()
    {
        yield return new WaitForSeconds(4);
        playerRigidbody.AddForce(200f * Vector3.up);
        yield return new WaitForSeconds(0.5f);
        TurnOnRagDoll();
    }

}
