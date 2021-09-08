using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Rigidbody 2D bola
    private Rigidbody2D rigidBody2D;
    // Besarnya gaya awal yang diberikan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;
    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;
    
    private int starting = 0;
    Vector2 keeper; //mengingat velonvity awal
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        // Mulai game
        trajectoryOrigin = transform.position;
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetBall()
    {
        // Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;

        // Reset kecepatan menjadi (0,0)
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        Vector2 newDirect;

        //starting force
        if (starting <= 0) { 
            // float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
            // Tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
            float randomDirection = Random.Range(0, 2);

            // -1 = kiri 
            if (randomDirection < 1.0f)
                newDirect = new Vector2(-xInitialForce, yInitialForce);
            else
                newDirect = new Vector2(xInitialForce, yInitialForce);

            //simpen nilai
            keeper = newDirect; starting++;
        }
        //counter
        else
        {
            float yDirection = Random.Range(-yInitialForce, yInitialForce);
            float xDirection = Random.Range(-xInitialForce, xInitialForce);
            Vector2 tmpDirect = new Vector2(xDirection, -yInitialForce); // buat arah baru
            Vector2 newVel = keeper.magnitude * tmpDirect.normalized; //kecepatan awal dengan arah baru
            newDirect = new Vector2(newVel.x,newVel.y);
        }

        rigidBody2D.AddForce(newDirect);
    }   
    void RestartGame()
    {
        // Kembalikan bola ke posisi semula
        ResetBall();
        // Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 2);
    }
    // Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }
    // Untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}