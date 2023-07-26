using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;

    public float time = 0.5f;
    public int frame { get; private set; }

    public bool loop = true;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(UpdateFrame), this.time, this.time);
    }

    private void UpdateFrame()
    {
        if (!this.spriteRenderer.enabled)
        {
            return;
        }

        this.frame++;

        if ((this.frame > this.sprites.Length) && this.loop)
        {
            this.frame = 0;
        }

        if (this.frame < this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[this.frame];
        }
    }

    public void Restart()
    {
        this.frame = -1;
        UpdateFrame();
    }
}
