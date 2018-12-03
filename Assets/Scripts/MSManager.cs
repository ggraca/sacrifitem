using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSManager : MonoBehaviour {

    public enum SearchType
    {
        Name,
        Tag
    };


    /// <summary>
    /// Returns the audio source component of the object with given name. 
    /// </summary>
    /// <param name="id">Name or tag of the music/sound player</param>
    /// <param name="st">Search type for the player. Search by name or tag</param>
    /// <returns>Returns audio source attached to object, if there is one. Null otherwise.</returns>
    public static AudioSource GetSoundPlayer(string id,SearchType st = SearchType.Name)
    {
        switch(st)
        {
            case SearchType.Name: return GameObject.Find(id).gameObject.GetComponent<AudioSource>();
            case SearchType.Tag:  return GameObject.FindGameObjectWithTag(id).gameObject.GetComponent<AudioSource>();
        }

        return GameObject.Find(id).gameObject.GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays the given clip on the given audio source.
    /// </summary>
    /// <param name="id">Music/sound player name or tag</param>
    /// <param name="sClip">Sound Clip</param>
    /// <param name="shouldRepeat">Should audio be in a loop, false by default</param>
    /// <param name="st">Search type for the player. Search by name or tag</param>
    public static void PlaySound(string id, AudioClip sClip,bool shouldRepeat = false ,SearchType st = SearchType.Name)
    {
        AudioSource source = GetSoundPlayer(id, st);

        if (!source) { Debug.Log("No music player has been found!");return;}

        source.loop = shouldRepeat;

        source.clip = sClip;
        source.Play();
    }

    /// <summary>
    /// Stop the clip on the given audio source.
    /// </summary>
    /// <param name="id">Music/sound player name or tag</param>
    /// <param name="st">Search type for the player. Search by name or tag</param>
    public static void StopSound(string id, SearchType st = SearchType.Name)
    {
        AudioSource source = GetSoundPlayer(id, st);

        if (!source) { Debug.Log("No music player has been found!"); return; }

        source.Stop();
    }

    /// <summary>
    /// Continues on the clip(if there is any) on the given audio source.
    /// </summary>
    /// <param name="id">Music/sound player name or tag</param>
    /// <param name="st">Search type for the player. Search by name or tag</param>
    public static void ContinueSound(string id, SearchType st = SearchType.Name)
    {
        AudioSource source = GetSoundPlayer(id, st);

        if (!source) { Debug.Log("No music player has been found!"); return; }

        if(source.clip)
        {
            source.Play();
        }
    }
    
}
