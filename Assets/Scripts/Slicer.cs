using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Slicer : MonoBehaviour
{
    public Transform basLame;
    public Transform hautLame;
    public float cutForce = 500.0f;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;
    public Material materialInterieur;
    public AudioClip sliceSound;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(hautLame.position, basLame.position, out RaycastHit hit, sliceableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            target.GetComponent<MeshRenderer>().material = target.GetComponent<SkinnedMeshRenderer>().material;
            target.GetComponent<SkinnedMeshRenderer>().BakeMesh(target.GetComponent<MeshFilter>().mesh, true);
            Slice(target);
        }
    }
    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Debug.Log(velocity);
        Vector3 planeNormal = Vector3.Cross(hautLame.position - basLame.position, velocity);
        planeNormal.Normalize();
        SlicedHull hull = target.Slice(hautLame.position, planeNormal);

        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target,materialInterieur);
            SetupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target,materialInterieur);
            SetupSlicedComponent(lowerHull);
            AudioSource.PlayClipAtPoint(sliceSound, target.transform.position);

            if (target.CompareTag("Enemy"))
            {
                FindAnyObjectByType<UtilityManager>().score += 1;
                FindAnyObjectByType<UtilityManager>().actualizeScore();
            }
            Destroy(target);
        }
    }
    public void SetupSlicedComponent(GameObject slicedObject)
    {
       slicedObject.AddComponent<BOMB>();
       Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
       MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
       collider.convex = true;
       rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
