using UnityEngine;
using Mirror;




public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] Transform pivotY;


    // Vector2 output;
    Joystick joystick;
    OrbitCamera cam;
    Vector2 lastInput;

    void Start()
    {
        

        if(!isLocalPlayer) return;

        joystick = Joystick.instance;
    }
    public override void OnStartServer()
    {
        rb.freezeRotation = true;
    }
    public override void OnStartLocalPlayer()
    {
        cam = FindAnyObjectByType<OrbitCamera>(FindObjectsInactive.Include);
        
    }

    
    void FixedUpdate()
    {
        if(!isLocalPlayer) return;

        if (joystick.output.magnitude > 0.1)
        {
            Vector3 forward = cam.transform.forward;
            Vector3 right = cam.transform.right;

            forward.y = 0;
            right.y = 0;

            Vector3 move = joystick.output.y * forward + joystick.output.x * right;
            
            if(Vector2.Distance(lastInput,joystick.output)> 0.01f)
            {
                CmdMove(move);
                lastInput = joystick.output;
            }
            
            
        }

    }

    public Transform Get_Pivot()
    {
        if(isLocalPlayer)
        {
            return pivotY;
        }
        return null;
    }
    [Command]
    void CmdMove(Vector3 move)
    {
        MoveOnServer(move);
    }

    [Server]
    void MoveOnServer(Vector3 move)
    {
        if (move.sqrMagnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,rotationSpeed* Time.deltaTime);

            rb.linearVelocity = move * speed;    
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}
