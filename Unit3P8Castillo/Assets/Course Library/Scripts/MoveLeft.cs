using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;
    private PlayerController playerControllerScript;
    private float leftBound;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript =
            GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.startGame == true)
        {
            if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

       if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        }
        else if (playerControllerScript.startGame == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * 6);
        }
        // Move the object to the left while the game is not over
        if (playerControllerScript.gameOver == false)
        {
            if (playerControllerScript.isDashing == true)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime * playerControllerScript.dash);
            }
            else
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
    }
}
