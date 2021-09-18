using UnityEngine;

public class Item : MonoBehaviour
{

    public GameObject itemMesh;

    public float transitionSpeed = 7.5f;

    public bool dead = false;

    Vector3 targetScale;

    void FixedUpdate()
    {
        if (dead)
        {
            Vector3 targetLocalScale = targetScale;

            itemMesh.transform.localScale = Vector3.MoveTowards(itemMesh.transform.localScale, targetLocalScale, Time.deltaTime * transitionSpeed);
            itemMesh.transform.localPosition = new Vector3(itemMesh.transform.localScale.x / -2, itemMesh.transform.localScale.y / -2, itemMesh.transform.localScale.z / 2);

            if (Vector3.Distance(itemMesh.transform.localScale, targetLocalScale) < 0.005f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Die()
    {

        itemMesh = GameObject.Find("Item Mesh");

        targetScale = new Vector3(0, 0, 0);

        dead = true;

    }
}
