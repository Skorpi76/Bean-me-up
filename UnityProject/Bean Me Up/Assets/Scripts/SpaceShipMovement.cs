using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float maxVelocity = 3;
    public float rotationSpeed = 3;
    GameObject engine;


    public GameObject playerPrefab;
    //[HideInInspector]
    //public GameObject playerInstance;

    [HideInInspector]
    public bool CanLand = false;
    [HideInInspector]
    public Vector3 PlanetCore;

    ShipState shipState = ShipState.NotLanding;
    public PlayerState playerState = PlayerState.Ship;

    Coroutine landingCoroutine;  
    public LayerMask PlanetLayer;

    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        engine = GameObject.FindGameObjectWithTag("Engine");
        engine.SetActive(false);

    }
    private void Update()
    {
        if (playerState == PlayerState.Ship)
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



            if (Input.GetKeyDown("f"))
            {


                if (shipState == ShipState.NotLanding) //land ship
                {
					print (PlanetCore);
                    Vector3 dir = PlanetCore - transform.position;
					RaycastHit2D hit = Physics2D.Raycast (transform.position, dir, 20, PlanetLayer);
					print (hit.collider.name);
                    if (hit.collider.tag == "PlanetLand")
                    {
                        shipState = ShipState.Landing;
                        landingCoroutine = StartCoroutine(LandShip(hit.point + (hit.normal * 1), hit.normal, hit.transform.position));
                    }
                }
                else if (shipState == ShipState.Landing) //cancel landing
                {
                    StopCoroutine(landingCoroutine);
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    shipState = ShipState.NotLanding;
                }



            }
        }


    }

    public void StartTakeOff() {
        playerState = PlayerState.Ship;
       // Destroy(playerInstance);
        rb.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(TakeOff());
    }

    IEnumerator TakeOff() {
        playerState = PlayerState.Ship;
        Camera.main.GetComponent<CameraFollowSpaceShip>().followPlayer = false;
        float t = 5;
        while (t > 0) {
            print("taking off");
            t = t - 5 * Time.deltaTime;
            rb.AddForce(transform.up * 250 * Time.deltaTime);
            yield return null;
        }
        shipState = ShipState.NotLanding;
		GetComponent<SpaceShoot> ().enabled = true;


    }

    IEnumerator LandShip(Vector3 landingSpot, Vector2 landingDirection, Vector3 core)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        float startDistance = Vector3.Distance(transform.position, landingSpot);

        while (transform.position != landingSpot)
        {
            transform.position = Vector3.MoveTowards(transform.position, landingSpot, 5 * Time.deltaTime);

            float transition = Mathf.Clamp(Vector3.Distance(transform.position, landingSpot) / startDistance, 0, 1);
            transform.rotation = Quaternion.Lerp(Quaternion.FromToRotation(Vector3.up, landingDirection), transform.rotation, transition);
            rb.velocity = new Vector3(0, 0, 0);
            yield return null;
        }
        shipState = ShipState.Landed;
        GameObject player = Instantiate(playerPrefab, transform.position + transform.right ,transform.rotation) as GameObject;
        player.GetComponent<BeanBoyMovement>().planetCore = core;
        playerState = PlayerState.Bean;

		GetComponent<SpaceShoot> ().enabled = false;

        //playerInstance = player;
        Camera.main.GetComponent<CameraFollowSpaceShip>().followPlayer = true;
		 
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
		rb.velocity = transform.up * rb.velocity.magnitude;
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

public enum ShipState {NotLanding, Landing, Landed}

public enum PlayerState { Ship,Bean }
