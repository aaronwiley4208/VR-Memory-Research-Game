using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RayScript : MonoBehaviour
{
    public SteamVR_Action_Boolean grabPinch;

    void Update() {

        if (grabPinch.GetState(SteamVr_Input_Sources.Any)) {

        }
    }
    
}
