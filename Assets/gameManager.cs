using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public PlayerControl player1;
    private Rigidbody2D player1Rigidbody;
    public PlayerControl player2;
    private Rigidbody2D player2Rigidbody;
    public ballcontrol Ball;
    private Rigidbody2D ballRigidbody;
    private CircleCollider2D ballCollider;
    public int maxScore;
    private bool isDebugWindowShown = false;
    public Trajectory trajectory;

    // Start is called before the first frame update
    private void Start()
    {
        player1Rigidbody = player1.GetComponent<Rigidbody2D>();
        player2Rigidbody = player2.GetComponent<Rigidbody2D>();
        ballRigidbody = Ball.GetComponent<Rigidbody2D>();
        ballCollider = Ball.GetComponent<CircleCollider2D>();
    }
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score); //tampilkan skor pemain 1 di kiri atas
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);//tampilkan skor pemin 2 di kanan atas  
        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))//tombol restart untuk mulai game dari awwal
        {
            player1.ResetScore();//ketika tombol ditekan, reset skor kedua pemain
            player2.ResetScore();
            Ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);//restart game
        }
        if (player1.Score == maxScore)//jika pemain1 mencapai skor max
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "Player One Wins");//tampilkan teks player one wins
            Ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        else if (player2.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "Player Two Wins");//tampilkan teks player two wins
            Ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);//kembalikan ke tengah 

        }
        if (isDebugWindowShown)
        {
            Color oldcolor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;

            float ballMass = ballRigidbody.mass;
            Vector2 ballvelocity = ballRigidbody.velocity;
            float ballSpeed = ballRigidbody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballvelocity;
            float ballFriction = ballCollider.friction;

            float impulseplayer1X = player1.LastContactPoint.normalImpulse;
            float impulseplayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulseplayer2X = player2.LastContactPoint.normalImpulse;
            float impulseplayer2Y = player2.LastContactPoint.tangentImpulse;

            //menentukan debugtext
            string debugText =
                "Ball mass = " + ballMass + "\n" +
            "Ball Velocity = " + ballvelocity + "\n" +
            "Ball Speed = " + ballSpeed + "\n" +
            "Ball Momentum =" + ballMomentum + "\n" +
            "Ball Friction= " + ballFriction + "\n" +
            "Last Impulse From Player 1 =(" + impulseplayer1X + "," + impulseplayer1Y + ")\n" +
            "Last Impulse From Player 2 =(" + impulseplayer2X + "," + impulseplayer2Y + ")\n";

            //tampilkan debug window
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);

            //kembalikan ke warna lama GUI
            GUI.backgroundColor = oldcolor;
        }
            //toggle nilai window ketika pemain mengklik tombol
            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "Toggle \nDebug Info"))
            {
                isDebugWindowShown = !isDebugWindowShown;
                trajectory.enabled = !trajectory.enabled;
            }
        
    }


    // Update is called once per frame
    void Update()
    {

    }
}
