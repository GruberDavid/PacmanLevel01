using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GameManager gameManager { get; private set; }

    public int phase;

    public int points = 200;

    public Movement movement { get; private set; }

    public enum ghostType
    {
        Blinky,
        Inky,
        Pinky,
        Clyde
    };
    public ghostType type;

    public GhostBehaviour initBehaviour;
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public Transform[] scatterZone;

    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }

    public Transform pacman;

    private void Awake()
    {
        this.gameManager = FindObjectOfType<GameManager>();

        this.movement = GetComponent<Movement>();

        this.home = GetComponent<GhostHome>();
        this.scatter = GetComponent<GhostScatter>();
        this.chase = GetComponent<GhostChase>();
        this.frightened = GetComponent<GhostFrightened>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.home.Disable();
        this.scatter.Disable();
        this.chase.Disable();
        this.frightened.Disable();
        this.initBehaviour.Enable();
    }

    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same since it determines draw depth
        position.z = transform.position.z;
        transform.position = position;
    }


    public void ChangePhase()
    {
        this.phase++;
        switch (this.phase)
        {
            case 2 :
                this.scatter.duration = 7;
                this.chase.duration = 20;
                break;
            case 3 :
                this.scatter.duration = 5;
                this.chase.duration = 20;
                break;
            case 4 :
                this.scatter.duration = 5;
                this.chase.duration = 20;
                break;
            default :
                this.scatter.duration = 0.5f;
                this.chase.duration = 20;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled)
            {
                this.gameManager.EatGhost(this);
            }
            else
            {
                this.gameManager.Death();
            }
        }
    }
}
