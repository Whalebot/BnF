using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast_Shadow_Script : MonoBehaviour {
    SpriteRenderer SR;
    public LayerMask shadowCollision;
    public float rayLength;
    public float yOffset;
    GameObject target;
    RaycastHit2D shadowRay;
    Vector3 startScale;
    float xOffset;
    public Animator mainCharacterAnimator;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
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
         
        transform.position = shadowRay.point + new Vector2(xOffset*Mathf.Sign(transform.parent.localScale.x),yOffset);
        transform.localScale =  startScale * (1-shadowRay.distance/ rayLength);
        AnimatorCopy();
	}

    void AnimatorCopy() {
        anim.SetBool("FromAir", mainCharacterAnimator.GetBool("FromAir"));
        anim.SetBool("Walking", mainCharacterAnimator.GetBool("Walking"));
        anim.SetBool("Running", mainCharacterAnimator.GetBool("Running"));
        anim.SetBool("Hitstun",mainCharacterAnimator.GetBool("Hitstun"));
        anim.SetBool("Dashing", mainCharacterAnimator.GetBool("Dashing"));
        anim.SetBool("Backdashing", mainCharacterAnimator.GetBool("Backdashing"));
        anim.SetBool("FromDash", mainCharacterAnimator.GetBool("FromDash"));
        anim.SetBool("FromRun", mainCharacterAnimator.GetBool("FromRun"));
        anim.SetBool("Grounded", mainCharacterAnimator.GetBool("Grounded"));
        anim.SetBool("Startup", mainCharacterAnimator.GetBool("Startup"));
        anim.SetBool("Active", mainCharacterAnimator.GetBool("Active"));
        anim.SetBool("Recovery", mainCharacterAnimator.GetBool("Recovery"));
        anim.SetBool("Attacking", mainCharacterAnimator.GetBool("Attacking"));

        anim.SetInteger("AttackID", mainCharacterAnimator.GetInteger("AttackID"));
        anim.SetInteger("Ascending", mainCharacterAnimator.GetInteger("Ascending"));

    }
}
