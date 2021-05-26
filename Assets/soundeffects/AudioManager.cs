using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {
	public static AudioManager instance;

	//public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	void Awake() {

		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			//s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	/// <summary>
	/// Plays the sound that is chosen.
	/// Method by: Jonas
	/// </summary>
	/// <param name="sound">Input the name of the sound</param>
	public void Play(string sound) {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume;
		s.source.pitch = s.pitch;

		s.source.Play();
	}

	/// <summary>
	/// Stops the sound that is chosen.
	/// Method by: Jonas
	/// </summary>
	/// <param name="sound">Input the name of the sound</param>
	public void Stop(string sound) {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.Stop();
	}

	/// <summary>
	/// Sets the volume.
	/// Method by; Jonas
	/// </summary>
	/// <param name="volume">Insert the volume as a float</param>
	public void SetVolume(float volume) {
		foreach (Sound s in sounds) {
			s.volume = volume;
			s.source.volume = volume;
		}
	}
}
