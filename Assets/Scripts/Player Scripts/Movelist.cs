using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movelist : MonoBehaviour {

    [HeaderAttribute("Dash attributes")]
    [Space(10)]

    public int dashSpeed;
    public int dashDuration;
    public int dashRecovery;

    public int backdashSpeed;
    public int backdashDuration;
    public int backdashRecovery;

    public bool canElectric;
    public int electricCost;

    [HeaderAttribute("Running attacks")]
    [Space(10)]
    public Move move6A;
    public GameObject moveObject6A;

    [HeaderAttribute("Special attacks")]
    [Space(10)]
    public Move move5S;
    public GameObject moveObject5S;
    public Move move5SS;
    public GameObject moveObject5SS;
    public Move move5SSS;
    public GameObject moveObject5SSS;
    public Move move8S;
    public GameObject moveObject8S;
    public Move move2S;
    public GameObject moveObject2S;
    public Move moveExtra;
    public GameObject moveObjectExtra;
    public Move moveExtra2;
    public GameObject moveObjectExtra2;

    [HeaderAttribute("Grounded attacks")]
    [Space(10)]
    public Move move5A;
    public GameObject moveObject5A;
    public Move move5AA;
    public GameObject moveObject5AA;
    public Move move5AAA;
    public GameObject moveObject5AAA;
    public Move move5AAAA;
    public GameObject moveObject5AAAA;
    public Move move8A;
    public GameObject moveObject8A;
    public Move move8AA;
    public GameObject moveObject8AA;
    public Move move2A;
    public GameObject moveObject2A;
    public Move move2AA;
    public GameObject moveObject2AA;

    [HeaderAttribute("Jump attacks")]
    [Space(10)]
    public Move moveJ5A;
    public GameObject moveObjectJ5A;
    public Move moveJ5AA;
    public GameObject moveObjectJ5AA;
    public Move moveJ5AAA;
    public GameObject moveObjectJ5AAA;
    public Move moveJ5AAAA;
    public GameObject moveObjectJ5AAAA;
    public Move moveJ8A;
    public GameObject moveObjectJ8A;
    public Move moveJ8AA;
    public GameObject moveObjectJ8AA;
    public Move moveJ2A;
    public GameObject moveObjectJ2A;
    public Move moveJ2AA;
    public GameObject moveObjectJ2AA;

    [HeaderAttribute("Jump attacks")]
    [Space(10)]
    public bool limitJ8A;
    public bool chargeWeapon = false;
    public bool justFrameWeapon = false;

    [HeaderAttribute("Charged attacks")]
    [Space(10)]
    public Move moveC5A;
    public GameObject moveObjectC5A;
    public Move moveCC5A;
    public GameObject moveObjectCC5A;
    public Move moveC5AA;
    public GameObject moveObjectC5AA;
    public Move moveCC5AA;
    public GameObject moveObjectCC5AA;
    public Move moveC5AAA;
    public GameObject moveObjectC5AAA;
    public Move moveCC5AAA;
    public GameObject moveObjectCC5AAA;

    public Move moveC8A;
    public GameObject moveObjectC8A;
    public Move moveC8AA;
    public GameObject moveObjectC8AA;
    public Move moveC2A;
    public GameObject moveObjectC2A;
    public Move moveC2AA;
    public GameObject moveObjectC2AA;

}
