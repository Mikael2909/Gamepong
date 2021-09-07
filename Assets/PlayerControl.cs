using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode upButton = KeyCode.W;// Tombol untuk menggerakkan ke atas
    public KeyCode downButton = KeyCode.S; // Tombol untuk menggerakkan ke bawah
    public float speed = 10.0f;//kecepatan gerak
    public float yBoundary = 9.0f;//batas atas dan batas bawah game scene
    private Rigidbody2D rigidBody2D;//Rigidbody2d raket
    private int score;//skor pemain
    private ContactPoint2D lastContactPoint;
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("bola"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }
    void Start()
    {
        rigidBody2D = GetComponent < Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rigidBody2D.velocity;//dapatkan kecepatan raket
        if (Input.GetKey(upButton))
        {
            velocity.y = speed;//jika menekan tombol w
        }
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }
        else
        {
            velocity.y = 0.00f;
        }
        rigidBody2D.velocity = velocity;//masukkan kembali kecepatan
        Vector3 position = transform.position;//mendapatkan posisi raket
        if (position.y > yBoundary)
        {
            position.y = yBoundary;//jika posisi raket melewati batas atas maka kembalikan ke batas atas
        }
        else if (position.y < -yBoundary)
        {
            position.y = -yBoundary;//jika posisi raket melewati batas bawah, maka kembalikan ke batas bawah
        }
        transform.position = position;//kembalikan posisi
    }
    public void IncrementScore()
    {
        score++;
    }
    public void ResetScore()
    {
        score = 0;
    }
    public int Score
    {
        get { return score; }
    }
}
