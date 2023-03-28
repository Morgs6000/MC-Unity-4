using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {    
    private void Start() {
        StartCoroutine(Transition());
    }

    private void Update() {
        
    }

    private IEnumerator Transition() {
        yield return new WaitForSeconds(3.0f);

        this.gameObject.SetActive(false);
    }
}
