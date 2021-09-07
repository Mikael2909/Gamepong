using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidewall : MonoBehaviour
{
    public PlayerControl player;
    [SerializeField]
    private gameManager gameManager;
    void OnTriggerEnter2D(Collider2D anotherCollider)
    {
        if (anotherCollider.name == "bola")//jika objek bernama ball
        {
            player.IncrementScore();

            if (player.Score < gameManager.maxScore)
            {
                //restart game
                anotherCollider.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }   
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
