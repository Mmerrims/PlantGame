using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player != null)
        {
            transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + .4f);
        }
    }
}
