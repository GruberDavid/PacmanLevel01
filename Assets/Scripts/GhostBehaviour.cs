using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehaviour : MonoBehaviour
{
    public Ghost ghost { get; private set; }

    public float duration;

    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
    }

    public void Enable()
    {
        Enable(this.duration);
    }

    public virtual void Enable(float duration)
    {
        if(duration > 0)
        {
            this.enabled = true;

            CancelInvoke();
            Invoke(nameof(Disable), duration);
        }
        else
        {
            Disable();
        }
    }

    public virtual void Disable()
    {
        this.enabled = false;

        CancelInvoke();
    }
}
