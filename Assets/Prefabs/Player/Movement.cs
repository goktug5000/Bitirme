using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Movement _movement;
    [SerializeField] private float speed;

    private GameObject playerObj;
    private GameObject playerTipObj;
    private VCLookForward vCLookForward;
    private Animator playerAnimator;
    private Rigidbody playerRigidbody;

    public bool inAttackAnim;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Dash")]
    private bool isDashing;
    [SerializeField] private float dashingTime;

    [Header("Jump")]
    public bool isGrounded;
    [SerializeField] private float jumpingPower;


    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isJumping;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;


    [SerializeField] private int jumpMax;
    private int jumpLeft;

    void Awake()
    {
        if (_movement != null)
        {
            Destroy(this);
        }
        else
        {
            _movement = this;
        }
    }

    void Start()
    {
        playerObj = PlayerConstantsHolder._playerConstantsHolder.holderObj;
        playerTipObj = PlayerConstantsHolder._playerConstantsHolder.holderTipObj;
        playerAnimator = PlayerConstantsHolder._playerConstantsHolder.holderAnimator;
        playerRigidbody = PlayerConstantsHolder._playerConstantsHolder.holderRigidbody;
        vCLookForward = VCLookForward._vCLookForward;
    }

    void Update()
    {
        PlayerMove();
        PlayerJump();
    }

    private async void PlayerMove()
    {
        if (CanMove())
        {
            Vector2 MovementVec = MovementVector();
            if(MovementVec.magnitude > 0.2f)
            {
                float angle = Mathf.Atan2(MovementVec.y, MovementVec.x) * Mathf.Rad2Deg;
                angle += vCLookForward.GetCamAngle();
                Quaternion targetRotation = Quaternion.Euler(0, angle, 0);

                playerObj.transform.rotation = Quaternion.Slerp(
                    playerObj.transform.rotation,
                    targetRotation,
                    Time.deltaTime * rotationSpeed
                );

                playerAnimator.SetBool("Movement", true);
                playerObj.transform.Translate(0, 0, MovementVec.magnitude * Time.deltaTime * speed);
            }
            else
            {
                playerAnimator.SetBool("Movement", false);
            }

            if (Input.GetKeyDown(KeyBindings.KeyCodes[KeyBindings.KeyCode_Dash]))
            {
                await Teleport(1000, 0.3f);
            }
        }
    }

    private void PlayerJump()
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            jumpLeft = jumpMax;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyBindings.KeyCodes[KeyBindings.KeyCode_Jump]))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if ((coyoteTimeCounter > 0f || jumpLeft > 0) && jumpBufferCounter > 0f && !isJumping)
        {
            StartCoroutine(Jump());
            StartCoroutine(JumpCooldown());
        }
    }

    IEnumerator Jump()
    {
        playerAnimator.SetTrigger("Jump");
        playerAnimator.SetBool("Grounded", false);
        playerRigidbody.AddForce(jumpingPower * Vector3.up);
        //Instantiate(SFX_Jump);
        jumpBufferCounter = 0f;
        coyoteTimeCounter = 0;
        isGrounded = false;
        yield return new WaitForSeconds(dashingTime / 2);
        playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y * 0.3f, playerRigidbody.velocity.z);
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (!GlobalConstans.NonJumpableTags.Contains(col.gameObject.tag))
        {
            isGrounded = true;
            playerAnimator?.SetBool("Grounded", true);
        }
    }

    private Vector2 MovementVector()
    {
        Vector2 MovementVec = new Vector2();

        if (Input.GetKey(KeyBindings.KeyCodes[KeyBindings.KeyCode_Forward]))
        {
            MovementVec += new Vector2(1, 0);
        }
        if (Input.GetKey(KeyBindings.KeyCodes[KeyBindings.KeyCode_Right]))
        {
            MovementVec += new Vector2(0, 1);
        }
        if (Input.GetKey(KeyBindings.KeyCodes[KeyBindings.KeyCode_Left]))
        {
            MovementVec += new Vector2(0, -1);
        }
        if (Input.GetKey(KeyBindings.KeyCodes[KeyBindings.KeyCode_Back]))
        {
            MovementVec += new Vector2(-1, 0);
        }

        return MovementVec.normalized;
    }

    public bool CanMove()
    {
        return !isDashing && !inAttackAnim;
    }

    public bool CanDash()
    {
        return !isDashing;
    }

    public async Task Teleport(float power, float teleportTime)
    {
        await DashAsync(1000, teleportTime, Vector3.forward);
    }

    public async Task DashAsync(float power, float? dashingTimee = null, Vector3? dashDir = null)
    {
        if (!CanDash())
        {
            return;
        }

        var myDashDir = playerObj.transform.forward;
        var myDashingTime = dashingTimee.HasValue ? dashingTimee : dashingTime;
        if (dashDir.HasValue)
        {
            myDashDir = playerTipObj.transform.TransformDirection(dashDir.Value.normalized);
        }
        isDashing = true;
        playerRigidbody.AddForce(power * myDashDir);
        await Task.Delay((int)(myDashingTime * 1000));
        isDashing = false;
        playerRigidbody.velocity = Vector3.zero;
    }
}
