using UnityEngine;
using System.Collections;

public class DebugScript : MonoBehaviour {

	bool debug = true;

	static string text = "";

	void OnGUI () {
		if (debug) {
			this.GetComponent<GUIText>().text = text;
		} else {
			this.GetComponent<GUIText>().text = "";
		}
	}

	static public void AddText(string str) {
		if (text.Length != 0) {
			text += "\n";
		}
		text += str;
	}

	static public void Clear () {
		text = "";
	}
}
