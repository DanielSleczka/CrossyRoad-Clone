using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private GameView gameView;

    public int currentPoints = 0;
    public int positionBased = 0;

    public void InitializeSystem()
    {
        gameView.UpdatePoints(currentPoints);
    }

    public int GetScore()
    {
        return currentPoints;
    }

    public void AddPoints(int segmentMovement)
    {
        positionBased += segmentMovement;
        if (positionBased > currentPoints)
        {
            currentPoints = positionBased;
        }
        gameView.UpdatePoints(currentPoints);
    }
}
