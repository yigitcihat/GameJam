using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] GameObject ball = null;

    private float forceAmount = 5;

  
    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            RaycastHit hit;
            if (IsInput(TouchPhase.Began))
            {
                Ray _ray = Camera.main.ScreenPointToRay(Input.touchCount == 1 ? (Vector3)Input.GetTouch(0).position : Input.mousePosition);



                if (Physics.Raycast(_ray, out hit))
                {

                    Vector3 force = new Vector3(-hit.point.x + ball.transform.position.x,0, -hit.point.z + ball.transform.position.z);
                    force = GetFixedForce(force);
                    ball.GetComponent<Rigidbody>().AddForce(force * Time.deltaTime * forceAmount,ForceMode.Impulse );

                }
            }
            else if (IsInput(TouchPhase.Moved))
            {

            }
            else if (IsInput(TouchPhase.Ended))
            {

            }
        }


    }

    private Vector3 GetFixedForce(Vector3 force)
    {
        if (Mathf.Abs(force.z) > Mathf.Abs(force.x))
        {
            float ratio = force.x / force.z;
            if (force.z < 0)
            {
                force.x = -ratio;
                force.z = -1;
            }
            else
            {
                force.x = ratio;
                force.z = 1;
            }
            return force;
        }
        else
        {
            float ratio = force.z / force.x;
            if (force.x < 0)
            {
                force.z = -ratio;
                force.x = -1;
            }
            else
            {
                force.z = ratio;
                force.x = 1;
            }
            return force;
        }
    }
    private bool IsInput(TouchPhase phase)
    {
        switch (phase)
        {
            case TouchPhase.Began:
                return (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0);
            case TouchPhase.Moved:
                return (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0);
            default:
                return (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0);
        }
    }
}
