using UnityEngine;

public class StartDoorScript : MonoBehaviour
{
    private Animator doorAnim;

    private void Awake()
    {
        doorAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            doorAnim.SetBool("IsOpen", true);
            GameManager.Instance.StartGame();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            doorAnim.SetBool("IsOpen", false);
        }
    }

}
