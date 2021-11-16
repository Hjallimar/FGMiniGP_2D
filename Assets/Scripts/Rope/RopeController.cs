using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [Header("Scaling")]
    [SerializeField, Range(0.1f, 1.0f)] private float ScaleSize = 1.0f;
    [SerializeField, Range(0, 11)] private int AmmountOfJoints = 7;
    
    [Space, Header("Links")]
    [SerializeField] private GameObject RopeLinkPrefab;
    [SerializeField] private GameObject Root;
    [SerializeField] private GameObject RootSprite;
    [SerializeField] private int AmmountOfLinks = 0;


    [SerializeField] private bool ReBuild = false;
    [SerializeField] private bool ResetRope = false;
    
    private Rigidbody2D RootRB;
    [SerializeField] private List<GameObject> Vevare;
    private List<MyRopeLink> RopeLinks;
    private Vector2 LinkAnchors = new Vector2(0.6f, -0.4f);
    private float OldScale = 1.0f;

    private struct MyRopeLink
    {
        public GameObject gameObject;
        public Rigidbody2D rigidBody;
        public HingeJoint2D hingeJoint;
        public BoxCollider2D collider;
        public Transform spriteVisual;
        public CircleCollider2D triggerCollider;
        
        public override bool Equals(object other)
        {
            if((object)other == null)
                return false;
            GameObject otherObj = other as GameObject;
            return gameObject == otherObj;
        }
    }

    void CreateNewLink()
    {
        MyRopeLink newLink = new MyRopeLink();
        newLink.gameObject = Instantiate(RopeLinkPrefab, Root.transform);
        newLink.rigidBody = newLink.gameObject.GetComponent<Rigidbody2D>();
        newLink.hingeJoint = newLink.gameObject.GetComponent<HingeJoint2D>();
        newLink.collider = newLink.gameObject.GetComponent<BoxCollider2D>();
        newLink.triggerCollider = newLink.gameObject.GetComponent<CircleCollider2D>();
        newLink.spriteVisual = newLink.gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.transform;
        RopeLinks.Add(newLink);
        Vevare.Add(newLink.gameObject);
    }

    void InstantiateLink(int i)
    {
        RopeLinks[i].gameObject.transform.localPosition = (i + 1)  * Vector2.down;
        RopeLinks[i].collider.size = new Vector2(0.2f, 0.9f) * ScaleSize;
        RopeLinks[i].triggerCollider.radius = ScaleSize * 0.5f;
        RopeLinks[i].spriteVisual.localScale = new Vector2(0.2f, 0.9f) * ScaleSize;
        
        if (i > 0)
            RopeLinks[i].hingeJoint.connectedBody = RopeLinks[i - 1].rigidBody;
        else
            RopeLinks[i].hingeJoint.connectedBody = RootRB;
       
        RopeLinks[i].hingeJoint.anchor = new Vector2(0.0f, LinkAnchors.x) * ScaleSize;
        RopeLinks[i].hingeJoint.connectedAnchor = new Vector2(0.0f, LinkAnchors.y) * ScaleSize;
    }
    
    void BuildRope()
    {
        GatherInformation();
        for (int i = 0; i < AmmountOfJoints; i++)
        {
            CreateNewLink();
        }
        
        for (int i = 0; i < RopeLinks.Count; i++)
        {
            InstantiateLink(i);
        }
        Debug.Log("Rope has been built and has " + RopeLinks.Count + " links");
        OldScale = ScaleSize;
    }

    void GatherInformation()
    {
        RopeLinks = new List<MyRopeLink>();
        RootRB = Root.GetComponent<Rigidbody2D>();
        RootSprite.transform.localScale = new Vector2(0.2f, 0.9f) * ScaleSize;
    }

    void AddLinksToRope()
    {
        int Addition = AmmountOfJoints - RopeLinks.Count;

        for (int i = 0; i < Addition; i++)
        {
            CreateNewLink();
        }
        for (int j = RopeLinks.Count - Addition; j < RopeLinks.Count; j++)
        {
            InstantiateLink(j);
        }
    }

    void RemoveLinksFromRope()
    {
        int Ammount = RopeLinks.Count - AmmountOfJoints;
        for (int i = 0; i < Ammount; i++)
        {
            GameObject Obj = RopeLinks[RopeLinks.Count - 1].gameObject;
            RopeLinks.RemoveAt(RopeLinks.Count -1);
            Vevare.Remove(Obj);
            DestroyImmediate(Obj);
        }
    }

    void ReScaleRope()
    {
        RootSprite.transform.localScale = new Vector2(0.2f, 0.9f) * ScaleSize;
        float i = ScaleSize;
        foreach (MyRopeLink link in RopeLinks)
        {
            link.gameObject.transform.localPosition = i * Vector2.down;
            link.collider.size = new Vector2(0.2f, 0.9f) * ScaleSize;
            link.spriteVisual.localScale = new Vector2(0.2f, 0.9f) * ScaleSize;
            link.triggerCollider.radius = ScaleSize * 0.5f;
            link.hingeJoint.anchor = new Vector2(0.0f, LinkAnchors.x) * ScaleSize;
            link.hingeJoint.connectedAnchor = new Vector2(0.0f, LinkAnchors.y) * ScaleSize;
            i+= ScaleSize;
        }

        OldScale = ScaleSize;
    }
    
    private void OnDrawGizmosSelected()
    {
        if (ResetRope)
        {
            AmmountOfLinks = 0;
            AmmountOfJoints = 0;
            RemoveLinksFromRope();
            RopeLinks.Clear();
            Vevare.Clear();
            ResetRope = false;
            ScaleSize = 1.0f;
            OldScale = 1.0f;
        }
        if (ReBuild)
        {
            ReBuild = false;
            if (AmmountOfJoints != AmmountOfLinks)
            {
                if (AmmountOfLinks == 0)
                {
                    Debug.Log("Building new rope wants: " + AmmountOfJoints + ", current Rope Links: " +  AmmountOfLinks);
                    BuildRope();
                }
                else if(RopeLinks.Count > AmmountOfJoints)
                {
                    Debug.Log("Removing links: " + AmmountOfJoints + ", current Rope Links: " +  AmmountOfLinks);
                    RemoveLinksFromRope();
                }
                else
                {
                    Debug.Log("Adding links: " + AmmountOfJoints + ", current Rope Links: " +  AmmountOfLinks);
                    AddLinksToRope();
                }
            }
            else if(OldScale != ScaleSize)
            {
                ReScaleRope();   
            }

            AmmountOfLinks = RopeLinks.Count;
        }
          
    }
}
