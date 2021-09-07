using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public ballcontrol ball;
    CircleCollider2D ballCollider;
    Rigidbody2D ballRigidbody;

    public GameObject ballAtCollision;
    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody = ball.GetComponent<Rigidbody2D>();//dapatkan komponen rigidbody
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool drawBallAtCollision = false;
        Vector2 offsetHitPoint = new Vector2();//titik tumbukan digeser, untuk menggambar ball at collision
        RaycastHit2D[] circleCastHit2DArray = Physics2D.CircleCastAll(ballRigidbody.position, ballCollider.radius, ballRigidbody.velocity.normalized);

        //untuk setiap titik tumbukan
        foreach (RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            if(circleCastHit2D.collider != null &&
                circleCastHit2D.collider.GetComponent<ballcontrol>() == null)
            {
                Vector2 hitPoint = circleCastHit2D.point;//tentukan titik tumbukan
                Vector2 hitNormal = circleCastHit2D.normal;//tentukan normal di titik tumbukan
                offsetHitPoint = hitPoint + hitNormal * ballCollider.radius;//tentukan offset hitpoint, yaitu titik tengah bola saat bertumbukan
                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);

                if (circleCastHit2D.collider.GetComponent<Sidewall>() == null)//jika bukan sidewal, gambar pantulannya
                {
                    Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;//hitung vektor datang
                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);//hitung vektor keluar
                    float outDot = Vector2.Dot(outVector, hitNormal);//terjadi tumbukn tidak digambar
                    if(outDot>-1.0f && outDot < 1.0)
                    {
                        DottedLine.DottedLine.Instance.DrawDottedLine(offsetHitPoint, offsetHitPoint + outVector * 10.0f);//gambar lintasan pantulan
                        drawBallAtCollision = true;
                    }
                }
                break;
            }
        }
        if (drawBallAtCollision)
        {
            ballAtCollision.transform.position = offsetHitPoint;
            ballAtCollision.SetActive(true);//gambar bola di prediksi
        }
        else
        {
            ballAtCollision.SetActive(false);//sembunyikan
        }
    }

}
