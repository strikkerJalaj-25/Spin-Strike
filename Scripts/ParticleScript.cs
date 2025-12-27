using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        GetComponent<ParticleSystem>().Play();
        GetComponent<MeshRenderer>().enabled = false;
        //gameObject.SetActive(false);
    }
}
