using UnityEngine;
public class OrbitCamera : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public  Quaternion Orbitrotation;
    [SerializeField] public Quaternion orbitrotation = Quaternion.identity;

    Vector3 offset = new Vector3(0,1.10f,-2.67f);

    // void Start()
    // {
    //     target = FindAnyObjectByType<PlayerMovement>(FindObjectsInactive.Include).Get_Pivot();
    // }
    void Update()
    {
        if (!target) return;
        
        transform.position = target.position + orbitrotation * offset;
        transform.LookAt(target);

    }
}
