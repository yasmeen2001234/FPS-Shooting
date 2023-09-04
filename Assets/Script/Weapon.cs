using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;

    [SerializeField] float range = 100f;


    [SerializeField] GameObject hitEffect;
 
    [SerializeField] float FireRate = 0f;

 
    [SerializeField] AudioSource audioSource;


    public List<AudioClip> audioClipsList = new List<AudioClip>();

    [SerializeField] Recoil RecoilObject;

    bool canShoot = true;

    

    private void Update()
    {
        if (Input.GetMouseButton(0) && canShoot )
        {
            StartCoroutine(Shoot());

            if (RecoilObject.recoil <= 0.2f)
                RecoilObject.recoil += 0.1f;
            else RecoilObject.recoil = 0;
        }

      
    }


    private IEnumerator Shoot()
    {
              canShoot = false;

             ShootingRaycast();
        
            PlayAudioClipAtIndex(0); // play gunshot sound
        
        yield return new WaitForSeconds(FireRate); // wait for time to enable shooting
        canShoot = true;
    }




    private void ShootingRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            HitImpact(hit);
        }
    }

    // creating a visual impact effect at the point where a raycast hit an object
    private void HitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);

    }

    private void PlayAudioClipAtIndex(int index)
    {
        if (index >= 0 && index < audioClipsList.Count)
        {
            audioSource.PlayOneShot(audioClipsList[index]);
        }
    }



}
