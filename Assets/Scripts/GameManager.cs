using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager INSTANCE;

    public GameOver GameOver;

    int points = 0;
    int maxPoints = 20;

    private void Awake()
    {
        INSTANCE = this;
    }


    private void Update()
    {
        if (points > maxPoints)
        {
            GameOver.Setup();
        }
    }

    public void UpdatePoints(int points)
    {
        this.points += points;
    }


}
