using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainLight : MonoBehaviour
{
    [SerializeField] private Light rightLight;
    [SerializeField] private Light leftLight;

    public void ActiveLights()
    {
        StartCoroutine(LightAnimation());
    }

    public void DeactiveLights()
    {
        StopAllCoroutines();
        IdleLights();
    }
    public void IdleLights()
    {
        rightLight.GetComponent<Light>().enabled = false;
        leftLight.GetComponent<Light>().enabled = false;
    }

    private IEnumerator LightAnimation()
    {
        rightLight.GetComponent<Light>().enabled = false;
        leftLight.GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(0.5f);

        rightLight.GetComponent<Light>().enabled = true;
        leftLight.GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(0.5f);

        ActiveLights();
    }
}
