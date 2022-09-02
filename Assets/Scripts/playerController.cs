using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Collider2D playerCollider;
    private SpriteRenderer playerRenderer;
    public LayerMask groundMask;
    public float jumpForce;
    public float movementSpeed;
    int jumps;
    public Animator animator;
    Vector3 startPosition;

    public const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const string STATE_VFORCE = "verticalForce";
    const string STATE_WALKING = "isWalking";
    const string STATE_ISSTATIC = "isStatic";

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Indicamos que esta vivo y que esta en el suelo al inicio del juego

        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
        animator.SetBool(STATE_WALKING, false);

        startPosition = this.transform.position;

    }

    public void StartGame()
    {
        
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
        animator.SetBool(STATE_WALKING, false);

        Invoke("RestartPosition", 0.1f);

    }

    public void RestartPosition()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
        animator.SetBool(STATE_WALKING, false);
        this.transform.position = startPosition;
        this.playerRigidbody.velocity = Vector2.zero;

        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();

    }

    // Update is called once per frame
    void Update()
    {


        //Condicionales que detecta si el Collider del jugador esta tocando la capa Suelo
        if (playerCollider.IsTouchingLayers(groundMask)) //Condicional 1
        {
            animator.SetBool(STATE_ON_THE_GROUND, true);
            jumps = 0;
            playerRigidbody.mass = 1f;

        }
        else if (!playerCollider.IsTouchingLayers(groundMask)) //Condicional 2
        {
            animator.SetBool(STATE_ON_THE_GROUND, false);

        }

        //Condicional para que se mueva solo cuando esta InGame
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            //Esto settea la velocidad del jugador en el Animator
            animator.SetFloat(STATE_VFORCE, playerRigidbody.velocity.y);

            //Condicional para saltar usando el espacio
            if (Input.GetKeyDown(KeyCode.Space))
            {

                Jump(3f, animator, playerRigidbody);

            }
            Movement(playerRigidbody, playerRenderer, animator);

        }
        else
        {
            //Si no esta InGame, no se mueve;
            playerRigidbody.velocity = new Vector2(0, 0);
        }

    }
    private void FixedUpdate()
    {          

    }
    //Metodo para saltar
    void Jump(float jumpForce, Animator animator, Rigidbody2D rigidbody2D)
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {

            //Condicionales que limitan la velocidad de salto teniendo en cuenta las veces que se ha saltado
            if (jumps == 0)
            {
                rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetBool(STATE_ON_THE_GROUND, false);

                jumps++;

            }
            else if (jumps <= 3)
            {
                rigidbody2D.AddForce(Vector2.up * 0f, ForceMode2D.Impulse);
                rigidbody2D.AddForce((Vector2.up * jumpForce) / 2, ForceMode2D.Impulse);

                animator.SetBool(STATE_ON_THE_GROUND, false);
                jumps++;

            }

        }
    }
    float Movement(Rigidbody2D playerRigidbody, SpriteRenderer playerRenderer, Animator animator)
    {
        if (Input.GetAxis("Horizontal") < 0f)
        {
            playerRenderer.flipX = true;
            playerRigidbody.velocity = new Vector2(-movementSpeed, playerRigidbody.velocity.y);
            animator.SetBool(STATE_WALKING, true);
            animator.SetBool(STATE_ISSTATIC, false);

        }
        else if (Input.GetAxis("Horizontal") > 0f) 
        {
            playerRenderer.flipX = false;
            playerRigidbody.velocity = new Vector2(movementSpeed, playerRigidbody.velocity.y);
            animator.SetBool(STATE_WALKING, true);
            animator.SetBool(STATE_ISSTATIC, false);
        }
        else
        {
            animator.SetBool(STATE_WALKING, false);
            animator.SetBool(STATE_ISSTATIC, true);
        }
        

        return playerRigidbody.velocity.x;
    }

    public void Die() 
    {

        animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    
    }

}

