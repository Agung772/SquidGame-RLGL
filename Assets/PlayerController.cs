using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public CameraFollow cameraFollow;
    public DollController dollController;
    public GameObject playerBody, playerRadDoll;
    public Transform radDollHips;
    public ParticleSystem efekDarah;

    public float moveSpeed, heading;
    public bool isDead, hasWon;

    [Space]

    public float moveX;
    public float moveZ;

    [Space]

    public float moveAnim;

    public Animator animator;
    public readonly string moveAnimParameter = "Move";

    AudioSource audioSource;
    public AudioClip gotSfx;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (isDead) return;

        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        
        Vector3 move = new Vector3(moveX, 0, moveZ);
        characterController.Move(move * moveSpeed * Time.deltaTime);

        if (!hasWon)
        {
            if (moveX != 0 || moveZ != 0)
            {
                if (!dollController.isGreenLight)
                {
                    dollController.ShootPlayer(transform);
                    print("Kamu ditembak Mati!!");

                }
            }
        }


        moveAnim = new Vector2(moveX, moveZ).magnitude;
        animator.SetFloat(moveAnimParameter, moveAnim);


        heading = Mathf.Atan2(moveX, moveZ);       
        if (moveX == 0 && moveZ == 0) return;
        transform.rotation = Quaternion.Euler(0, heading * Mathf.Rad2Deg, 0); 
    }

    public void Dead()
    {
        audioSource.PlayOneShot(gotSfx);
        efekDarah.Play();
        cameraFollow.playerTarget = radDollHips;
        playerBody.SetActive(false);
        playerRadDoll.SetActive(true);
        isDead = true;
        print("Player Mati");
        StartCoroutine(RestratGameCoroutine());
    }

    IEnumerator RestratGameCoroutine()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FinisLine>())
        {
            hasWon = true;
        }
    }
}
