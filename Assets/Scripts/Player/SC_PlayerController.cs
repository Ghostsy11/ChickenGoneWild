using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class SC_PlayerController : MonoBehaviour
{
    //public PlayerInputActions playerInput;
    [SerializeField] 
    private InputActionReference move, jump, shoot;
    private Rigidbody rb;


    private enum PlayerState {grounded, jumping, double_jumping, hanging};
    private PlayerState playerState = PlayerState.double_jumping;

    [Tooltip("Radius in which player checks for climable surface")]
    [SerializeField] private int searchRadius;
    [SerializeField] private LayerMask climableMask;
    [SerializeField] private float checkDistance;

    [SerializeField] private LayerMask groundMask;


    [SerializeField] private float maxSpeed = 5;
    private float speed = 0;
    [SerializeField] private int speedRampUpMultiplier = 1;
    private bool moving = false;
    [SerializeField]private float steps = 1000;
    [SerializeField]private float currentStep;

    // Start is called before the first frame update
    void Start()
    {
        //playerInput = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        CheckIfGrouned();
        CheckIfNearClimable();
        if (moving)
        {
            MoveHorizontal(move.action.ReadValue<Vector2>().x);
            MoveVertical(move.action.ReadValue<Vector2>().y);
        }
        else if(speed > 0 || speed < 0){
            currentStep--;
            if (currentStep < 0)
            {
                currentStep = 0;
            }
        }
    }


    private void CheckIfGrouned()
    {

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
            if(playerState == PlayerState.double_jumping) return;
            if(playerState == PlayerState.grounded)
            {
                playerState = PlayerState.jumping;
                Jump();
            }
            else if (playerState == PlayerState.jumping)
            {
                playerState = PlayerState.double_jumping;
                Jump();
            }
            else if(playerState == PlayerState.hanging)
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
        //Debug.Log(h);
        speed = speedCalculation((int)h);
        rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
        Debug.Log(speed);
        //Debug.Log(speed);
        //CheckForRamp();
    }
    private void MoveVertical(float y)
    {
        
    }
    public void AttackAction(InputAction.CallbackContext context)
    {

    }
    private float speedCalculation(int dir)
    {
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
}