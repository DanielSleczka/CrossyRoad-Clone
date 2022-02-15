using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSegment : BaseSegment
{
    [SerializeField] private List<GameObject> trainList;
    [SerializeField] private List<GameObject> currentTrain;

    [SerializeField] private float trainSpeed;
    [SerializeField] private float respawnTime;

    [SerializeField] private Transform rightRespawn;
    [SerializeField] private Transform leftRespawn;
    private Transform startPoint;
    private Transform endPoint;

    public override void InitializeSegment()
    {
        base.InitializeSegment();
        ChooseRespawnSide();
        trainSpeed = Random.Range(5, 7);
        respawnTime = Random.Range(7, 10);
        StartCoroutine(RespawnNewTrainWithDelay(respawnTime));
    }
    public void ChooseRespawnSide()
    {
        if (Random.Range(0, 101) < 50)
        {
            startPoint = rightRespawn;
            endPoint = leftRespawn;
        }
        else
        {
            startPoint = leftRespawn;
            endPoint = rightRespawn;
        }
    }
    private void RespawnNewTrain()
    {
        GameObject newTrain = Instantiate(trainList[Random.Range(0, trainList.Count)]);
        newTrain.transform.position = startPoint.position;
        if (startPoint == leftRespawn)
        {
            newTrain.transform.Rotate(0, 180, 0);
        }
        currentTrain.Add(newTrain);

        StartCoroutine(RespawnNewTrainWithDelay(respawnTime));

    }
    public override void UpdateSegment()
    {
        base.UpdateSegment();
        UpdateMovingTrain();
    }

    public void UpdateMovingTrain()
    {
        foreach (var train in currentTrain)
        {
            // Start moving train
            train.transform.Translate(Vector3.left * trainSpeed * Time.deltaTime);

            // Check that the train is at the ending point
            if (train.transform.position.x >= endPoint.transform.position.x && startPoint == leftRespawn)
            {
                DestroyCar(train.gameObject);
            }
            else if (train.transform.position.x <= endPoint.transform.position.x && startPoint == rightRespawn)
            {
                DestroyCar(train.gameObject);
            }
        }
    }
    public void DestroyCar(GameObject train)
    {
        currentTrain.Remove(train);
        Destroy(train.gameObject);
    }

    public override void DeinitalizeSegment()
    {
        base.DeinitalizeSegment();
    }

    private IEnumerator RespawnNewTrainWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RespawnNewTrain();
    }

}

