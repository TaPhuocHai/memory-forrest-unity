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
			this.labelName.Set(value.name.text);
			this.labelDescription.Set(value.description.text);
		}
	}

	void Start () {
	
	}

	void Update () {
	
	}
}
