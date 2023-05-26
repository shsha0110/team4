using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomXRGrabInteractable : XRGrabInteractable
{
    public GameObject suctionCupPlane; // the plane to stick to
    private bool collidingWithPlane = false;
    private bool isPressed = false;
    private InputDevice device;
    Vector3 _staticPosition;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isPressed = false;
    }

    private void Start()
    {
        // Find the Suction Cup Interaction object by tag
        suctionCupPlane = GameObject.FindGameObjectWithTag("suctionCupPlane");

        // Get the first available controller
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        if (devices.Count > 0)
        {
            device = devices[0];
        }
    }

    private void Update()
    {
        
        if (device.isValid)
        {
            device.TryGetFeatureValue(CommonUsages.triggerButton, out isPressed);
            if (isSelected&&collidingWithPlane && isPressed)  // if (isSelected && collidingWithPlane && isPressed)
            {
                this.transform.position = _staticPosition;
                isPressed = false; // optional: release the button after sticking
            }
        }
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == suctionCupPlane)
        {
            collidingWithPlane = true;
            _staticPosition=this.gameObject.transform.position;

        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == suctionCupPlane)
        {
            collidingWithPlane = false;
        }
    }
    
}
