using UnityEngine;

public class PlayerStatus : CharacterStatus
{
    [SerializeField] private int nowBullets = 0;
    [SerializeField] private int maxBullets = 30;

    public int NowBullets { get { return nowBullets; } set { nowBullets = value < 0 ? 0 : value; } }
    public int MaxBullets { get { return maxBullets; } set { maxBullets = value; } }


    private void Awake()
    {
        nowBullets = 5; //총알 초기화
    }
    public void ReloadBullets() //애니메이터에서 호출
    {
        Debug.Log("Loaded");
        nowBullets = maxBullets;
    }

    public override bool CanAttack()
    {
        Debug.Log("하하");
        return base.CanAttack() && nowBullets > 0;
    }

    public void DecreaseBullet(int amount)
    {
        NowBullets -= amount;
    }

}
