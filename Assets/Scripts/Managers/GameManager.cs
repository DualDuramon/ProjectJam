using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int nowScore;
    [SerializeField] private EnemySpawner spawner;

    static private GameManager instance;
    static public GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            GameManager.Instance.spawner = spawner;
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        nowScore = 0;
    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;
        spawner.StartSpawn();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CanvasManager.Instance.BtnPanel.SetActive(false);
    }

    public void HoldGame()
    {
        Time.timeScale = 0.0f;
        spawner.StopSpawn();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        CanvasManager.Instance.BtnPanel.SetActive(true);
    }

    public void AddScore(int score)
    {
        nowScore += score;
        CanvasManager.Instance.UpdateScoreText(nowScore);
    }

    public void ResetScore()
    {
        nowScore = 0;
    }

    public void ChangeScene(int idx)
    {
        Time.timeScale = 1.0f;
        if (idx == 2) SceneManager.sceneLoaded += ShowScore;
        SceneManager.LoadScene(idx);
    }

    private void ShowScore(Scene arg0, LoadSceneMode arg1)
    {
        CanvasManager.Instance.UpdateScoreText(nowScore);
    }
}
