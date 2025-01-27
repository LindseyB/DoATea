﻿using UnityEngine;
using System.Collections;

public class Blackhole : MonoBehaviour {
	bool moving = false;

	void Update () {
		if(GameState.Blackhole) {
			GameObject directionalLight = GameObject.Find("Directional Light");
			GameObject.Find("Music").GetComponent<AudioSource>().enabled = false;

			ParticleSystem ps = directionalLight.GetComponentInChildren<ParticleSystem>();

			directionalLight.GetComponentInChildren<AudioSource>().enabled = true;
			ps.Play();
			ps.transform.GetChild(0).gameObject.SetActive(true);

			Rigidbody rb;
			if(rb = gameObject.GetComponent<Rigidbody>()){
				rb.useGravity = false;
				rb.detectCollisions = false;
			}

			StartCoroutine(GravitySuck(directionalLight.transform.position, Random.Range(50.0f, 100f)));
			StartCoroutine(EndBlackHole(10f, ps,directionalLight.GetComponentInChildren<AudioSource>()));
		}
	}

	IEnumerator GravitySuck(Vector3 targetPosition, float time){
		if (!moving){
			moving = true;
			float t = 0f;
			while (t < 1f){
				t += Time.deltaTime / time;
				transform.position = Vector3.Lerp(transform.position, targetPosition, t); 
				transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, t);
				yield return 0;
			}
			moving = false;
		}
	}

	IEnumerator EndBlackHole(float waitTime, ParticleSystem ps, AudioSource audio) {
		yield return new WaitForSeconds(waitTime);
		ps.Stop();
		audio.Stop();
	}
}
