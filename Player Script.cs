using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour
{
    public GameObject Bullet;   // Bullet to shoot

    public Rigidbody2D rb;      // Rigid body 2D

    public Transform Buster;    // Shoot bullet from

    public int Blast;       // Speed of bullet
    public int Speed;       // Speed of player
    public int jump;        // Jump of player

    public bool isGrounded;         // Checks if player is grounded
    public Transform groundCheck;   // Checks if player is grounded
    public float groundCheckRadius; // Checks if player is grounded
    public LayerMask isGroundLayer; // Checks if player is grounded

    bool isFired = false;       // Bool to check if bullet is being shot

    void Update()
    {
        // Movement:Left
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 Dir = new Vector3(Speed, 0, 0);
            transform.position += Dir * Time.deltaTime;
        }

        // Movement:Right
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 Dir = new Vector3(-Speed, 0, 0);
            transform.position += Dir * Time.deltaTime;
        }

        // Shooting
        if (Input.GetKey(KeyCode.K) && !isFired)
        {
            GameObject bullet = Instantiate(Bullet, Buster.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(Blast, 0.0f));
            Destroy(bullet, 2.5f);
            isFired = true;
            StartCoroutine(BulletTimer());
        }

        // Jump
        if (groundCheck)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        }

        if (isGrounded)
        {
            if ((Input.GetKey(KeyCode.Space)))
            {
                rb.AddForce(new Vector2(0, jump * Time.deltaTime));
                Debug.Log("HI");
            }
        }
    }

    // Shooting timer
    IEnumerator BulletTimer()
    {
        yield return new WaitForSeconds(0.5f);
        isFired = false;
        StopAllCoroutines();
    }
}