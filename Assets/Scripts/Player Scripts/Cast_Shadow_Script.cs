using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast_Shadow_Script : MonoBehaviour {
    SpriteRenderer SR;
    public LayerMask shadowCollision;
    public float rayLength;
    GameObject target;
    RaycastHit2D shadowRay;
    Vector3 startScale;
    float xOffset; 

    void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        xOffset = transform.localPosition.x;
        target = transform.parent.gameObject;
    }

	// Use this for initialization
	void Start () {
        startScale = transform.localScale;
        transform.DetachChildren();
	}
	
	// Update is called once per frame
	void Update () {
        shadowRay = Physics2D.Raycast(target.transform.position, Vector2.down, rayLength, shadowCollision);
        if (shadowRay == false) SR.enabled = false;
        else SR.enabled = true;
         
        transform.position = shadowRay.point + new Vector2(xOffset*Mathf.Sign(transform.parent.localScale.x),0);
        transform.localScale = startScale * (1-shadowRay.distance/ rayLength);
	}
}
