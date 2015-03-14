using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int hits;
    public float chargeDuration;
    private float chargeTime;
    private bool charging = false;
    private bool jumping;
    private Vector2 jumpPosition;
    private float jumpTime;
    public float chargeDistance;
    public float jumpDistance;
    public float jumpSpeed;
    public float moveSpeed;

    private Vector2 playerPosition;
    private Vector2 position;

	void FixedUpdate () {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        position = new Vector2(transform.position.x, transform.position.y);

        if(!charging && !jumping && (playerPosition - position).magnitude <= chargeDistance) {
            charging = true;
            chargeTime = Time.time;
        }
        else if(!charging && !jumping && (playerPosition - position).magnitude > chargeDistance) {
            Move();
        }
        
        if(charging) {
            Charge();
        }
        
        if(jumping) {
            UpdateAttack();
        }

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

    void Attack() {
        Vector2 position = transform.position;
        jumping = true;
        jumpPosition = position + (playerPosition - position).normalized * jumpDistance;
        jumpTime = Time.time;
    }

    void UpdateAttack() {
        if(Time.time - jumpTime > jumpDistance / jumpSpeed) {
            jumping = false;
            return;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = (jumpPosition - new Vector2(transform.position.x, transform.position.y)).normalized * jumpSpeed;
    }

    void Move() {
        gameObject.GetComponent<Rigidbody2D>().velocity = (playerPosition - position).normalized * moveSpeed;
    }

    void Charge() {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if(Time.time - chargeTime > chargeDuration) {
            Attack();
            charging = false;
        }
    }
}
