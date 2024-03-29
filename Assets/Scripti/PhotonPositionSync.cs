using UnityEngine;
using System.Collections;

//Copyright 2013 Tech Drone and William H Hendrickson

/// <summary>
/// Photon Position Sync:
/// To be used with Photon Unity Netowrking
/// This class will synchronize objects' positions over the network when enabled & interPosition == true
/// Supports all objects via interpolation and rigidbodies via optional extrapolation.
/// </summary>
/// 
[RequireComponent (typeof (PhotonView))]
public class PhotonPositionSync : Photon.MonoBehaviour {
	
	//used to instantiate things conviniently via Photon
	[HideInInspector]
	public GameObject spawnPoint;
	[HideInInspector]
	public GameObject spawningObject;
	
	// Synchronize the position of the object automatically using interpolation.
	public bool interPosition = false;
	public enum InterPositionMode {InterpolateTransformation, ExtrapolateRigidbodyPhysics};
	public InterPositionMode syncMode = InterPositionMode.ExtrapolateRigidbodyPhysics;
	public float interpolationBias = 5.0f;
	public Vector3 correctPosition = Vector3.zero;
	public Quaternion correctRotation = Quaternion.identity;
	
	//--Extrapolation data
	public double m_InterpolationBackTime = 0.1;//0.05;
	public double m_ExtrapolationLimit = 0.5;
	internal struct State
	{
		internal double timestamp;
		internal Vector3 pos;
		internal Vector3 velocity;
		internal Quaternion rot;
		internal Vector3 angularVelocity;
	}

	// We store twenty states with "playback" information
	State[] m_BufferedState = new State[20];
	// Keep track of what slots are used
	int m_TimestampCount;
	
	
	[HideInInspector]
	public PhotonView photonView;
	
	void Start () {
		photonView = PhotonView.Get(this);//GetComponent<PhotonView>();
	}
	
	void Update () {
		if (interPosition && !photonView.isMine) {
			if (syncMode == InterPositionMode.InterpolateTransformation || GetComponent<Rigidbody>() == null) {
				transform.position = Vector3.Lerp(transform.position, correctPosition, Time.deltaTime * interpolationBias);
				transform.rotation = Quaternion.Lerp(transform.rotation, correctRotation, Time.deltaTime * interpolationBias);
			}
			else
				UpdateRigidbody();
		}
	}
	
