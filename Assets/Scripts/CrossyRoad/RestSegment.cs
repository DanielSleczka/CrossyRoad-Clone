using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSegment : BaseSegment
{

    [SerializeField][Range(0, 100)] private int chanceToFreeSpace;
    [SerializeField] private List<GameObject> props;


    public override void InitializeSegment()
    {
        base.InitializeSegment();

        foreach(var field in fields)
        {
            int randomNumber = Random.Range(0, 101);
            if (randomNumber > chanceToFreeSpace)
            {
                GameObject newProp = Instantiate(props[Random.Range(0, props.Count)]);
                newProp.transform.position = field.transform.position;
                newProp.transform.SetParent(field.transform);
                field.SetCanEnter(false);
            }
        }

    }

    public override void UpdateSegment()
    {
        base.UpdateSegment();

    }

    public override void DeinitalizeSegment()
    {
        base.DeinitalizeSegment();

    }
}
