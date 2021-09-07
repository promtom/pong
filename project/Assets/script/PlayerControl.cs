using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //control
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;

    //batasan
    public float speed = 10.0f;
    public float yBoundary = 9.0f;
    private int score;

    //2D raket
    private Rigidbody2D rigidBody2D;
    
    // Titik tumbukan terakhir dengan bola, untuk menampilkan variabel-variabel fisika terkait tumbukan tersebut
    private ContactPoint2D lastContactPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Dapatkan kecepatan raket sekarang.
        Vector2 velocity = rigidBody2D.velocity;
        if (Input.GetKey(upButton)) velocity.y = speed; //atas
        else if (Input.GetKey(downButton)) velocity.y = -speed; //kebawah
        else velocity.y = 0.0f; //idle
        // Masukkan kembali kecepatannya ke rigidBody2D.
        rigidBody2D.velocity = velocity;
        
        // Dapatkan posisi raket sekarang.
        Vector3 position = transform.position;  
        if (position.y > yBoundary) position.y = yBoundary; //batas atas
        else if (position.y < -yBoundary) position.y = -yBoundary; //batas bawah
        // Masukkan kembali posisinya ke transform.
        transform.position = position;
    }

    // Menaikkan skor sebanyak 1 poin
    public void IncrementScore()
    {
        score++;
    }

    // Mengembalikan skor menjadi 0
    public void ResetScore()
    {
        score = 0;
    }

    // Mendapatkan nilai skor
    public int Score
    {
        get { return score; }
    }

    // Untuk mengakses informasi titik kontak dari kelas lain
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    // Ketika terjadi tumbukan dengan bola, rekam titik kontaknya.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }

}
