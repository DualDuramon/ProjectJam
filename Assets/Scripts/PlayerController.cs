using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //기본 컴포넌트들
    private Rigidbody myRigid;
    [SerializeField]private Collider myCol;

    //이동,점프 관련 변수
    private Vector3 moveInput = Vector3.zero;
    [SerializeField]private float moveSpeed = 5.0f;

    //점프 관련 변수
    [SerializeField]private float jumpForce = 5.0f;
    private bool isOnGround = false;

    private void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
        myCol = GetComponent<Collider>();
    }


    private void Update()
    {
        TryPlayerMovement();
        TryPlayerJump();
    }

    private void TryPlayerMovement()
    {
        GetPlayerMovement();
    }


    private void GetPlayerMovement()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.z = Input.GetAxisRaw("Vertical");

        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
    }

    private void TryPlayerJump()
    {
        CheckOnGround();
        GetPlayerJump();
    }

    private void GetPlayerJump()
    {
        if (isOnGround && Input.GetButtonDown("Jump"))
        {
            myRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }
    private void CheckOnGround()
    {
        if (!isOnGround)
        {
            isOnGround = Physics.Raycast(myCol.bounds.center, Vector3.down, 1.2f);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(myCol.bounds.center, Vector3.down * 1.2f);
    }

}
