using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public HitPoints theHP;

    public Renderer cpRender;

    public Material cpOff;
    public Material cpOn;

    // Start is called before the first frame update
    void Start()
    {
        theHP = FindObjectOfType<HitPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CPTriggerOn()
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint cp in checkpoints)
        {
            cp.CPTriggerOff();
        }

        cpRender.material = cpOn;

    }
    public void CPTriggerOff()
    {
        cpRender.material = cpOff;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            theHP.SetSpawnPoint(transform.position);
            CPTriggerOn();
        }

    }

}
