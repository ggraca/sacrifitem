using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    [SerializeField]
    private List<AudioClip> clips;


	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
        PlayBackground();
    }

    // Update is called once per frame

    private void PlayBackground()
    {
        MSManager.PlaySound("MusicPlayer", clips[0],true);
    }

    public void PlaySound(string itemName)
    {
        var clip = clips.Find(x => x.name.Equals(itemName));

        MSManager.PlaySound("SoundPlayer", clip);
    }

}
