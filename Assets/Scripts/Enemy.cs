using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int hits;

	void Start () {
	
	}
	
	void Update () {
	
	}

    public void Damage() {
        hits -= 1;
        if(hits <= 0) {
            GameObject.Destroy(gameObject);
        }
        else if(hits == 1) {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.color = new Color(255, 155, 155);
        }
    }
}
