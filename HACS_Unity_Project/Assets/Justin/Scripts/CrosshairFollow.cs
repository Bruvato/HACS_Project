using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairFollow : MonoBehaviour
{
    public LayerMask targets;
    private Transform bulletEmitter;
    
    public Camera cam;
    private Vector3 source;
    private Ray bulletPath;
    [HideInInspector]public RaycastHit hit;
    public float range;
    private Vector3 screenPos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        source = new Vector3(bulletEmitter.position.x, bulletEmitter.position.y, bulletEmitter.position.z);
        bulletPath = new Ray(source, bulletEmitter.forward);
        if(Physics.Raycast(bulletPath, out hit, range, targets))
        {
            screenPos = cam.WorldToScreenPoint(hit.point);
        }
        else
        {
            screenPos = cam.WorldToScreenPoint(bulletPath.GetPoint(range));
        }
        transform.position = screenPos;

    }
}
