using UnityEngine;

public class GunBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private float dmg = 10.0f;
    private float lifeTime = 5.0f;
    private float nowLifeTime = 0.0f;
    [SerializeField] private GameObject onHitEffectPrefab;
    [SerializeField] private Rigidbody myRigid;
    public float Dmg { get;set; }

    private void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
        Dmg = dmg;
    }

    private void Start()
    {
        myRigid.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    private void Update()
    {
        nowLifeTime += Time.deltaTime;
        if (nowLifeTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //单固瘤 贸府 肺流
            Debug.Log("利 鸥拜");
        }
        Instantiate(onHitEffectPrefab, transform.position + Vector3.back*0.1f, Quaternion.identity);
        Destroy(gameObject);
    }
}
