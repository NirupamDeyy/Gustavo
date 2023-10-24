using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collderScript : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cells"))
        {
            Debug.Log("ess");
        }
    }

    private void Update()
    {
        transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y, .2f * Time.deltaTime);
    }
}
