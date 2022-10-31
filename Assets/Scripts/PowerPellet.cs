using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 8f;

    private void Awake()
    {
        points = 50;
    }

    protected override void Eat()
    {
        FindObjectOfType<GameManager>().EatPowerPellet(this);
    }
}
