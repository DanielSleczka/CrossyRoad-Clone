using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameView : BaseView
{
    [SerializeField] private TextMeshProUGUI pointsValue;

    public void UpdatePoints(float points)
    {
        pointsValue.text = $"{points}";
    }
}