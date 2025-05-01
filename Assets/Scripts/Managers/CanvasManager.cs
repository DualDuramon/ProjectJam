using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    //총알 관련
    public Text nowBulletText;
    public Text maxBulletText;

    //hp바 관련
    public Slider HpBar;
    public Text nowHpText;
    public Text maxHpText;

    //점수 관련
    public Text scoreText;

    //종료버튼셋
    public GameObject BtnPanel;
    [SerializeField] private Button exitBtn;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button GoTitleBtn;

    private static CanvasManager instance;

    public static CanvasManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        InitBtns();
    }

    private void InitBtns()
    {
        if(restartBtn != null) restartBtn.onClick.AddListener(ReStartGame);
        if(exitBtn != null) exitBtn.onClick.AddListener(GoEndingScene);
        if (GoTitleBtn != null) GoTitleBtn.onClick.AddListener(GoStartScene);
    }

    private void ReStartGame()
    {
        GameManager.Instance.ResetScore();
        GameManager.Instance.ChangeScene(1);
    }

    private void GoStartScene()
    {
        GameManager.Instance.ChangeScene(0);
    }

    private void GoEndingScene()
    {
        GameManager.Instance.ChangeScene(2);
    }

    public void NowBulletTextUpdate(int bullets)
    {
        nowBulletText.text = bullets.ToString();
    }
    
    public void MaxBulletTextUpdate(int bullets)
    {
        maxBulletText.text = bullets.ToString();
    }

    public void NowHpBarUpdate(float hp)
    {
        HpBar.value = hp;
        nowHpText.text = hp.ToString();
    }

    public void MaxHpBarUpdate(float maxHp)
    {
        HpBar.maxValue = maxHp;
        maxHpText.text = maxHp.ToString();
    }

    public void UpdateScoreText(int score)
    {
        Debug.Log("Score Updated");
        scoreText.text = score.ToString();
    }
}
