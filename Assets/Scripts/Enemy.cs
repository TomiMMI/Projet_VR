using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Material col;
    public Material skybox;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<SkinnedMeshRenderer>().material = col;
        gameObject.GetComponent<MeshRenderer>().material = skybox;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        GameManager.remaining -= 1;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
