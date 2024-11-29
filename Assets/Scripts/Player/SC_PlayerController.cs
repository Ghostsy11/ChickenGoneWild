using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity;
public class SC_PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject playerModel;
    [SerializeField] private Collider baseCollider, jumpCollider, dodgeCollider;
    public PlayerInputActions playerInput;
    [Header("Controls")]
    public InputActionReference move, jump, attack;
    private enum PlayerState { grounded, jumping, double_jumping, climbing, dodging };
    [SerializeField] private PlayerState playerState = PlayerState.double_jumping;

    [Header("Climbing")]
    [Tooltip("Radius in which player checks for climable surface from the climbSearchPoint")]
    [SerializeField] private float searchRadius;
    [SerializeField] private Transform climbSearchPoint;
    [SerializeField] private LayerMask climableMask;


    [Header("Run")]
    [SerializeField] private float maxSpeed = 5;
    private float speed = 0;
    private bool moving = false;
    [SerializeField] private int steps = 1000;
    [SerializeField] private int currentStep;
    [SerializeField] private LayerMask groundMask;
    private int direction = 0;
    [Tooltip("-1 left, 0 no movement, 1 right")]
    private int xMove;
    [Tooltip("-1 down, 0 no movement, 1 up")]
    private int yMove;

    [Header("Ramp")]
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 2f;
    [SerializeField] float rampCheckDistance = 0.5f;
    private float surfaceAngle = 0;

    [Header("Jump")]
    [SerializeField] private float jumpSpeed = 10;
    [SerializeField] private float edgeJumpTime = 0.1f;
    private void Awake()
    {
        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
    }
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        baseCollider = GetComponent<Collider>();
        playerModel = transform.GetChild(0).gameObject;
    }
    private void FixedUpdate()
    {
        if (moving)
        {
            MoveHorizontal(xMove);
            MoveVertical(yMove);
            StepClimb();
        }
        else if (rb.velocity.x > 0 || rb.velocity.x < 0)
        {
            currentStep = CalculateCurrentStep();
            if (currentStep < 0)
            {
                currentStep = 0;
            }
        }
        else
        {
            currentStep = 0;
        }
        if (playerState == PlayerState.climbing)
        {
            Climb();
        }
        else
        {
            rb.useGravity = true;
        }
    }
    private void Climb()
    {
        Vector3 pos = CheckIfNearClimable();
        if (pos != Vector3.zero)
        {
            //set position of arm for ik on the point
            //for now place player beneath surface
            rb.position = new Vector3(rb.position.x, pos.y - baseCollider.bounds.size.y, rb.position.z);
        }
        else
        {
            playerState = PlayerState.jumping;
            rb.useGravity = true;
        }
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
    private Vector3 CheckIfNearClimable()
    {
        Collider[] hitColliders = Physics.OverlapSphere(climbSearchPoint.position, searchRadius, climableMask);
        if (hitColliders.Length > 0)
        {
            float dist = float.PositiveInfinity;
            int index = 0;
            for (int i = 0; i < hitColliders.Length; i++)
            {
                float d = Vector3.Distance(transform.position, hitColliders[i].transform.position);
                if (dist > d)
                {
                    dist = d;
                    index = i;
                }
            }
            return hitColliders[index].transform.position;
            
        }
        return Vector3.zero;
    }
    public void JumpAction(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping");
        if (context.performed)
        {          
            Vector3 climbPos = CheckIfNearClimable();
            if (playerState == PlayerState.grounded)
            {
                playerState = PlayerState.jumping;
                Jump();
            }
            else if (playerState == PlayerState.jumping || playerState == PlayerState.double_jumping)
            {
                if (climbPos != Vector3.zero)
                {
                    playerState = PlayerState.climbing;
                    rb.useGravity = false;
                }
                else if(playerState == PlayerState.jumping)
                {
                    playerState = PlayerState.double_jumping;
                    Jump();
                }
            }
            else if (playerState == PlayerState.climbing){
                playerState = PlayerState.jumping;
                rb.useGravity = true;
                ClimbJump(climbPos);
            }
        }
    }

    private void ClimbJump(Vector3 pos)
    {
        Vector3 dir = rb.transform.position - pos;
        dir = dir.normalized;
        rb.AddForce(-dir * jumpSpeed);
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
    }

    public void MoveAction(InputAction.CallbackContext context)
    {
        if (context.performed && move.action.enabled)
        {
            moving = true;
            xMove = (int)context.action.ReadValue<Vector2>().x;
            yMove = (int)context.action.ReadValue<Vector2>().y;
        }
        else if (context.canceled)
        {
            moving = false;
            xMove = 0;
            yMove = 0;
        }
    }
    private void MoveHorizontal(float h)
    {
        speed = SpeedCalculation((int)h);
        rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
    }
    private void MoveVertical(float y)
    {
        if (playerState == PlayerState.climbing)
        {
            //rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
        }
    }
    public void AttackAction(InputAction.CallbackContext context)
    {
        if (context.performed && attack.action.enabled)
        {

        }
    }
    private float SpeedCalculation(int dir)
    {
        if (dir != 0)
        {
            playerModel.transform.rotation = Quaternion.Euler(0, dir * 90, 0);
            if (dir > direction || dir < direction)
            {
                currentStep = 0;
            }
            direction = dir;
        }
        //https://solhsa.com/interpolation/ for the formula
        float X = speed;
        if (speed < maxSpeed || speed > -maxSpeed)
        {
            
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
    private void StepClimb()
    {
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.transform.position, playerModel.transform.TransformDirection(Vector3.forward), out hitLower, rampCheckDistance, groundMask))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, playerModel.transform.TransformDirection(Vector3.forward), out hitUpper, rampCheckDistance * 2, groundMask))
            {
                rb.position += new Vector3(0f, stepSmooth, 0f);
            }
        }
    }
}