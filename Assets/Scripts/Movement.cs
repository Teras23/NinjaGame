using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float moveSpeed = 1;
    public float noMove = 0.5f;
    public float jumpDistance = 2.5f;
    public float jumpSpeed = 5;
    public float hitDistance = 0.2f;
    private bool jumping;
    private Vector2 jumpPosition;
    private float jumpTime;

    private Vector2 position;
    private Vector2 mousePosition;
    private Rigidbody2D playerRigidbody;
	
	void Update () {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(0) && !jumping) {
            Attack();
        }
	}

    void FixedUpdate() {
        position = new Vector2(transform.position.x, transform.position.y);
        playerRigidbody = GetComponent<Rigidbody2D>();

        if(!jumping) {
            Move();
        }

        if(jumping) {
            UpdateAttack();
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if(jumping) {
            if(collider.gameObject.tag == "Enemy")
                collider.gameObject.GetComponent<Enemy>().Damage();
        }
    }

    void Move() {
        if((mousePosition - position).magnitude > noMove)
            playerRigidbody.velocity = (mousePosition - position).normalized * moveSpeed;
        else
            playerRigidbody.velocity = Vector2.zero;
    }

    void Attack() {
        Vector2 position = transform.position;
        jumping = true;
        jumpPosition = position + (mousePosition - position).normalized * jumpDistance;
        jumpTime = Time.time;
    }

    void UpdateAttack() {
        if(Time.time - jumpTime > jumpDistance / jumpSpeed) {
            jumping = false;
            return;
        }

        playerRigidbody.velocity = (jumpPosition - position).normalized * jumpSpeed;
    }
}
