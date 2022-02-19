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

    private bool isMoving;
    private bool notMoving;

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
        isMoving = true;
        GameObject newTrain = Instantiate(trainList[Random.Range(0, trainList.Count)]);
        newTrain.transform.position = startPoint.position;
        if (startPoint == leftRespawn)
        {
            newTrain.transform.Rotate(0, 180, 0);
        }
        currentTrain.Add(newTrain);
    }
    private IEnumerator RespawnNewTrainWithDelay(float delay)
    {
        notMoving = false;
        yield return new WaitForSeconds(delay);
        RespawnNewTrain();
    }

    public override void UpdateSegment()
    {
        base.UpdateSegment();
        UpdateMovingTrain();
        if (notMoving)
        {
            StartCoroutine(RespawnNewTrainWithDelay(respawnTime));
        }
    }

    public void UpdateMovingTrain()
    {
        for (int i = currentTrain.Count - 1; i >= 0; i--)
        {
            // Start moving train
            currentTrain[i].transform.Translate(Vector3.left * trainSpeed * Time.deltaTime);

            // Check that the train is at the ending point
            if (currentTrain[i].transform.position.x >= endPoint.transform.position.x && startPoint == leftRespawn)
            {
                DestroyTrain(currentTrain[i].gameObject);
            }
            else if (currentTrain[i].transform.position.x <= endPoint.transform.position.x && startPoint == rightRespawn)
            {
                DestroyTrain(currentTrain[i].gameObject);
            }
        }
    }
    public void DestroyTrain(GameObject train)
    {
        isMoving = false;
        notMoving = true;
        currentTrain.Remove(train);
        Destroy(train.gameObject);
    }

    public override void DeinitalizeSegment()
    {
        base.DeinitalizeSegment();
    }



}
