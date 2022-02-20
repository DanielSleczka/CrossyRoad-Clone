using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSegment : MonoBehaviour
{
    [SerializeField] private int segmentID;
    public int SegmentID => segmentID;

    [SerializeField] protected List<Field> fields;
    [SerializeField] private bool testMode;

    private void Start()
    {
        if (testMode)
        {
            InitializeSegment();
        }
    }

    private void Update()
    {
        if (testMode)
        {
            UpdateSegment();
        }
    }

    public virtual void InitializeSegment()
    {

    }

    public virtual void UpdateSegment()
    {

    }

    public virtual void DeinitalizeSegment()
    {

    }

    public void SetID(int value)
    {
        segmentID = value;
    }

    public bool CheckFieldEnterable(int index, out Vector3 position)
    {
        bool canEnter = fields[index].CanEnter;
        position = canEnter ? fields[index].transform.position : Vector3.zero;
        return canEnter;
    }
}
