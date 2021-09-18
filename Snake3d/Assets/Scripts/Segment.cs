using UnityEngine;

public class Segment : MonoBehaviour
{

    public GameObject cube;

    public float transitionSpeed = 5f;

    public bool dead = false;

    Vector3 targetGridPos;

    private void Start()
    {

        targetGridPos = Vector3Int.RoundToInt(transform.position);

    }

    void FixedUpdate()
    {
        if (dead)
        {
            Vector3 targetPosition = targetGridPos;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * transitionSpeed);

            if (Vector3.Distance(transform.position, targetGridPos) < 0.05f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Die()
    {

        cube = GameObject.Find("Cube");

        cube.transform.localScale = new Vector3(0.9999f, 0.9999f, 0.9999f);

        targetGridPos += transform.forward;

        dead = true;

    }
}
