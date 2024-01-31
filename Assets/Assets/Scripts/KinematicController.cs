using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicController : MonoBehaviour, IDamagable
{
    [SerializeField, Range(0, 40)] float speed = 1f;

    public float health = 100;

    public void ApplyDamage(float damage)
    {
        health -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        Vector3 force = direction * speed * Time.deltaTime;
        transform.localPosition += force;

        //transform.localPosition

    }
}

