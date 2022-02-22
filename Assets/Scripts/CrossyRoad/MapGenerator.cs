using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<BaseSegment> segmentsList;
    [SerializeField] private RestSegment restSegment;
    [SerializeField] private List<BaseSegment> currentSegmentsList;

    [SerializeField] [Min(20)] private float maxRangeOfExistSegments;

    private Vector3 newSegmentPosition;
    private int segmentIndexValue = 0;

    public int currentSegment;
    public int currentField;


    public void TryMove(int segmentDiff, int fieldDiff, UnityAction<Vector3> OnMovenmentSuccess, UnityAction OnMovementFailed)
    {
        BaseSegment segment = currentSegmentsList.Find(c => c.SegmentID == currentSegment + segmentDiff);

        if (segment != null)
        {
            if (segment.CheckFieldEnterable(currentField + fieldDiff, out Vector3 position))
            {
                currentSegment += segmentDiff;
                currentField += fieldDiff;
                OnMovenmentSuccess?.Invoke(position);
                if (segmentDiff == 1)
                    GenerateNewSegment();
            }
            else
                OnMovementFailed?.Invoke();
        }
    }
    
    private void Start()
    {
        GenerateStartMap();
    }

    private void Update()
    {
        if (currentSegmentsList.Count > maxRangeOfExistSegments)
        {
            RemoveLastSegment();
        }
    }

    public void GenerateStartMap()
    {
        for(int i = segmentIndexValue; i < 2; i++)
        {
            RestSegment newRestSegment = Instantiate(restSegment);
            newRestSegment.transform.position = new Vector3(0, 0, i);
            newRestSegment.SetID(i);
            newRestSegment.SetRandomChance(1f);
            currentSegmentsList.Add(newRestSegment);
            segmentIndexValue++;
        }

        for (int i = segmentIndexValue; i <= 15; i++)
        {
            BaseSegment newSegment = Instantiate(segmentsList[Random.Range(0, segmentsList.Count)]);
            newSegment.transform.position = new Vector3(0, 0, i);
            newSegment.SetID(i);
            currentSegmentsList.Add(newSegment);
            segmentIndexValue++;
        }
    }

    public void GenerateNewSegment()
    {
        newSegmentPosition = new Vector3(0, 0, segmentIndexValue);
        BaseSegment newSegment = Instantiate(segmentsList[Random.Range(0, segmentsList.Count)]);
        newSegment.transform.position = newSegmentPosition;
        newSegment.SetID(segmentIndexValue);
        currentSegmentsList.Add(newSegment);
        segmentIndexValue++;
    }   
   
    public void RemoveLastSegment()
    {
        currentSegmentsList.Remove(currentSegmentsList[0]);
        Destroy(currentSegmentsList[0].gameObject);
    }

    private void OnApplicationQuit()
    {
        
    }
}
