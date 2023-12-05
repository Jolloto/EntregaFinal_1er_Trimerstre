using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lateralSpeed = 5f;

    private float horizontalInput;
    private float verticalInput;

    private float xRange = 9f;
    private float zRange = 7f;

    private int points;
    private int lives;

    public bool isGameOver;

    void Start()
    {
        lives = 3;
    }

    
    void Update()
    {

        if (isGameOver = true)
        {
            //Movimiento vertical

            verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput);


            //Movimiento horizontal

            horizontalInput = Input.GetAxis("Horizontal");

            transform.Translate(Vector3.right * lateralSpeed * Time.deltaTime * horizontalInput);


            PlayerInBounds();

        }

        if (isGameOver = false) 
        {
        
        }
    }

    private void Awake() 
    {
        isGameOver = false;
    }
    
    private void PlayerInBounds() 
    {
        Vector3 pos = transform.position;

        //Limite por la izquierda

        if(pos.x < -xRange) 
        { 
            //transform.position = new Vector3(-xRange, pos.y, pos.z);
            pos.x= -xRange;

        }

        //Limite por la derecha

        if(pos.x > xRange) 
        {
            //transform.position = new Vector3(xRange, pos.y, pos.z);

            pos.x= xRange;
        }

        if (pos.z < -zRange)
        {
           
            pos.z = -zRange;

        }

        //Limite por la derecha

        if (pos.z > zRange)
        {
          

            pos.z = zRange;
        }

        transform.position = pos;
    }

    
    
    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("Good Coin")) 
        {
            points += 5;
            Debug.Log(points);

            if (points >= 50) 
            {
                Debug.Log("You Win");
            }   
        }

        if (other.gameObject.CompareTag("Bad Coin"))
        {
            lives --;

            if(lives <= 0) 
            {
                Debug.Log("Game Over");
            }
            Debug.Log(lives);
        }

        Destroy(other.gameObject);
    }

    private void GameOver() 
    {
        Debug.Log("GAME OVER");
        isGameOver = true;
    }
}