	void UpdateRigidbody () {
		// This is the target playback time of the rigid body
		double interpolationTime = Network.time - m_InterpolationBackTime;

		// Smoothing
		// Use interpolation if the target playback time is present in the buffer
		if (m_BufferedState[0].timestamp > interpolationTime)
		{
			// Go through buffer and find correct state to play back
			for (int i=0;i<m_TimestampCount;i++)
			{
				if (m_BufferedState[i].timestamp <= interpolationTime || i ==
					m_TimestampCount-1)
				{
					// The state one slot newer (<100ms) than the best playback state
					State rhs = m_BufferedState[Mathf.Max(i-1, 0)];
					// The best playback state (closest to 100 ms old (default time))
					State lhs = m_BufferedState[i];
					// Use the time between the two slots to determine if interpolation is necessary
					double length = rhs.timestamp - lhs.timestamp;
					float t = 0.0F;
					// As the time difference gets closer to 100 ms t gets closer to 1 in
					// which case rhs is only used
					// Example:
					// Time is 10.000, so sampleTime is 9.900
					// lhs.time is 9.910 rhs.time is 9.980 length is 0.070
					// t is 9.900 - 9.910 / 0.070 = 0.14. So it uses 14% of rhs, 86% of lhs
					if (length > 0.0001)
						t = (float)((interpolationTime - lhs.timestamp) / length);
					// if t=0 => lhs is used directly
					transform.localPosition = Vector3.Lerp(lhs.pos, rhs.pos, t);
					transform.localRotation = Quaternion.Slerp(lhs.rot, rhs.rot, t);
					return;
				}
			}
		}
		// Use extrapolation (Prediction)
		else
		{
			State latest = m_BufferedState[0];
			float extrapolationLength = (float)(interpolationTime - latest.timestamp);
			// Don't extrapolation for more than 500 ms, you would need to do that carefully
			if (extrapolationLength < m_ExtrapolationLimit )
			{
				float axisLength = extrapolationLength * latest.angularVelocity.magnitude
								* Mathf.Rad2Deg;
				Quaternion angularRotation = Quaternion.AngleAxis(axisLength, latest.angularVelocity);
				GetComponent<Rigidbody>().position = latest.pos + latest.velocity * extrapolationLength;
				GetComponent<Rigidbody>().rotation = angularRotation * latest.rot;
				GetComponent<Rigidbody>().velocity = latest.velocity;
				GetComponent<Rigidbody>().angularVelocity = latest.angularVelocity;
			}
		}	
	}
	
	
	void UpdateTransformMetaData(PhotonStream stream, PhotonMessageInfo info) {
		if(stream.isWriting) {
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else {
			correctPosition = (Vector3)stream.ReceiveNext();
			correctRotation = (Quaternion)stream.ReceiveNext();
		}
	}
	
	void ExtrapolateRigidbodyPosition (PhotonStream stream, PhotonMessageInfo info) {
		// Send data out
		
		if (stream.isWriting)
		{
			Vector3 pos = GetComponent<Rigidbody>().position;
			Quaternion rot = GetComponent<Rigidbody>().rotation;
			Vector3 velocity = GetComponent<Rigidbody>().velocity;
			Vector3 angularVelocity = GetComponent<Rigidbody>().angularVelocity;
			stream.Serialize(ref pos);
			stream.Serialize(ref velocity);
			stream.Serialize(ref rot);
			stream.Serialize(ref angularVelocity);
		}
		//Read data innt
		else
		{
			Vector3 pos = Vector3.zero;
			Vector3 velocity = Vector3.zero;
			Quaternion rot = Quaternion.identity;
			Vector3 angularVelocity = Vector3.zero;
			stream.Serialize(ref pos);
			stream.Serialize(ref velocity);
			stream.Serialize(ref rot);
			stream.Serialize(ref angularVelocity);
			// Shift the buffer sideways, deleting state 20
			for (int i=m_BufferedState.Length-1;i>=1;i--)
			{
				m_BufferedState[i] = m_BufferedState[i-1];
			}
			// Record current state in slot 0
			State state;
			state.timestamp = info.timestamp;
			state.pos = pos;
			state.velocity = velocity;
			state.rot = rot;
			state.angularVelocity = angularVelocity;
			m_BufferedState[0] = state;
			// Update used slot count, however never exceed the buffer size
			// Slots aren't actually freed so this just makes sure the buffer is
			// filled up and that uninitalized slots aren't used.
			m_TimestampCount = Mathf.Min(m_TimestampCount + 1,
			m_BufferedState.Length);
			// Check if states are in order, if it is inconsistent you could reshuffel or
			// drop the out-of-order state. Nothing is done here
			for (int i=0;i<m_TimestampCount-1;i++)
			{
				if (m_BufferedState[i].timestamp < m_BufferedState[i+1].timestamp)
				Debug.Log("State inconsistent");
			}
		}

	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
			//Debug.Log("View Serialized");
		
		if (interPosition) {
			if(syncMode == InterPositionMode.ExtrapolateRigidbodyPhysics)
				ExtrapolateRigidbodyPosition(stream, info);
			else
				UpdateTransformMetaData(stream, info);
		}
	}
	
	protected bool CheckIsConnected() {
		return PhotonNetwork.connected;
	}
	
	protected bool CheckIsInRoom() {
		bool ret = false;
		if (PhotonNetwork.room != null)
			ret = true;
		return ret;
	}

	//Use PlayMaker's SetProperty and Invoke actions to use these with Playmaker
#region Invokables
	public GameObject SpawnObject () {
		return PhotonNetwork.Instantiate(spawningObject.name, spawnPoint.transform.position, spawnPoint.transform.rotation, 0);
	}
	

	
#endregion
}
