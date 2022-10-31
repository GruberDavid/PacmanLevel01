using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject startText;

    public TMP_Text score;
    public TMP_Text highscore;

    public TMP_Text gameover;

    public Image[] lives;
    // Start is called before the first frame update
    void Start()
    {
        this.score.text = this.gameManager.score.ToString();
        foreach(Image life in lives)
        {
            life.enabled = true;
        }
    }

    public void UpdateScore()
    {
        this.score.text = this.gameManager.score.ToString();

        if(this.gameManager.highscore < this.gameManager.score)
        {
            this.gameManager.highscore = this.gameManager.score;
            this.highscore.text = this.gameManager.highscore.ToString();
        }
    }

    public void UpdateLifes()
    {
        switch (this.gameManager.lives)
        {
            case 3:
                lives[0].enabled = true;
                lives[1].enabled = true;
                lives[2].enabled = true;
                break;
            case 2:
                lives[0].enabled = true;
                lives[1].enabled = true;
                lives[2].enabled = false;
                break;
            case 1:
                lives[0].enabled = true;
                lives[1].enabled = false;
                lives[2].enabled = false;
                break;
            default:
                lives[0].enabled = false;
                lives[1].enabled = false;
                lives[2].enabled = false;
                break;
        }
    }
}
