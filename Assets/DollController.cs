using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollController : MonoBehaviour
{
    public float minTime, maxTime;

    public bool isGreenLight = true;

    public Animator animator;

    public readonly string greenLightAnim = "GreenLight";

    public GameObject bulletPrefab;
    public Transform shotPoint;
    bool hasShoot;

    public AudioClip redLightSfx, greenLightSfx, shootSfx;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        StartCoroutine(ChangeLightCoroutine());
    }

    IEnumerator ChangeLightCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        if (isGreenLight)
        {
            
            animator.SetBool(greenLightAnim, false);
            audioSource.PlayOneShot(redLightSfx);
            yield return new WaitForSeconds(0.7f);
            isGreenLight = false;

            print("LAMPU MERAH, PLAYER TIDAK BOLEH JALAN!!");
        }

        else
        {
            isGreenLight = true;
            audioSource.PlayOneShot(greenLightSfx);
            animator.SetBool(greenLightAnim, true);

            print("LAMPU HIJAU, SILAHKAN JALAN");
        }

        StartCoroutine(ChangeLightCoroutine());
    }

    public void ShootPlayer(Transform playerTarget)
    {
        

        if (hasShoot) return;
        GameObject bulletGo = Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);
        audioSource.PlayOneShot(shootSfx);
        bulletGo.GetComponent<BulletMove>().Player = playerTarget;

        hasShoot = true;
    }
    

}
