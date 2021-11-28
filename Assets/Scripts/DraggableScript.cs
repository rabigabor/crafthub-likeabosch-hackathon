using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableScript : MonoBehaviour
{
    private Color originalColor;


    private void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }


    private float rotationSpeed = 5f;
    private void OnMouseDrag()
    {

        GetComponent<Renderer>().material.color = originalColor + Color.white * 1f;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.rotation = transform.rotation * Quaternion.AngleAxis(
                -rotationSpeed * (Input.GetAxis("Mouse X") + Input.GetAxis("Mouse Y")),
                 Vector3.up
            );
        }
        else
        {

            // transform.position += new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
            // transform.position.y += Input.GetAxis("Mouse Y"); 
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                var mesh = GetComponent<MeshFilter>().mesh;
                transform.position = hit.point + Vector3.down * mesh.bounds.min.y;


            }
        }
    }
    private void OnMouseUp()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }



    private void OnCollisionStay(Collision other)
    {
        // Debug.Log(other.transform.gameObject.tag);
        // if (other.transform.gameObject.tag == "Car")
        // {

        //     GetComponent<Renderer>().material.color = originalColor + Color.red * 1f;
        // }
    }
}
