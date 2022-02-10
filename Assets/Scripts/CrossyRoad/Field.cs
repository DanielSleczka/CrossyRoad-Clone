using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private int indexID;
    [SerializeField] private bool canEnter;


    public void SetCanEnter(bool value)
    {
        canEnter = value;
    }

    public bool GetCanEnter()
    {
        return canEnter;
    }

}
