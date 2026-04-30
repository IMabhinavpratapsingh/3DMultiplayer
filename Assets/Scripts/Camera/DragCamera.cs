using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Mirror;
public class DragCamera : NetworkBehaviour
{
    [SerializeField] public Transform cameraa;
    [SerializeField] float sensitivity = 0.2f;
    [SerializeField] float xRotation;
    [SerializeField] float yRotation;
    [SerializeField] public OrbitCamera orbitCam;
    [SerializeField] Transform pivotY;

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }
    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Start()
    {
        if(!isLocalPlayer)
        {
            enabled = false;
            return;
        }
           

        orbitCam = FindAnyObjectByType<OrbitCamera>(FindObjectsInactive.Include);
        orbitCam.target = gameObject.transform;
    }

    void Update()
    {
        RotateCamera();
    }
    public void RotateCamera()
    {
        var touches = Touch.activeTouches;
        if (touches.Count > 0)
        {
            var fingers = Touch.activeFingers;
            if(fingers.Count == 1)
            {
                var touch = touches[0];

                Vector2 pos = touch.screenPosition;
                Vector2 delta = touch.delta;

                if (pos.x > Screen.width / 2)
                {   
                    
                    yRotation += delta.x * sensitivity;
                    xRotation -= delta.y * sensitivity;
                    

                    xRotation = Mathf.Clamp(xRotation,-60,40);
                    if (orbitCam)
                    {
                        orbitCam.orbitrotation = Quaternion.Euler(xRotation,yRotation,0);
                    }           
                }
            }
        }
        
    }


}
