using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float jump_Power = 25f;
    private bool is_Jump = false;
    private Rigidbody playerRb;
    private Vector3 gravity_Vector = new Vector3(0, -49.05f,0);
    private float gravity_Modify = 5f;
    private QuizManager quizManager;
    private GameManager gameManager;
    private SoundManager soundManager;
    private Animator animator_Player;

    public ParticleSystem dirt_Particle;
    public ParticleSystem explosion_Particle;

    private void Awake()
    {
        animator_Player = GetComponent<Animator>();
        animator_Player.SetFloat("Speed_f", 1);
    }

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = gravity_Vector;
        quizManager = GameObject.Find("Quiz_Manager").GetComponent<QuizManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        dirt_Particle.Play();
    }

    private void Update()
    {
        if(gameManager.is_Activate == true)
        {
            Player_Jump();
        }
    }

    void Player_Jump() // player press space button, player jump to y position
    {
        if(Input.GetKeyDown(KeyCode.Space) && is_Jump ==false)
        {
            playerRb.AddForce(Vector3.up * jump_Power , ForceMode.Impulse);
            animator_Player.SetTrigger("Jump_trig");
            soundManager.Jump();
            is_Jump = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground")) // if player collide on ground, playe can use player jump
        {
            is_Jump = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CorrectWall")) // if player collide this wall, next quiz appear on screen
        {
            quizManager.quiz_Correct();
        }

        if (other.CompareTag("Wall")) // if player collide wall, player has game over state
        {
            gameManager.is_Activate = false;
            animator_Player.SetBool("Death_b", true);
            animator_Player.SetInteger("DeathType_int", 1);
            explosion_Particle.Play();
            dirt_Particle.Stop();
            soundManager.Crash();
            soundManager.BGM_Stop();
            StartCoroutine(quizManager.Gameover_Delay());
            Debug.Log("Wall Crash");
        }
    }
}
