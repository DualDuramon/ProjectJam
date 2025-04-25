using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerStatus : CharacterStatus
{
    [SerializeField] PlayerController myController;
    [SerializeField] private int nowBullets = 0;
    [SerializeField] private int maxBullets = 30;

    public int NowBullets { get { return nowBullets; } set { nowBullets = value < 0 ? 0 : value; } }
    public int MaxBullets { get { return maxBullets; } set { maxBullets = value; } }


    private void Awake()
    {
        myController = GetComponent<PlayerController>();
    }
    private void Start()
    {
        nowBullets = 5; //총알 초기화
        CanvasManager.instance.NowBulletTextUpdate(nowBullets); //UI에 총알 수 업데이트
        CanvasManager.instance.MaxBulletTextUpdate(maxBullets); //UI에 최대 총알 수 업데이트
    }

    public void ReloadBullets() //애니메이터에서 호출
    {
        Debug.Log("Loaded");
        nowBullets = maxBullets;
        CanvasManager.instance.NowBulletTextUpdate(nowBullets); //UI에 총알 수 업데이트
    }

    public override bool CanAttack()
    {
        return base.CanAttack() && nowBullets > 0;
    }

    public void DecreaseBullet(int amount)
    {
        NowBullets -= amount;
        CanvasManager.instance.NowBulletTextUpdate(nowBullets);
    }

    protected override void GetDie()
    {
        IsDead = true;
        myController.DeadSeqControl();
    }
}
