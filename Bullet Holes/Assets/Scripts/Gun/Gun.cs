using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [Header("Positions and layers")]
    public Transform fpsCam;
    public RaycastHit rayHit;
    public LayerMask whatIsWall;

    [Header("Effects")]
    public GameObject bulletHole;

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    private void MyInput()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();
    }

    private void Shoot()
    {
        //Direction
        Vector3 direction = fpsCam.transform.forward;

        direction.Normalize();

        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, 1000f, whatIsWall))
        {

            //Instantiate bullet hole
            GameObject hole = Instantiate(bulletHole, rayHit.point, Quaternion.identity);
            hole.transform.rotation = Quaternion.LookRotation(-rayHit.normal, Vector3.up);
            hole.transform.position -= hole.transform.forward * 0.01f;

            //Set the color to the hit object color
            if (!rayHit.transform.GetComponent<MeshRenderer>().material.mainTexture)
                hole.GetComponent<SpriteRenderer>().material.SetColor("_NewColor", rayHit.transform.GetComponent<MeshRenderer>().material.color);
        }

    }

}