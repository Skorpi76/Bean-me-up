using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanBoyMovement : MonoBehaviour
{

    Rigidbody2D rb;
    public float walkSpeed = 5;
    bool jumping = false;
    public GameObject spriteObject;

    public LayerMask playerWalk;
    public LayerMask shipLayerMask;

    [HideInInspector]
    public Vector3 planetCore;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            print("f pressed");
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 1, transform.forward, 10, shipLayerMask);
            if (hit.collider.tag == "Player")
            {
                
                hit.collider.GetComponent<SpaceShipMovement>().StartTakeOff();
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (Quaternion.FromToRotation(Vector3.down, (new Vector3(planetCore.x, planetCore.y, 0) - new Vector3(transform.position.x, transform.position.y, 0)).normalized) == Quaternion.FromToRotation(Vector3.down, Vector3.up))
        {
            //print("Dont flip out bean boy");
        }
        else
        {
            transform.rotation = Quaternion.Lerp(Quaternion.FromToRotation(Vector3.down, (new Vector3(planetCore.x, planetCore.y, 0) - new Vector3(transform.position.x, transform.position.y, 0)).normalized), transform.rotation, Time.fixedDeltaTime);
        }



        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up * -1, 0.1f, playerWalk);
        rb.velocity = transform.right * Input.GetAxis("Horizontal") * 10;

        if (Input.GetKeyDown("space") && hit.collider != null)
        {

            StartCoroutine(Jump());
        }

        Gravity();
    }

    void Gravity()
    {

        rb.AddForce((new Vector3(planetCore.x, planetCore.y, 0) - new Vector3(transform.position.x, transform.position.y, 0)).normalized * 90.8f);

    }

    IEnumerator Jump()
    {
        float t = 1;
        while (t > 0)
        {
            rb.AddForce((new Vector3(planetCore.x, planetCore.y, 0) - new Vector3(transform.position.x, transform.position.y, 0)).normalized * -(400.0f * t));
            t -= 0.05f;
            yield return null;
        }
    }
}
