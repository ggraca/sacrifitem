using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOnClick : MonoBehaviour
{
	public void Quit ()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit ();
#endif
	}

	public void Restart() {
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Menu() {
		Application.LoadLevel(0);
	}
}
