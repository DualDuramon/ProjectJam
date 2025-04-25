using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    //기본 컴포넌트들
    [SerializeField] private NavMeshAgent myAgent;
    [SerializeField] private Animator myAnim;

    //적 스테이터스
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private float attackRange = 2.0f;

    //공격관련
    private float nowAtkCoolTime = 1.0f;
    private GameObject detectedObj = null;

    public float NowAtkCoolTime {
        get { return nowAtkCoolTime; }
        private set
        {
            nowAtkCoolTime = (value < maxAtkCoolTime ? value : maxAtkCoolTime);
        } 
    }
    public float maxAtkCoolTime = 2.0f;

    //플레이어
    [SerializeField] private GameObject player;

    private void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        InitiateStatus();
    }

    private void InitiateStatus()
    {
        myAgent.speed = moveSpeed;
    }

    void Update()
    {
        MoveTowardsPlayer();
        NowAtkCoolTime += Time.deltaTime;        
        if(myAgent.remainingDistance < attackRange)
        {
            FaceTowards();
        }
        TryAttack();
    }

    private void MoveTowardsPlayer()
    {
        myAgent.SetDestination(player.transform.position);
        myAnim.SetFloat("Speed", myAgent.velocity.magnitude);
    }

    private void FaceTowards()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0.0f;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5.0f);
    }

    protected virtual void TryAttack()
    {
        if(Physics.Raycast(transform.position + Vector3.up, transform.forward, out RaycastHit hit, attackRange, LayerMask.GetMask("Player")))
        {
            if(nowAtkCoolTime >= maxAtkCoolTime)
            {
                Attack(hit.rigidbody.gameObject);
            }
        }
    }

    protected virtual void Attack(GameObject target)
    {
        Debug.Log(target.name +" 검출");
        nowAtkCoolTime = 0.0f;
        myAnim.SetTrigger("AttackTrigger");

    }
}
