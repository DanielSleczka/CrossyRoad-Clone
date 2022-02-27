using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSegment : BaseSegment
{
    [SerializeField] private int freeField;
    [SerializeField] [Range(0f, 1f)] private float randomChance;
    [SerializeField] private List<GameObject> propsList;

    public override void InitializeSegment()
    {
        base.InitializeSegment();
        for (int i = 0; i < fields.Count; i++)
        {
            if (i < 5 || i > 13)
            {
                GameObject newTree = Instantiate(propsList[7]);
                newTree.transform.position = fields[i].transform.position;
                newTree.transform.SetParent(fields[i].transform);
                fields[i].SetCanEnter(false);
            }

            else if (i == freeField)
                continue;

            else if (i > 5 || i < 13)
            {
                int randomNumber = Random.Range(0, 101);
                if (100 * randomChance < randomNumber)
                {
                    GameObject newProp = Instantiate(propsList[Random.Range(0, propsList.Count)]);
                    newProp.transform.position = fields[i].transform.position;
                    newProp.transform.SetParent(fields[i].transform);
                    fields[i].SetCanEnter(false);
                }
            }
        }
    }

    public override void UpdateSegment()
    {
        base.UpdateSegment();
    }
    public void SetAlwaysEnterableFieldID(int value)
    {
        freeField = value;
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
