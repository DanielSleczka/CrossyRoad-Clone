using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<BaseSegment> segmentsList;
    [SerializeField] private List<BaseSegment> currentSegments;
    [SerializeField] private RestSegment restSegment;

    private Vector3 startPosition;

    public int currentSegment;
    public int currentField;


    private int indexValue = 0;

    public void TryMove(int segmentDiff, int fieldDiff, UnityAction<Vector3> OnMovenmentSuccess, UnityAction OnMovementFailed)
    {
        BaseSegment segment = currentSegments.Find(c => c.SegmentID == currentSegment + segmentDiff);

        if (segment != null)
        {
            if (segment.CheckFieldEnterable(currentField + fieldDiff, out Vector3 position))
            {
                currentSegment += segmentDiff;
                currentField += fieldDiff;
                OnMovenmentSuccess?.Invoke(position);
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
        GenerateNewSegment();
        startPosition = new Vector3(0, 0, indexValue);
    }

    public void GenerateStartMap()
    {
        for(int i = indexValue; i < 2; i++)
        {
            RestSegment newRestSegment = Instantiate(restSegment);
            newRestSegment.transform.position = new Vector3(0, 0, i);
            newRestSegment.SetID(i);
            newRestSegment.SetRandomChance(1f);
            currentSegments.Add(newRestSegment);
            indexValue++;
        }

        for (int i = indexValue; i < 8; i++)
        {
            BaseSegment newSegment = Instantiate(segmentsList[Random.Range(0, segmentsList.Count)]);
            newSegment.transform.position = new Vector3(0, 0, i);
            newSegment.SetID(i);
            currentSegments.Add(newSegment);
            indexValue++;
        }
    }

    public void GenerateNewSegment()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BaseSegment newSegment = Instantiate(segmentsList[Random.Range(0, segmentsList.Count)]);
            newSegment.transform.position = startPosition;
            newSegment.SetID(indexValue);
            currentSegments.Add(newSegment);
            indexValue++;
            startPosition.z++;
        }
    }   
   
    public void RemoveLastSegment()
    {

    }


    private void OnApplicationQuit()
    {
        
    }
}
