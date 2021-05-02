using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRSet : MonoBehaviour
{
    public SteamVR_Action_Boolean Teigger_Pressed; 
    public SteamVR_Action_Vector3 mMoveValue= null;
    public SteamVR_Behaviour_Pose m_Pose;

    // varaibles for storing if the touchpads have been used on the controller
    public SteamVR_Action_Vector3 m_TiggerhPos;
    public SteamVR_Action_Pose m_pose;

    private CharacterController m_CharacterController = null;
    private Transform m_CameraRig = null;
    private Transform m_Head = null;
    private Transform R_hand = null;




    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }
    void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
        

    }

    // Update is called once per frame
    void Update()
    {
        HandleHead();
        if (Teigger_Pressed.GetStateDown(m_Pose.inputSource)) {
            Control_hand();
        }
        
    }

    private void HandleHead()
    {
        Vector3 oldPosition = m_CameraRig.position;
        Quaternion oldRotation = m_CameraRig.rotation;

        transform.eulerAngles = new Vector3(0.0f,m_Head.rotation.eulerAngles.y,0.0f);

        m_CameraRig.position = oldPosition;
        m_CameraRig.rotation = oldRotation;

        Debug.Log(oldPosition);
    
    }
    private void Control_hand() 
    {
        Vector3 newPos = m_TiggerhPos[SteamVR_Input_Sources.LeftHand].delta;
        Debug.Log(newPos);
    }
}
