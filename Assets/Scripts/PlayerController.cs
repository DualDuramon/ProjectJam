using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //기본 컴포넌트들
    private Rigidbody myRigid;
    [SerializeField]private Collider myCol;
    private Animator myAnim;

    //플레이어 스테이터스
    [SerializeField] PlayerStatus myStatus;

    //이동,점프 관련 변수
    private Vector3 moveInput = Vector3.zero;

    //점프 관련 변수
    private bool isOnGround = false;

    //카메라 관련 변수
    private Camera myCamera;

    private float rotX = 0.0f;
    [SerializeField]private float camRotSpeed_x = 10.0f;
    [SerializeField]private float camRotSpeed_y = 5.0f;

    //공격 관련 변수
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private MuzzleController bulletMuzzle;
    [SerializeField] private Transform bulletPos;


    private void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
        myCol = GetComponent<Collider>();
        myAnim = GetComponent<Animator>();
        myCamera = Camera.main;
        rotX = myCamera.transform.localRotation.eulerAngles.x;
        myStatus = GetComponent<PlayerStatus>();
    }

    private void OnEnable()
    {
        InitiateStatus();
    }

    private void InitiateStatus()
    {
        //ReferenceManager.Instance.Player = gameObject; //ReferenceManager에 Player를 등록한다.
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        TryPlayerMovement();
        TryPlayerRotate();
        TryPlayerJump();
        TryReload();
        TryShootBullet();

    }

    private void TryPlayerMovement()
    {
        GetPlayerMovement();
    }

    private void TryPlayerRotate()
    {
        transform.Rotate(Vector3.up, Input.mousePositionDelta.x * camRotSpeed_x * Time.deltaTime);

        rotX -= Input.mousePositionDelta.y * camRotSpeed_y * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -4.0f, 27.0f);

        myCamera.transform.localRotation = Quaternion.Euler(rotX, 0.0f, 0.0f);
    }


    private void GetPlayerMovement()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.z = Input.GetAxisRaw("Vertical");

        transform.Translate(moveInput * myStatus.MoveSpeed * Time.deltaTime);

        myAnim.SetFloat("speedX", moveInput.x);
        myAnim.SetFloat("speedZ", moveInput.z);
    }

    private void TryPlayerJump()
    {
        CheckOnGround();
        if (isOnGround && Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }
    }

    private void PlayerJump()
    {
        myRigid.AddForce(Vector3.up * myStatus.JumpForce, ForceMode.Impulse);
        isOnGround = false;
        myAnim.SetTrigger("Jump");
    }
    private void CheckOnGround()
    {
        if (!isOnGround)
        {
            isOnGround = Physics.Raycast(myCol.bounds.center, Vector3.down, 1.2f);
        }
        
    }

    private void TryShootBullet()
    {
        if (Input.GetMouseButton(0)&& myStatus.CanAttack())
        {
            ShootBullet();
            myStatus.NowAttackDelay = 0.0f;
            myAnim.SetTrigger("Fire");
        }
    }

    private void ShootBullet()
    {
        myStatus.DecreaseBullet(1);
        bulletMuzzle.PlayMuzzleFlash();
        Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
    }

    private void TryReload()
    {
        if(Input.GetKeyDown(KeyCode.R)&& myStatus.NowBullets < myStatus.MaxBullets)
        {
            myAnim.SetTrigger("Reload");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(myCol.bounds.center, Vector3.down * 1.2f);
    }

}
