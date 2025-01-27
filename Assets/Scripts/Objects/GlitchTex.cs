﻿using UnityEngine;
using System.Collections;

public class GlitchTex : MonoBehaviour {
	public MovieTexture glitch;

	void Start() {
		// For some reason I need to clamp before repeating - unity bug?
		glitch.wrapMode = TextureWrapMode.Clamp;
		gameObject.GetComponent<Renderer>().material.SetTexture("_Detail", glitch);
		glitch.loop = true;
		glitch.Play();
		glitch.wrapMode = TextureWrapMode.Repeat;
	}

	void Update() {
		if(!GameState.Glitch) { glitch.Stop(); }
		if(GameState.Glitch && !glitch.isPlaying) { glitch.Play(); }
	}
}
