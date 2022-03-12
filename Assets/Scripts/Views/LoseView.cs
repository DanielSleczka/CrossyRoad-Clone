using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class LoseView : BaseView
{
    [SerializeField] private TextMeshProUGUI scoreValue;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button exitButton;

    public override void ShowView()
    {
        transform.localScale = new Vector3(0f, 0f, 1f);
        base.ShowView();
        transform.DOScale(1f, 2f);
    }

    public void ShowScoreValue(float points)
    {
        scoreValue.text = $"{points}";
    }
    
    public void OnResetButtonClicked_AddListener(UnityAction listener)
    {
        resetButton.onClick.AddListener(listener);
    }

    public void OnResetButtonClicked_RemoveListener(UnityAction listener)
    {
        resetButton.onClick.RemoveListener(listener);
    }

    public void OnExitButtonClicked_AddListener(UnityAction listener)
    {
        exitButton.onClick.AddListener(listener);
    }

    public void OnExitButtonClicked_RemoveListener(UnityAction listener)
    {
        exitButton.onClick.RemoveListener(listener);
    }

}
