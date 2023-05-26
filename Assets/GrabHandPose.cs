using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabHandPose : MonoBehaviour
{

    public HandData rightHandPose;

    private Vector3 startingHandPosition;
    private Vector3 finalHandPosition;
    private Quaternion startingHandRotation;
    private Quaternion finalHandRotation;

    private Quaternion[] startingfingerRotations;
    private Quaternion[] finalfingerRotations;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabinteractable = GetComponent<XRGrabInteractable>();

        grabinteractable.selectEntered.AddListener(SetupPose);
        rightHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactableObject is XRDirectInteractor)
        {
            HandData handData = arg.interactableObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = false;

            SetHandDataValues(handData, rightHandPose);
            SetHandData(handData, finalHandPosition, finalHandRotation, finalfingerRotations);
        }
    }

    public void UnsetPose(BaseInteractionEventArgs arg)
    {
        if(arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = true;

            SetHandDataValues(handData, rightHandPose);
            SetHandData(handData, finalHandPosition, finalHandRotation, finalfingerRotations);
        }
    }

    public void SetHandDataValues(HandData h1, HandData h2)
    {
        startingHandPosition = h1.root.localPosition;
        finalHandPosition = h2.root.localPosition;

        startingHandRotation = h1.root.localRotation;
        finalHandRotation = h2.root.localRotation;

        startingfingerRotations = new Quaternion[h1.fingerbones.Length];
        finalfingerRotations = new Quaternion[h1.fingerbones.Length];

        for (int i=0; i<h1.fingerbones.Length; i++)
        {
            startingfingerRotations[i] = h1.fingerbones[i].localRotation;
            finalfingerRotations[i] = h2.fingerbones[i].localRotation;
        }
    }

    public void SetHandData(HandData h, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation)
    {
        h.root.localPosition = newPosition;
        h.root.localRotation = newRotation;

        for (int i = 0; i < newBonesRotation.Length; i++)
        {
            h.fingerbones[i].localRotation = newBonesRotation[i];
        }
    }

}