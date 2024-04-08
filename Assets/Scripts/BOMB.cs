using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOMB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DIEDIEDIE");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DIEDIEDIE()
    {
        yield return new WaitForSecondsRealtime(3);
        Destroy(gameObject);
    }
}
