using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private int indexID;
    [SerializeField] private bool canEnter = true;
    public bool CanEnter => canEnter;


    public void SetCanEnter(bool value)
    {
        canEnter = value;
    }
}
