using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotat : MonoBehaviour {

    float y;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(transform.up, Time.deltaTime*25f);
	}
}
