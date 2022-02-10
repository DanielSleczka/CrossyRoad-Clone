using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSegment : BaseSegment
{
    [SerializeField] private List<GameObject> carsList;
    [SerializeField] private List<GameObject> currentCars;

    [SerializeField] private Transform leftRespawn;
    [SerializeField] private Transform rightRespawn;
    private Transform respawnPoint;
    private Transform endPoint;

    private float carSpeed;
    private float timeToRespawn;

    public override void InitializeSegment()
    {
        base.InitializeSegment();
        ChooseRespawnSide();
        RespawnNewCar();
        carSpeed = Random.Range(1, 4);
        timeToRespawn = Random.Range(2, 5);
    }

    public void ChooseRespawnSide()
    {
        if (Random.Range(0, 101) < 50)
        {
            respawnPoint = rightRespawn;
            endPoint = leftRespawn;
        }
        else
        {
            respawnPoint = leftRespawn;
            endPoint = rightRespawn;
        }
    }
    public void RespawnNewCar()
    {
        GameObject newCar = Instantiate(carsList[Random.Range(0, carsList.Count)]);
        newCar.transform.position = respawnPoint.position;
        if (respawnPoint == rightRespawn)
        {
            newCar.transform.Rotate(0, -180, 0);
        }
        currentCars.Add(newCar);

        StartCoroutine(RespawnNewCarWithDelay(timeToRespawn));
    }

    public override void UpdateSegment()
    {
        base.UpdateSegment();
        UpdateMovingCar();
        CheckCarPosition();
    }

    public override void DeinitalizeSegment()
    {
        base.DeinitalizeSegment();
    }

    public void DestroyCar(GameObject car)
    {
        currentCars.Remove(car);
        Destroy(car.gameObject);

    }

    public void UpdateMovingCar()
    {
        foreach (var car in currentCars)
        {
            car.transform.Translate(Vector3.right * carSpeed * Time.deltaTime);
        }
    }

    public void CheckCarPosition()
    {
        foreach (var car in currentCars)
        {
            Debug.Log($"Car transform {car.transform.position}");
            Debug.Log($"End Point: {endPoint.transform.position}");
            if (car.transform.position == endPoint.transform.position)
            {
                Debug.Log($"Destroy {car.transform}");
                DestroyCar(car.gameObject);
            }
            
        }
    }

    private IEnumerator RespawnNewCarWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RespawnNewCar();
    }


    // Trzeba:
    // ustalic punkt koncowy samochodu, po czym go zniszczyc
}

