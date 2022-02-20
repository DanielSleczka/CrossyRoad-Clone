using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSegment : BaseSegment
{
    [SerializeField] [Range(0f, 1f)] private float randomChance;
    [SerializeField] private List<GameObject> propsList;


    public override void InitializeSegment()
    {
        base.InitializeSegment();
        foreach (var field in fields)
        {
            int randomNumber = Random.Range(0, 101);
            if (100 * randomChance < randomNumber)
            {
                GameObject newProp = Instantiate(propsList[Random.Range(0, propsList.Count)]);
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

    public void SetRandomChance(float value)
    {
        randomChance = value;
    }

    public override void DeinitalizeSegment()
    {
        base.DeinitalizeSegment();

    }
}
