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
    [SerializeField] int pointspercoin = 5;
    private int lives;

    private const string BADCOIN_TAG = "Bad Coin";
    private const string GOODCOIN_TAG = "Good Coin";

    private const string DEATH_BOOL = "Death_b";


    [SerializeField] GameObject recolectgoodcoinParticleSystem;
    [SerializeField] GameObject recolectbadcoinParticleSystem;

    private AudioSource playerAudioSource;
    [SerializeField] AudioClip SFX_GOODCOIN;
    [SerializeField] AudioClip SFX_BADCOIN;

    [SerializeField] private AudioSource cameraAudioSource;

    private Animator playerAnimator;


    public bool isGameOver;
    public bool isYouWin;

    void Start()
    {
        lives = 3;
    }


 private void Awake() 
    {
        isGameOver = false;
        isYouWin = false;

        playerAudioSource = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();

    }
    
    void Update()
    {
        if(!isYouWin && !isGameOver)
        {

        
            //Movimiento vertical

            verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput);


            //Movimiento horizontal

            horizontalInput = Input.GetAxis("Horizontal");

            transform.Translate(Vector3.right * lateralSpeed * Time.deltaTime * horizontalInput);

            PlayerInBounds();
        }


        

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
       if(other.gameObject.CompareTag(GOODCOIN_TAG)) 
        {
            points += pointspercoin;
            Debug.Log(points);

            Instantiate(recolectgoodcoinParticleSystem, other.transform.position, Quaternion.identity);
            playerAudioSource.PlayOneShot(SFX_GOODCOIN, 1f);
            

            if (points >= 50) 
            {
                Debug.Log("You Win");
                YouWin();
            }   
        }

        if (other.gameObject.CompareTag(BADCOIN_TAG))
        {
            lives --;
            Debug.Log(lives);

            Instantiate(recolectbadcoinParticleSystem, other.transform.position, Quaternion.identity);
            playerAudioSource.PlayOneShot(SFX_BADCOIN, 1f);
            playerAnimator.SetTrigger("Dolor");


            if (lives <= 0) 
            {
                GameOver();
                cameraAudioSource.Stop();
            }
            
        }

        Destroy(other.gameObject);
    }

    private void GameOver() 
    {
        Debug.Log("GAME OVER");
        isGameOver = true;

    }

    private void YouWin() 
    {
        Debug.Log("YOU WIN");
        isYouWin = true;
    }

    void DestroyGameObject() 
    { 
        
    }
}
