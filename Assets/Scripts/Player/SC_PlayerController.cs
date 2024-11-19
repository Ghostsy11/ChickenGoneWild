using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class SC_PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Collider baseCollider;
    [SerializeField] private Collider jumpCollider;
    [SerializeField] private Collider dodgeCollider;
    //public PlayerInputActions playerInput;
    [Header("Controls")]
    [SerializeField]
    private InputActionReference move, jump, attack;

    private enum PlayerState { grounded, jumping, double_jumping, hanging, dodging };
    [SerializeField] private PlayerState playerState = PlayerState.double_jumping;

    [Header("Climbing")]
    [Tooltip("Radius in which player checks for climable surface")]
    [SerializeField] private int searchRadius;
    [SerializeField] private LayerMask climableMask;
    [SerializeField] private float checkDistance;

    [SerializeField] private LayerMask groundMask;

    [Header("Run")]
    [SerializeField] private float maxSpeed = 5;
    private float speed = 0;
    private bool moving = false;
    [SerializeField] private int steps = 1000;
    [SerializeField] private int currentStep;

    [Header("Ramp")]
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 2f;

    [Header("Jump")]
    [SerializeField] private float jumpSpeed = 10;
    [SerializeField] private float edgeJumpTime = 0.1f;
    private void Awake()
    {
        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
    }
    // Start is called before the first frame update
    void Start()
    {
        //playerInput = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        CheckIfNearClimable();
        if (moving)
        {
            MoveHorizontal(move.action.ReadValue<Vector2>().x);
            MoveVertical(move.action.ReadValue<Vector2>().y);
        }
        else if (rb.velocity.x > 0 || rb.velocity.x < 0)
        {
            currentStep = CalculateCurrentStep();
            if (currentStep < 0)
            {
                currentStep = 0;
            }
        }
        //StepClimb();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((groundMask.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            playerState = PlayerState.grounded;
        }
    }
    private IEnumerator OnCollisionExit(Collision collision)
    {
        if ((groundMask.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            yield return new WaitForSeconds(edgeJumpTime);
            playerState = PlayerState.jumping;
        }
    }
    private void CheckIfNearClimable()
    {
        Ray ray = new Ray(transform.position, transform.up);
        Debug.DrawLine(ray.origin, ray.direction * 10, Color.red);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);
    }
    public void JumpAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (playerState == PlayerState.double_jumping) return;
            if (playerState == PlayerState.grounded)
            {
                playerState = PlayerState.jumping;
                Jump();
            }
            else if (playerState == PlayerState.jumping)
            {
                playerState = PlayerState.double_jumping;
                Jump();
            }
            else if (playerState == PlayerState.hanging)
            {
                HangJump();
            }
        }
    }

    private void HangJump()
    {

    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
    }

    public void MoveAction(InputAction.CallbackContext context)
    {
        moving = true;
        if (context.canceled)
        {
            moving = false;
        }
    }


    private void MoveHorizontal(float h)
    {
        speed = SpeedCalculation((int)h);
        rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
    }
    private void MoveVertical(float y)
    {

    }
    public void AttackAction(InputAction.CallbackContext context)
    {

    }
    private float SpeedCalculation(int dir)
    {
        float X = speed;
        if (speed < maxSpeed || speed > -maxSpeed)
        {
            if (dir > 0 && rb.velocity.x < 0 || dir < 0 && rb.velocity.x > 0)
            {
                currentStep = 0;
            }
            float i = currentStep;
            float N = steps;
            float B = 0;
            float A = maxSpeed * dir;
            if (i < N)
            {
                float v = i / N;
                v = v * v;
                X = (A * v) + (B * (1 - v));
                currentStep++;
            }
        }
        return X;
    }
    private int CalculateCurrentStep()
    {
        int step = steps / (int)(maxSpeed / Math.Abs(rb.velocity.x));
        return step;
    }
    //private void StepClimb()
    //{
    //    RaycastHit hitLower;
    //    if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.right), out hitLower, 0.1f))
    //    {
    //        Debug.DrawLine(stepRayLower.transform.position, hitLower.point, Color.red);
    //        Debug.Log(hitLower.point);
    //        RaycastHit hitUpper;
    //        if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.right), out hitUpper, 0.2f))
    //        {
    //            rb.position += new Vector3(0f, stepSmooth, 0f);
    //        }
    //    }
    //}
}