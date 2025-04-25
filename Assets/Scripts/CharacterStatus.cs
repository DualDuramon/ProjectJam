using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    //스테이터스
    [SerializeField] private float hp = 100.0f;
    [SerializeField] private float maxHp = 100.0f;

    [SerializeField] private float nowAttackDelay = 0.0f;
    [SerializeField] private float maxAttackDelay = 5.0f;

    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;

    [SerializeField] private float attackRange = 2.0f;

    public float Hp { get { return hp; } set { hp = value < 0 ? 0 : value; } }
    public float MaxHp { get { return maxHp; } set { maxHp = value; } }
    public float NowAttackDelay { get { return nowAttackDelay; } set { nowAttackDelay = value < maxAttackDelay ? value : maxAttackDelay; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }

    private void Update()
    {
        ChargeDelays();
    }

    protected virtual void ChargeDelays()
    {
        NowAttackDelay += Time.deltaTime;
    }

    public virtual bool CanAttack()
    {
        return NowAttackDelay >= maxAttackDelay;
    }
}
