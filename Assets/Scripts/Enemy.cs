using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Material col;
    public UtilityManager util;
    public Material skybox;
    public AudioClip spawnSound;
    // Start is called before the first frame update
    void Start()
    {
        util = FindAnyObjectByType<UtilityManager>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<SkinnedMeshRenderer>().material = col;
        gameObject.GetComponent<MeshRenderer>().material = skybox;
        AudioSource.PlayClipAtPoint(spawnSound, transform.position);
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
            util.gameOver();
        }
    }
}
