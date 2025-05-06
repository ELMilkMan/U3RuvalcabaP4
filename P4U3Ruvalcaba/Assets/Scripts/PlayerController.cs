using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool doubleJumpUsed = false;
    public float doubleJumpForce = 2.0f;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip crashSound;
    public AudioClip jumpSound;
    private AudioSource playerAudio;


    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& isOnGround && !gameOver)
        {
            playerAudio.PlayOneShot(jumpSound, 2.0f);

            dirtParticle.Stop();

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");

            doubleJumpUsed = false;
        }
        else if(Input.GetKeyDown(KeyCode.Space)&& !isOnGround&&!doubleJumpUsed)
        {
            doubleJumpUsed= true;
            playerRb.AddForce(Vector3.up*doubleJumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3,0f);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            dirtParticle.Stop();

            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 4.0f);

        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            dirtParticle.Play();

            

            isOnGround=true;
            isOnGround=true;
        }
    }
}
