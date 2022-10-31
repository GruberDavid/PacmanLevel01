using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    public Transform target;

    private void Start()
    {
        this.target = this.ghost.scatterZone[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();
        if ((node != null) && this.enabled && !this.ghost.frightened.enabled)
        {
            bool inZone = false;
            if(collision.transform.position == this.ghost.scatterZone[0].position)
            {
                target = this.ghost.scatterZone[1];
                inZone = true;
            }
            else if (collision.transform.position == this.ghost.scatterZone[1].position)
            {
                target = this.ghost.scatterZone[2];
                inZone = true;
            }
            else if (collision.transform.position == this.ghost.scatterZone[2].position)
            {
                target = this.ghost.scatterZone[3];
                inZone = true;
            }
            else if (collision.transform.position == this.ghost.scatterZone[3].position)
            {
                target = this.ghost.scatterZone[0];
                inZone = true;
            }
            UpdateDirection(node, inZone);
        }
    }

    public void UpdateDirection(Node node, bool inZone)
    {
        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                if ((availableDirection == -this.ghost.movement.prevDirection) && !inZone)
                {
                    continue;
                }

                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (this.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }

    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }
}
