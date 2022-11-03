using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    private Vector3 targetRotation, targetPosition, currentRotation, currentPosition, initialPosition;
    public float recoilX;
    public float recoilY;
    public float recoilZ;
    public float kickZ;
    public float snappiness;
    public float returnAmount;
    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
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
        print(targetRotation + " "+ targetPosition);
    }
    void back()
    {
        targetPosition = Vector3.Lerp(targetPosition, initialPosition, returnAmount * Time.deltaTime);
        currentPosition = Vector3.Lerp(currentPosition, targetPosition, snappiness * Time.deltaTime);
        transform.localPosition = currentPosition;
    }
}
