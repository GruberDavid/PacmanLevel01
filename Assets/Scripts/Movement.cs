using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }

    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;

    public Vector2 initDirection;
    public Vector2 direction { get; private set; }
    public Vector2 prevDirection { get; private set; }
    public Vector2 nextDirection { get; private set; }

    public Vector3 startPosition { get; private set; }

    public LayerMask obstacleLayer;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();

        this.startPosition = this.transform.position;
        this.prevDirection = Vector2.zero;
        this.nextDirection = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetState();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.nextDirection != Vector2.zero)
        {
            SetDirection(this.nextDirection);
        }
    }

    void FixedUpdate()
    {
        Vector2 pos = this.rb.position;
        Vector2 vTranslate = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
        this.rb.MovePosition(pos + vTranslate);
    }

    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startPosition;
        this.rb.isKinematic = false;
        this.enabled = true;
    }

    public void SetDirection(Vector2 dir, bool forced = false)
    {
        if (forced || !Occupied(dir))
        {
            this.prevDirection = this.direction;
            this.direction = dir;
            this.nextDirection = Vector2.zero;
        }
        else
        {
            this.nextDirection = dir;
        }
    }

    public bool Occupied(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, dir, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }
}
