using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] Vector3 Offset;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(0, Player.position.y + Offset.y, Player.position.z + Offset.z);
        if (Player != null)
        {
            transform.position = Player.position + Offset;
        }

    }
}
