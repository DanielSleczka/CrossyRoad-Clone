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
    public void SetNotEnterableFields()
    {
        for (int i = 0; i < fields.Count; i++)
        {
            if (i < 5 || i > 13)
            {
                fields[i].SetCanEnter(false);
            }
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
}
