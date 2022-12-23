using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    public Transform Player;
    public float speedBullet;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.position, speedBullet * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().Dead();
            Destroy(gameObject);
        }
    }
}
