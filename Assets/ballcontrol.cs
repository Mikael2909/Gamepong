using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballcontrol : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;//rigidbody 2d bola
    public float XinitialForce;//gaya awal mendorong bola dalam x
    public float YinitialForce;//gaya awal mendorong bola dalam y
    private Vector2 trajectoryOrigin;
    void ResetBall()
    {
        transform.position = Vector2.zero;//ubah posisi menjadi 0,0
        rigidBody2D.velocity = Vector2.zero;//ubah kecepatan menjadi 0,0
    }
    void PushBall()
    {
        
        float RandomDirection = Random.Range(0, 2);//menentukan nilai acak
        if (RandomDirection < 1.0f)
        {
            rigidBody2D.AddForce(new Vector2(-XinitialForce, YinitialForce));
        }
        else
        {
            rigidBody2D.AddForce(new Vector2(XinitialForce, YinitialForce));
        }
    }
    void RestartGame()
    {
        ResetBall();//memanggil fungsi reset
        Invoke("PushBall", 2);//memanggil fungsi pushball setelah 2 detik
    }
    
    // Start is called before the first frame update
    void Start()
    {
        trajectoryOrigin = transform.position;
        rigidBody2D = GetComponent<Rigidbody2D>();
        RestartGame();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
