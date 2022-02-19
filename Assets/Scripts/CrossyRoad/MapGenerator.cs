using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<BaseSegment> segmentsPrefabs;

    private Vector3 startPosition;


    private void Start()
    {
        startPosition = new Vector3(0, 0, 5);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BaseSegment newSegment = Instantiate(segmentsPrefabs[Random.Range(0, segmentsPrefabs.Count)]);
            newSegment.transform.position = startPosition;
            startPosition.z++;
        }
    }

    public void GenerateStartMap(int segments)
    {

    }

    public void GenerateNewSegment()
    {

    }   
   
    public void RemoveLastSegment()
    {

    }

}
