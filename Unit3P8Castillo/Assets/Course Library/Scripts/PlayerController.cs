using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpforce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioSource playerAudio;
    public bool canDoubleJump;
    public float dash = 2;
    public bool isDashing = false;
    private float animSpeed;
    public bool startGame = false;



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        _ = StartCoroutine(WalkThenRun());
        IEnumerator WalkThenRun()
        {
            WalkIn();
            yield return new WaitForSeconds(1.5f);
            Run();
        }

        void WalkIn()
        {
            playerAnim.SetFloat("Speed_f", 0.4f);
        }
        void Run()
        {
            playerAnim.SetFloat("Speed_f", 1f);
            startGame = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            canDoubleJump = true;
        }
        //double jump if
        if (Input.GetKeyDown(KeyCode.Space) && playerRb.velocity.y > 0f && canDoubleJump && !gameOver)
        {
            canDoubleJump = false;

            playerRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);


            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1);

        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animSpeed = playerAnim.GetFloat("Speed_f") * dash;
            playerAnim.SetFloat("Speed_f", animSpeed);
            isDashing = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerAnim.SetFloat("Speed_f", animSpeed / dash);
            isDashing = false;
        }
    }   
        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();

        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
