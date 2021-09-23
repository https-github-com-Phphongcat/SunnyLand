using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRepeating : MonoBehaviour
{
    public bool EnableShow;
    private bool IsShowSpike;
    [SerializeField] private float TimeDelayShowSpike;
    [SerializeField] private GameObject SpikeObjects;

    void Awake()
    {
        EnableShow = true;
        IsShowSpike = true;
    }

    void RepeatingShowSpike()
    {
        if(IsShowSpike){
            StartCoroutine(DelaySpike(SpikeObjects));
        }else{
            StartCoroutine(DelaySpike());
        }
    }

    IEnumerator DelaySpike()
    {
        yield return new WaitForSeconds(TimeDelayShowSpike);
        IsShowSpike = true;
    }

    IEnumerator DelaySpike(GameObject Spike)
    {
        Spike.SetActive(true);
        yield return new WaitForSeconds(TimeDelayShowSpike);
        Spike.SetActive(false);
        IsShowSpike = false;
    }

    void Update()
    {
        if(EnableShow){
            RepeatingShowSpike();
        }else{
            SpikeObjects.SetActive(false);
            IsShowSpike = false;
        }
    }
}
