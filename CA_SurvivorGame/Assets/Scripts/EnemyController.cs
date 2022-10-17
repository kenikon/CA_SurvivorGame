using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float Health = 10f;
    [Range(0,20)]
    [SerializeField] float Speed = 5f;
    GameObject Body;
    [SerializeField] Color BodyColor;
    GameObject Eye;
    [SerializeField] Color EyeColor;
    GameObject LeftArm;
    [SerializeField] Color LeftArmColor;
    GameObject RightArm;
    [SerializeField] Color RightArmColor;
    Rigidbody rb;
    GameObject _Player;

    void Start()
    {
        Body = gameObject;
        ColorizedBodyParts(Body, BodyColor);
        Eye = transform.GetChild(0).gameObject;
        ColorizedBodyParts(Eye, EyeColor);
        LeftArm = transform.GetChild(1).gameObject;
        ColorizedBodyParts(LeftArm, LeftArmColor);
        RightArm = transform.GetChild(2).gameObject;
        ColorizedBodyParts(RightArm, RightArmColor);

        _Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();

    }

    void ColorizedBodyParts(GameObject go, Color c)
    {
        go.transform.GetComponent<Renderer>().material.color = c;
    }

    public void EnemyTakeDamage(float Damage)
    {
        Health -= Damage;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Die();
        }
         transform.LookAt(_Player.transform.position);

         if (Vector3.Distance(transform.position, _Player.transform.position) <= 1.5f)
         {
             _Player.GetComponent<PlayerController>().PlayerTakeDamage();
        }

          transform.position += Speed * Time.deltaTime * transform.forward;
        //rb.AddForce(Vector3.forward * Speed * Time.deltaTime,ForceMode.Impulse);
    }
}
