using Unity.VisualScripting;
using UnityEditor;
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

    //카메라 관련 변수
    private Camera myCamera;

    private float rotX = 0.0f;
    private float camRotSpeed_x = 10.0f;
    private float camRotSpeed_y = 5.0f;


    private void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
        myCol = GetComponent<Collider>();
        myCamera = Camera.main;
        rotX = myCamera.transform.localRotation.eulerAngles.x;

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
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }

        TryPlayerMovement();
        TryPlayerRotate();
        TryPlayerJump();

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
