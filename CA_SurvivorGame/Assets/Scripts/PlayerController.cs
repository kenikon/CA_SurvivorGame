using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    bool Alive = true;
    [SerializeField] float Health = 100f;
    GameObject Body;
    [SerializeField] Color BodyColor;
    GameObject Eye;
    [SerializeField] Color EyeColor;
    GameObject LeftArm;
    [SerializeField] Color LeftArmColor;
    GameObject RightArm;
    [SerializeField] Color RightArmColor;
    GameObject _Enemy;

    public float MoveSpeed = 5;
    private Rigidbody rb;
    private Vector3 MoveInput;
    private Vector3 MoveVelocity;

    private Camera _MainCamera;
    private Plane GroundPlane;

    [SerializeField] GameObject Projectile;
    [SerializeField] Transform ProjectileTransform;

    void Start()
    {
        Body = gameObject;
        ColorizedBodyParts(Body, BodyColor);
        Eye = gameObject.transform.GetChild(0).gameObject;
        ColorizedBodyParts(Eye, EyeColor);
        LeftArm = gameObject.transform.GetChild(1).gameObject;
        ColorizedBodyParts(LeftArm, LeftArmColor);
        RightArm = gameObject.transform.GetChild(2).gameObject;
        ColorizedBodyParts(RightArm, RightArmColor);

        rb = GetComponent<Rigidbody>();
        _MainCamera = FindObjectOfType<Camera>();
        GroundPlane = new Plane(Vector3.up, Vector3.zero);

        _Enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    //private void OnMouseDown()
    //{
    //    //Instantiate(Projectile, ProjectileTransform.position, Quaternion.Euler(0, 0, 0));
    //    Instantiate(Projectile, transform.position, transform.rotation);
    //}

    void ColorizedBodyParts(GameObject go, Color c)
    {
        go.GetComponent<Renderer>().material.color = c;
    }

    public void PlayerTakeDamage()
    {
        Health -= 10f;
    }

    public void Die()
    {
        Alive = false;
        //Destroy(gameObject);
        gameObject.SetActive(false);
        // Restart the game
        Invoke(nameof(Restart), 0.5f);
    }

    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void FixedUpdate()
    {
        if (!Alive) return;

        rb.velocity = MoveVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        MoveVelocity = MoveInput * MoveSpeed;

        Ray CameraRay = _MainCamera.ScreenPointToRay(Input.mousePosition);
        // Plane GroundPlane = new Plane(Vector3.up, Vector3.zero);

        if (GroundPlane.Raycast(CameraRay, out float RayLength))
        {
            Vector3 PointToLook = CameraRay.GetPoint(RayLength);
            Debug.DrawLine(CameraRay.origin, PointToLook, Color.magenta);

            transform.LookAt(new Vector3(PointToLook.x, transform.position.y, PointToLook.z));
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(Projectile, transform.position, transform.rotation);
        }
        //if(_Enemy)
        //{
        //    if (Vector3.Distance(transform.position, _Enemy.transform.position) <= 15)
        //    {
        //        transform.LookAt(_Enemy.transform.position);
        //    }
        //}

        if (transform.position.y < -2 || Health <= 0)
        {
            Die();
        }
    }
}
