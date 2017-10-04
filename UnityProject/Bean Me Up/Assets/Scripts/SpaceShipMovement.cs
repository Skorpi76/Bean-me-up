using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour {
    private Rigidbody2D rb;
    public float maxVelocity = 3;
    public float rotationSpeed = 3;
    GameObject engine;

    bool isLanded = false;
    public bool CanLand = false;
    public bool isLanding = false;
    public Vector3 PlanetCore;
    public LayerMask PlanetLayer;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        engine = GameObject.FindGameObjectWithTag("Engine");
        engine.SetActive(false);
    }
    private void Update()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");
        if (yAxis > 0)
        {
            engine.SetActive(true);
        }
        else
        {
            engine.SetActive(false);
        }
        ThrustForward(yAxis);
        Rotate(transform, xAxis * -rotationSpeed);


        if (Input.GetKeyDown("f") && CanLand) {


            

            Vector3 dir = PlanetCore - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 20, PlanetLayer);
            if (hit.collider != null)
            {
                print("land ship");
                isLanding = true;
                StartCoroutine(LandShip(hit.point, dir));
            }
            else {
                print("Don't Land");
            }
            
        }


    }

    IEnumerator LandShip(Vector3 landingSpot, Vector3 landingDirection) {
        rb.bodyType = RigidbodyType2D.Kinematic;
        float targetZRotation = Vector3.Angle(transform.up, -1 *landingDirection);
        print(targetZRotation);
        while (transform.position != landingSpot && transform.rotation.z != targetZRotation) {
            transform.position = Vector3.MoveTowards(transform.position, landingSpot, 5 * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, targetZRotation), 5 * Time.deltaTime);
            rb.velocity = new Vector3(0, 0, 0);
            yield return null;
        }
        isLanding = false;
        isLanded = true;
    }


    /// <summary>
    ///  This funtion will stop us of speeding up at some point
    /// </summary>
    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);

        rb.velocity = new Vector2(x, y);
    }

    private void ThrustForward(float amount)
    {
        Vector2 force = transform.up * amount;    
        rb.AddForce(force); // will keep adding force as we keep the button on
    }


    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, 0, amount);
    }





    // Suuuuucks More of a shooter movement, will keep it here maybe you will like it i donno :-) 
    //private int speed;
    //private float angle;
    //private Vector3 input;
    //private Rigidbody2D rb;
    //private Camera playerCamera;
    //private Vector2 playerToMouse;

    //// ===================================
    //void Start()
    //{
    //    speed = 5;
    //    rb = GetComponent<Rigidbody2D>();
    //    input = Vector3.zero;
    //    playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    //}

    //// ===================================
    //void Update()
    //{
    //    input.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
    //    playerToMouse = transform.position - playerCamera.ScreenToWorldPoint(Input.mousePosition);
    //    angle = (Mathf.Atan2(playerToMouse.y, playerToMouse.x) * Mathf.Rad2Deg) + 90;
    //    transform.rotation = Quaternion.Euler(0, 0, angle);
    //    float inputLength = Vector2.Distance(Vector2.zero, new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));        
    //}

    //// ===================================
    //void FixedUpdate()
    //{
    //    rb.velocity = input * speed;
    //}
}
