using UnityEngine;

public class Vine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Contains("Ball")) return;

        Ball ball = collision.GetComponent<Ball>();

        if (ball != null) ball.onVine = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.tag.Contains("Ball")) return;

        Ball ball = collision.GetComponent<Ball>();

        if (ball != null) ball.onVine = false;
    }
}
