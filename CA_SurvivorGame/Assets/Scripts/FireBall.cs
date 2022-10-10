using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float Speed;
    GameObject _Player;
    GameObject _Enemy;
    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _Enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        if(_Enemy != null)
        _Enemy.GetComponent<EnemyController>().EnemyTakeDamage(20);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_Player.transform.position, transform.position) > 20)
        {
            Destroy(gameObject);
        }
        //transform.position += transform.forward * Speed * Time.deltaTime;
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
}
