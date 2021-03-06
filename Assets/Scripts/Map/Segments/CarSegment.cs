using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSegment : BaseSegment
{
    [SerializeField] private Transform leftRespawn;
    [SerializeField] private Transform rightRespawn;
    private Transform startPoint;
    private Transform endPoint;

    [SerializeField] private float minCarSpeed;
    [SerializeField] private float maxCarSpeed;
    private float carSpeed;

 
    [SerializeField] private float minTimeToRespawn;
    [SerializeField] private float maxTimeToRespawn;
    private float timeToRespawn;

    [SerializeField] private List<GameObject> carsList;
    [SerializeField] private List<GameObject> currentCars;

    public override void InitializeSegment()
    {
        base.InitializeSegment();
        SetNotEnterableFields();
        ChooseRespawnSide();
        carSpeed = Random.Range(minCarSpeed, maxCarSpeed);
        StartCoroutine(RespawnNewCarWithDelay(0f));
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
    public void RespawnNewCar()
    {
        GameObject newCar = Instantiate(carsList[Random.Range(0, carsList.Count)]);
        newCar.transform.SetParent(transform);
        newCar.transform.position = startPoint.position;
        if (startPoint == rightRespawn)
        {
            newCar.transform.Rotate(0, 180, 0);
        }
        currentCars.Add(newCar);
        SetupRespawnTime();
        StartCoroutine(RespawnNewCarWithDelay(timeToRespawn));
    }

    public override void UpdateSegment()
    {
        base.UpdateSegment();
        UpdateMovingCar();
    }

    public override void DeinitalizeSegment()
    {
        base.DeinitalizeSegment();
    }

    public void UpdateMovingCar()
    {
        for (int i = currentCars.Count - 1; i >= 0; i--)
        {
            // Start moving car
            currentCars[i].transform.Translate(Vector3.right * carSpeed * Time.deltaTime);

            // Check that the car is at the ending point
            if (currentCars[i].transform.position.x >= endPoint.transform.position.x && startPoint == leftRespawn)
            {
                DestroyCar(currentCars[i].gameObject);
            }
            else if (currentCars[i].transform.position.x <= endPoint.transform.position.x && startPoint == rightRespawn)
            {
                DestroyCar(currentCars[i].gameObject);
            }
        }
    }

    private void SetupRespawnTime()
    {
        timeToRespawn = Random.Range(minTimeToRespawn, maxTimeToRespawn);
    }

    public void DestroyCar(GameObject car)
    {
        currentCars.Remove(car);
        Destroy(car.gameObject);
    }

    private IEnumerator RespawnNewCarWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RespawnNewCar();
    }
}

