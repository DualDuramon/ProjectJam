using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    private GameObject player;
    public GameObject Player 
    {
        get { return player; }
        set { player = value; }
    }

    public static ReferenceManager Instance { get; private set; }
}
