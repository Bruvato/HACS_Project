using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    private Vector3 targetRotation, targetPosition, currentRotation, currentPosition, initialPosition, initialPosition2, aimTarget, aimTarget2, currentAim, currentAim2;
    private Vector3 originalPosition, originalAim, currentReset;
    public float recoilX;
    public float recoilY;
    public float recoilZ;
    public float kickZ;
    public float snappiness;
    public float returnAmount;
    public float aimSpeed;

    public Transform cam;
    public Transform aimLocation;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
        initialPosition2 = transform.localPosition;
        originalPosition = transform.localPosition;

        // initialPosition = AimLocation.localPosition;
        aimTarget = new Vector3(
            aimLocation.localPosition.x,
            aimLocation.localPosition.y,
            aimLocation.localPosition.z);
        aimTarget2 = new Vector3(
            aimLocation.localPosition.x,
            aimLocation.localPosition.y,
            aimLocation.localPosition.z);
        originalAim = new Vector3(
            aimLocation.localPosition.x,
            aimLocation.localPosition.y,
            aimLocation.localPosition.z);
        currentReset = currentAim;
    }

    
    // Update is called once per frame
    void Update()
    {   
        if(Input.GetButton("Fire2")){

            aimTarget = Vector3.Lerp(aimTarget, initialPosition, aimSpeed * Time.deltaTime);
            currentAim = Vector3.Lerp(currentAim, aimTarget, aimSpeed * Time.deltaTime);
            initialPosition = currentAim;
            
        }else{
            aimTarget2 = Vector3.Lerp(aimTarget2, initialPosition2, aimSpeed * Time.deltaTime);
            currentAim2 = Vector3.Lerp(currentAim2, aimTarget2, aimSpeed * Time.deltaTime);
            initialPosition = currentAim2;
            // if(currentAim == aimTarget2){}
            // reset aiming variables
            aimTarget = originalAim;
            currentAim = currentReset;

        }
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnAmount * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);
        transform.Rotate(currentRotation);
        cam.localRotation = Quaternion.Euler(currentRotation);

        back();//kick

    }
    public void recoil()
    {
        targetPosition -= new Vector3(0,0,kickZ);
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
    void back()
    {
        targetPosition = Vector3.Lerp(targetPosition, initialPosition, returnAmount * Time.deltaTime);
        currentPosition = Vector3.Lerp(currentPosition, targetPosition, snappiness * Time.deltaTime);
        transform.localPosition = currentPosition;
    }
}
