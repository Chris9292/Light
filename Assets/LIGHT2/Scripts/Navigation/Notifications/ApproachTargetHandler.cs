using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ApproachTargetHandler : MonoBehaviour
{

    public Transform targetA;
    public UnityEvent OnEnterA;
    public UnityEvent OnExitA;
    private bool InA;


    public Transform targetB;
    public UnityEvent OnEnterB;
    public UnityEvent OnExitB;
    private bool InB;


    public Transform targetC;
    public UnityEvent OnEnterC;
    public UnityEvent OnExitC;
    private bool InC;


    Transform player;
    //public List<Entry> route;
    public int TimerMax = 5;
    
    // Maybe we need different trigger distance for different targets (requires real-world concept testing)
    [Tooltip("Event trigger distance")]
    public float eventDistance = 5.0f;

    // Can be writtern with List (?)
    private float DistA = 0.0f, DistB = 0.0f, DistC = 0.0f;
    
    // Not sure why we need a timer
    private float timer = 0.0f;
    //private Node curNode;
    //private Grid grid;

    void Awake()
    {
        //grid = GetComponent<Grid>();

    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //curNode = grid.NodeFromWorldPoint(player.position);
        //route.Add(new Entry()
        //{
        //    node = curNode,
        //    time = Mathf.RoundToInt(Time.realtimeSinceStartup),
        //    WorldPos = new Vector2(player.position.x, player.position.z)
        //});
        //Debug.Log("Updated Route "  + route[route.Count-1].node.iGridX.ToString() + route[route.Count - 1].node.iGridY.ToString() 
        //                            + route[route.Count - 1].time.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //if(curNode != null)
        //    UpdateRoute();
        Vector2 playerPos = new Vector2(player.position.x, player.position.z);
        DistA = Vector2.Distance(playerPos, new Vector2(targetA.position.x, targetA.position.z));
        DistB = Vector2.Distance(playerPos, new Vector2(targetB.position.x, targetB.position.z));
        DistC = Vector2.Distance(playerPos, new Vector2(targetC.position.x, targetC.position.z));
        if (timer > TimerMax)
        {
            Debug.Log("A: " + DistA.ToString());
            Debug.Log("B: " + DistB.ToString());
            Debug.Log("C: " + DistC.ToString());
            timer = 0.0f;
        }
        DistanceEvendHandle();
    }

    private void DistanceEvendHandle()
    {
        if (DistA < eventDistance && !InA)
        {
            OnEnterA?.Invoke();
            InA = true;
            return;
        }
        if (DistA > eventDistance && InA)
        {
            OnExitA?.Invoke();
            InA = false;
            return;
        }
        if (DistB < eventDistance && !InB)
        {
            OnEnterB?.Invoke();
            InB = true;
            return;
        }
        if (DistB > eventDistance && InB)
        {
            OnExitB?.Invoke();
            InB = false;
            return;

        }
        if (DistC < eventDistance && !InC)
        {
            OnEnterC?.Invoke();
            InC = true;
            return;
        }
        if (DistC > eventDistance && InC)
        {
            OnExitC?.Invoke();
            InC = false;
            return;

        }
    }

    //   void UpdateRoute()
    //{
    //       Node newNode = grid.NodeFromWorldPoint(player.position);
    //       if (newNode.iGridX == curNode.iGridX && newNode.iGridY == curNode.iGridY)
    //           return;
    //       else
    //       {
    //           curNode.iGridX = newNode.iGridX;
    //           curNode.iGridY = newNode.iGridY;
    //           route.Add(new Entry()
    //           {
    //               node = curNode,
    //               time = Mathf.RoundToInt(Time.realtimeSinceStartup),
    //               WorldPos = new Vector2(player.position.x, player.position.z)
    //           }); 
    //           Debug.Log("Updated Route " + route[route.Count - 1].node.iGridX.ToString() + route[route.Count - 1].node.iGridY.ToString()
    //                                    + route[route.Count - 1].time.ToString());
    //       }
    //   }
}

//public class Entry
//{
//    public Vector2 WorldPos { get; set; }
//    public Node node;
//    public int time;
//}
