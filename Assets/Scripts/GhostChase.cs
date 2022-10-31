using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private GameObject blinky = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();
        if((node != null) && this.enabled && !this.ghost.frightened.enabled)
        {
            UpdateDirection(node);
        }
    }

    private void UpdateDirection(Node node)
    {
        switch (this.ghost.type)
        {
            case Ghost.ghostType.Blinky:
                BlinkyChase(node);
                break;
            case Ghost.ghostType.Inky:
                InkyChase(node);
                break;
            case Ghost.ghostType.Pinky:
                PinkyChase(node);
                break;
            case Ghost.ghostType.Clyde:
                ClydeChase(node);
                break;
            default:
                break;
        }
    }

    private void BlinkyChase(Node node)
    {
        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;

        foreach (Vector2 availableDirection in node.availableDirections)
        {
            Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
            float distance = (ghost.pacman.position - newPosition).sqrMagnitude;

            if (distance < minDistance)
            {
                direction = availableDirection;
                minDistance = distance;
            }
        }

        ghost.movement.SetDirection(direction);
    }

    private void InkyChase(Node node)
    {
        if(blinky == null)
        {
            blinky = GameObject.FindGameObjectWithTag("Blinky");
        }

        if(blinky != null)
        {
            Vector3 pacPos = new Vector3(ghost.pacman.position.x, ghost.pacman.position.y);
            Vector3 pacDir = new Vector3(ghost.gameManager.pacman.movement.direction.x, ghost.gameManager.pacman.movement.direction.y);
            Vector3 target = pacPos + 2 * pacDir;

            Vector3 blinkyPos = new Vector3(blinky.transform.position.x, blinky.transform.position.y);

            Vector3 direction = target - blinkyPos;

            target = blinkyPos + 2 * direction;

            Vector2 dir = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (target - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    dir = availableDirection;
                    minDistance = distance;
                }
            }

            ghost.movement.SetDirection(dir);
        }
    }

    private void PinkyChase(Node node)
    {
        Vector3 pacPos = new Vector3(ghost.pacman.position.x, ghost.pacman.position.y);
        Vector3 pacDir = new Vector3(ghost.gameManager.pacman.movement.direction.x, ghost.gameManager.pacman.movement.direction.y);
        Vector3 target = pacPos + 2 * pacDir;

        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;

        foreach (Vector2 availableDirection in node.availableDirections)
        {
            Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
            float distance = (target - newPosition).sqrMagnitude;

            if (distance < minDistance)
            {
                direction = availableDirection;
                minDistance = distance;
            }
        }

        ghost.movement.SetDirection(direction);
    }

    private void ClydeChase(Node node)
    {
        Vector3 pacPos = new Vector3(ghost.pacman.position.x, ghost.pacman.position.y);
        float distance = (pacPos - transform.position).sqrMagnitude;
        
        if(distance > 64)
        {
            BlinkyChase(node);
        }
        else
        {
            bool inZone = false;
            if (node.transform.position == this.ghost.scatterZone[0].position)
            {
                ghost.scatter.target = this.ghost.scatterZone[1];
                inZone = true;
            }
            else if (node.transform.position == this.ghost.scatterZone[1].position)
            {
                ghost.scatter.target = this.ghost.scatterZone[2];
                inZone = true;
            }
            else if (node.transform.position == this.ghost.scatterZone[2].position)
            {
                ghost.scatter.target = this.ghost.scatterZone[3];
                inZone = true;
            }
            else if (node.transform.position == this.ghost.scatterZone[3].position)
            {
                ghost.scatter.target = this.ghost.scatterZone[0];
                inZone = true;
            }
            ghost.scatter.UpdateDirection(node, inZone);
        }

    }

    private void OnDisable()
    {
        this.ghost.scatter.Enable();
        this.ghost.ChangePhase();
    }
}
