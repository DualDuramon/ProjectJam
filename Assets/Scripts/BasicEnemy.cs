using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    //기본 컴포넌트들
    [SerializeField] private NavMeshAgent myAgent;
    [SerializeField] private Animator myAnim;
    [SerializeField] private Collider myCol;
    [SerializeField] private Rigidbody myRigid;

    //적 스테이터스
    [SerializeField] private CharacterStatus myStatus;

    //플레이어
    [SerializeField] private GameObject player;

    //그외
    [SerializeField] private int score;

    private void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();
        myStatus = GetComponent<CharacterStatus>();
        myCol = GetComponent<Collider>();
        myRigid = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        InitiateStatus();
    }

    private void InitiateStatus()
    {
        myAgent.speed = myStatus.MoveSpeed;
    }

    void Update()
    {
        if (!myStatus.IsDead)
        {
            MoveTowardsPlayer();
            if (myAgent.remainingDistance < myStatus.AttackRange)
            {
                FaceTowards();
            }
            TryAttack();
        }
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
        if(Physics.Raycast(transform.position + Vector3.up, transform.forward, out RaycastHit hit, myStatus.AttackRange))
        {

            if(hit.transform.CompareTag("Player") && myStatus.CanAttack() && !hit.transform.GetComponent<CharacterStatus>().IsDead)
            {
                Attack(hit.rigidbody.gameObject);
            }
        }
    }

    protected virtual void Attack(GameObject target)
    {

        target.GetComponent<CharacterStatus>().TakeDamage(myStatus.AttackDamage);
        myStatus.NowAttackDelay = 0.0f;
        myAnim.SetTrigger("AttackTrigger");

    }

    public void TakeDamage(float dmg)
    {
        myStatus.TakeDamage(dmg);
        if(myStatus.IsDead)
        {
            myAgent.isStopped = true;
            myAgent.enabled = false;
            myCol.enabled = false;
            myRigid.isKinematic = true;
            GameManager.Instance.AddScore(score);
            myAnim.SetBool("IsDead", true);
            Destroy(gameObject, 3.0f); //3초 후에 적 오브젝트 삭제
        }
    }

    public void Revive()
    {
        myStatus.Revive();
        myAgent.isStopped = false;
        myAgent.enabled = true;
        myCol.enabled = true;
        myRigid.isKinematic = false;
        myAnim.SetBool("IsDead", false);
    }
}
