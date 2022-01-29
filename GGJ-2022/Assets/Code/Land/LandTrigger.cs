using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LandTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        VehicleController vehController = collision.gameObject.GetComponent<VehicleController>();
        if (vehController != null && vehController._hasControl)
        {
            LandController landController = collision.gameObject.GetComponent<LandController>();
            landController.TakeControl();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        LandController landController = collision.gameObject.GetComponent<LandController>();
        if (landController != null && landController._hasControl)
        {
            VehicleController vehController = collision.gameObject.GetComponent<VehicleController>();
            vehController.TakeControl();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(landController._movement.normalized * 3.5f, ForceMode2D.Impulse);
        }
    }
}
