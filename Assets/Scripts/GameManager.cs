using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;

    public bool gameStarted = false;

    public Ghost[] ghosts;
    public Pacman pacman;
    
    public Transform pellets;
    public int nbStartPellets = 244;
    public int nbPellets = 244;

    public int score { get; private set; }
    public int highscore = 0;
    private int ghostMultiplier = 1;
    public int lives { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        this.uiManager.startText.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if(!gameStarted && Input.anyKeyDown)
        {
            this.uiManager.startText.SetActive(false);
            gameStarted = true;
            NewGame();
        }

        if((this.lives<=0) && (Input.GetKeyDown(KeyCode.R)))
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        this.uiManager.gameover.enabled = false;
        SetScore(0);
        this.highscore = 0;
        this.uiManager.UpdateScore();
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        this.nbPellets = this.nbStartPellets;

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();

            this.ghosts[i].phase = 1;
            this.ghosts[i].scatter.duration = 7;
            this.ghosts[i].chase.duration = 20;
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
    }

    private void GameOver()
    {
        this.uiManager.gameover.enabled = true;
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void EatPellet(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(score + pellet.points);

        this.uiManager.UpdateScore();

        nbPellets--;

        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void EatPowerPellet(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].frightened.Enable(pellet.duration);
        }

        EatPellet(pellet);

        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    public void EatGhost(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + ghost.points);
        this.uiManager.UpdateScore();
        this.ghostMultiplier++;
    }

    public void Death()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);
        this.uiManager.UpdateLifes();

        if(this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }
}
