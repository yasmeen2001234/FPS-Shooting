using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class Recoil : MonoBehaviour
{

    //public GameObject Weapon;
    //determine the maximum amount of rotation the weapon can experience in the X and Y axes when shooting/recoiling
    public float maxRecoil_x = 0.0f;
    public float maxRecoil_y = -0.05f;

    // These variables determine the maximum translation(movement) the weapon can experience in the X and Z axes when shooting/recoiling.
    public float maxTrans_x = 0f;
    public float maxTrans_z = -0.05f;

    public float recoilSpeed = 10.0f; // how quickly the weapon returns to its original position after recoiling.

    public float recoil = 0.0f; // recoil level

    void Update()
    {
        if (recoil > 0)
        {
            // it calculates a random rotation within the specified limits 

            var maxRecoil = Quaternion.Euler(
                Random.Range(transform.localRotation.x, maxRecoil_x),
                Random.Range(transform.localRotation.y, maxRecoil_y),
                transform.localRotation.z);

            // Dampen towards the target rotation
            // Smoothly rotates between the current rotation and maxRecoil
            transform.localRotation = Quaternion.Slerp(transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeed);

            //it calculates a random translation within the specified limits 
            var maxTranslation = new Vector3(
                Random.Range(transform.localPosition.x, maxTrans_x),
                transform.localPosition.y,
                Random.Range(transform.localPosition.z, maxTrans_z));

            // and use Vector3.Slerp() gradually move the weapon between the current position and maxTranslation
            transform.localPosition = Vector3.Slerp(transform.localPosition, maxTranslation, Time.deltaTime * recoilSpeed);

            recoil -= Time.deltaTime; //recoil is decreased over time 
        }
        else
        {
            recoil = 0; //If recoil reaches 0, it means the recoil effect is done/ user stops shooting

            //It then uses Quaternion.Slerp() and Vector3.Slerp() to
            //gradually return the weapon to its initial position and rotation but at a slower rate (recoilSpeed / 2).

            var minRecoil = Quaternion.Euler(
                Random.Range(0, transform.localRotation.x),
                Random.Range(0, transform.localRotation.y),
                transform.localRotation.z);

            // Dampen towards the target rotation
            transform.localRotation = Quaternion.Slerp(transform.localRotation, minRecoil, Time.deltaTime * recoilSpeed / 2);


            var minTranslation = new Vector3(
            Random.Range(0, transform.localPosition.x),
                transform.localPosition.y,
                Random.Range(0, transform.localPosition.z));

            transform.localPosition = Vector3.Slerp(transform.localPosition, minTranslation, Time.deltaTime * recoilSpeed);
        }
    }
}