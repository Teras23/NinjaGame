using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {

	void Start () {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Entity"), LayerMask.NameToLayer("Entity"), true);
        /*Vector2 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector2 bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        
        Debug.Log(topLeft);
        Debug.Log(topRight);
        Debug.Log(bottomLeft);
        Debug.Log(bottomRight);*/
	}
	
	void Update () {
	
	}
}
