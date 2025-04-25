using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Text nowBulletText;
    public Text maxBulletText;
    public Slider HpBar;

    public static CanvasManager instance;
    
    private void Awake()
    {
        instance = this;
    }

    public void NowBulletTextUpdate(int bullets)
    {
        nowBulletText.text = bullets.ToString();
    }
    
    public void MaxBulletTextUpdate(int bullets)
    {
        maxBulletText.text = bullets.ToString();
    }
}
