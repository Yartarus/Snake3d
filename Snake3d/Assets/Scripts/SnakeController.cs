using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    float startDelay = 0.75f;

    public int boxSize = 9;

    public bool smoothTransition = true;
    public float transitionSpeed = 10f;
    public float transitionRotationSpeed = 500f;

    public string moveDirection = "forward";
    public bool turned = false;

    public GameObject Segment;
    public List<GameObject> segmentList = new List<GameObject>();
    public int snakeLength = 3;

    public GameObject Item;
    public List<GameObject> itemList = new List<GameObject>();

    public bool dead = false;


    Vector3 targetGridPos;
    Vector3 prevTargetGridPos;
    Vector3 targetRotation;

    Vector3 newItemPos;

    private void Start()
    {
        
        targetGridPos = Vector3Int.RoundToInt(transform.position);
        prevTargetGridPos = targetGridPos;

        CheckItem();

    }

    private void FixedUpdate()
    {

        if (startDelay >= 0)
        {
            startDelay -= Time.deltaTime;
            return;
        }

        MovePlayer();

        CheckItem();

    }

    void MovePlayer()
    {

        if (AtRest)
        {
            if (moveDirection == "left" && turned == false)
            {
                targetRotation = (Quaternion.Euler(targetRotation) * Quaternion.Euler(0, -90, 0)).eulerAngles;
                targetRotation = targetRotation.Round(0);
                moveDirection = "forward";
                turned = true;
            } else if (moveDirection == "right" && turned == false)
            {
                targetRotation = (Quaternion.Euler(targetRotation) * Quaternion.Euler(0, 90, 0)).eulerAngles;
                targetRotation = targetRotation.Round(0);
                moveDirection = "forward";
                turned = true;
            }
            else if (moveDirection == "up" && turned == false)
            {
                targetRotation = (Quaternion.Euler(targetRotation) * Quaternion.Euler(-90, 0, 0)).eulerAngles;
                targetRotation = targetRotation.Round(0);
                moveDirection = "forward";
                turned = true;
            }
            else if (moveDirection == "down" && turned == false)
            {
                targetRotation = (Quaternion.Euler(targetRotation) * Quaternion.Euler(90, 0, 0)).eulerAngles;
                targetRotation = targetRotation.Round(0);
                moveDirection = "forward";
                turned = true;
            }
            else
            {
                targetGridPos += transform.forward;
                targetGridPos = targetGridPos.Round(0);
                turned = false;
                CollisionCheck();
                CreateSegment();
            }
        }

        if (!dead)
        {

            prevTargetGridPos = targetGridPos;

            Vector3 targetPosition = targetGridPos;

            if (!smoothTransition)
            {
                transform.position = targetPosition;
                transform.rotation = Quaternion.Euler(targetRotation);
            } else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * transitionRotationSpeed);
            }

        } else
        {

            targetGridPos = prevTargetGridPos;

        }

    }

    public void RotateLeft() 
    { 
        moveDirection = "left";
    }
    public void RotateRight() 
    { 
        moveDirection = "right";
    }
    public void RotateUp()
    {
        moveDirection = "up";
    }
    public void RotateDown()
    {
        moveDirection = "down";
    }

    void CreateSegment()
    {
        if (dead)
        {
            return;
        }

        GameObject seg = Instantiate(Segment, prevTargetGridPos, Quaternion.Euler(targetRotation));
        seg.AddComponent<Segment>();
        segmentList.Insert(0, seg);

        if (segmentList.Count >= snakeLength)
        {
            segmentList[segmentList.Count - 1].GetComponent<Segment>().Die();
            segmentList.RemoveAt(segmentList.Count - 1);
        }
    }

    void CollisionCheck()
    {
        foreach (GameObject seg in segmentList)
        {
            if (targetGridPos == seg.transform.position.Round(0)
                || targetGridPos.x <= -1
                || targetGridPos.y <= -1
                || targetGridPos.z <= -1
                || targetGridPos.x >= boxSize
                || targetGridPos.y >= boxSize
                || targetGridPos.z >= boxSize)
            {
                if (!dead)
                {
                    FindObjectOfType<GameManager>().EndGame();
                }

                dead = true;
            }
        }

        if (itemList.Count > 0)
        {
            if (targetGridPos == itemList[0].transform.position.Round(0))
            {
                EatItem();
            }
        }
    }

    void EatItem()
    {
        if (itemList.Count > 0)
        {
            itemList[itemList.Count - 1].GetComponent<Item>().Die();
            itemList.Clear();
        }

        snakeLength += 3;
        FindObjectOfType<Score>().IncrementScore();
    }

    void CheckItem()
    {
        if (itemList.Count == 0)
        {
            SpawnItem();
        }
    }

    void SpawnItem()
    {

        newItemPos = new Vector3(Random.Range(0, boxSize), Random.Range(0, boxSize), Random.Range(0, boxSize));

        if (newItemPos == targetGridPos)
        {
            return;
        }

        foreach (GameObject seg in segmentList)
        {
            if (newItemPos == seg.transform.position)
            {
                return;
            }
        }

        GameObject item = Instantiate(Item, newItemPos, Quaternion.identity);
        item.AddComponent<Item>();
        itemList.Add(item);

    }


    bool AtRest
    {
        get
        {
            if ((Vector3.Distance(transform.position, targetGridPos) < 0.05f) &&
                (Quaternion.Angle(transform.rotation, Quaternion.Euler(targetRotation)) < 0.05f))
                return true;
            else
                return false;
        }
    }
}
