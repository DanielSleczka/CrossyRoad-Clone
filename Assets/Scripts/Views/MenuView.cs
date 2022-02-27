using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuView : BaseView
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    public void InitializeView()
    {
        base.ShowView();
    }

    public void OnStartButtonClicked_AddListener(UnityAction listener)
    {
        startButton.onClick.AddListener(listener);
    }

    public void OnStartButtonClicked_RemoveListener(UnityAction listener)
    {
        startButton.onClick.RemoveListener(listener);
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
