using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainLight : MonoBehaviour
{

    [SerializeField] private List<Material> materials;

    [SerializeField] private MeshRenderer rightLight;
    [SerializeField] private MeshRenderer leftLight;


    private void Start()
    {
        IdleLights();
    }

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
        rightLight.material = materials[0];
        leftLight.material = materials[0];
    }

    private IEnumerator LightAnimation()
    {
        rightLight.material = materials[0];
        leftLight.material = materials[1];
        yield return new WaitForSeconds(0.5f);
        rightLight.material = materials[1];
        leftLight.material = materials[0];
        yield return new WaitForSeconds(0.5f);
        ActiveLights();
    }
}
