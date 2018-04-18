using UnityEngine;
using System.Collections;

public class TapController : MonoBehaviour
{
    public float upForce;                   //Upward force of the "flap".
    private bool isDead = false;            //Has the player collided with a wall?
    
    public Rigidbody2D rb2d;               //Holds a reference to the Rigidbody2D component of the bird.
    public float tiltSmooth = 5;
    Quaternion downRotation;
    Quaternion forwardRotation;

    void Start()
    {
        //Get and store a reference to the Rigidbody2D attached to this GameObject.
        rb2d = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -55);
        forwardRotation = Quaternion.Euler(0, 0, 55);
    }

    void Update()
    {
        //Don't allow control if the bird has died.
        if (isDead == false)
        {
            //Look for input to trigger a "flap".
            if (Input.GetMouseButtonDown(0))
            {
                rb2d.velocity = Vector2.zero;
                transform.rotation = forwardRotation;
                //  new Vector2(rb2d.velocity.x, 0);
                //..giving the bird some upward force.
                rb2d.AddForce(new Vector2(0, upForce));
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ScoreZone")
        {
            GameControl.instance.BirdScored();

            //OnPlayerScored();
            //scoreSound.Play();
        }
        if (col.gameObject.tag == "DeadZone")
        {
            //rb2d.isKinematic = true;
            rb2d.simulated = false;
            rb2d.velocity = Vector2.zero;
            GameControl.instance.BirdDied();
            //OnPlayerDied();
            //dieSound.Play();
        }

    }


}