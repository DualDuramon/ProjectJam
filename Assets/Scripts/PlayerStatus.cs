using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerStatus : CharacterStatus
{
    [SerializeField] PlayerController myController;
    [SerializeField] private int nowBullets = 0;
    [SerializeField] private int maxBullets = 30;
    [SerializeField] private bool isReloading = false;

    public int NowBullets { get { return nowBullets; } set { nowBullets = value < 0 ? 0 : value; } }
    public int MaxBullets { get { return maxBullets; } set { maxBullets = value; } }
    public bool IsReloading { get { return isReloading; } set { isReloading = value; } }


    private void Awake()
    {
        myController = GetComponent<PlayerController>();
    }
    private void Start()
    {
        CanvasManager.Instance.MaxBulletTextUpdate(maxBullets); //UI에 최대 총알 수 업데이트
        CanvasManager.Instance.MaxHpBarUpdate(MaxHp); //UI에 최대 체력 업데이트
        CanvasManager.Instance.NowBulletTextUpdate(nowBullets); //UI에 총알 수 업데이트
        CanvasManager.Instance.NowHpBarUpdate(Hp); //UI에 현재 체력 업데이트
    }

    public void ReloadBullets() //애니메이터에서 호출
    {
        Debug.Log("Loaded");
        nowBullets = maxBullets;
        isReloading = false;
        CanvasManager.Instance.NowBulletTextUpdate(nowBullets); //UI에 총알 수 업데이트
    }

    public override bool CanAttack()
    {
        return base.CanAttack() && nowBullets > 0;
    }

    public void DecreaseBullet(int amount)
    {
        NowBullets -= amount;
        CanvasManager.Instance.NowBulletTextUpdate(nowBullets);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        CanvasManager.Instance.NowHpBarUpdate(Hp); //UI에 현재 체력 업데이트
    }

    protected override void GetDie()
    {
        IsDead = true;
        myController.DeadSeqControl();
    }
}
