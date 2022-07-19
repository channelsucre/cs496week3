using UnityEngine;

public class BetterJump : MonoBehaviour {
    public float baseMultiplier = 2f, fallMultiplier = 7f, lowJumpMultiplier = 4f;

    Rigidbody2D body;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (body.velocity.y < 0) {
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        } else if (body.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        } else if (body.velocity.y > 0) {
            body.velocity += Vector2.up * Physics2D.gravity.y * (baseMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
}