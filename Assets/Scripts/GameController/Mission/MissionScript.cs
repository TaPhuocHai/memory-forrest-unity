using UnityEngine;
using System.Collections;

public class MissionScript : MonoBehaviour {

	private Mission _mission;

	public PropertyReference labelName;
	public PropertyReference labelDescription;

	public Mission mission {
		get { return _mission;}
		set {
			_mission = value;
			this.labelName.Set(value.name);
			this.labelDescription.Set(value.description);
		}
	}

	void Start () {
	
	}

	void Update () {
	
	}
}
