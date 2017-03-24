using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    NORMAL,
    BURNING,
    FROZEN,
    STUNNED,
    HYPER,
    INVULNERABLE
};


[RequireComponent(typeof(Rigidbody))]
public class script_player : MonoBehaviour {

    //Public fields
    public float boostAmount;
    public float rotateAmount;
    public float defaultCooldown;

    //Private fields
    private Rigidbody rb;
    private Vector3 direction;
    private float boostCooldown;
    private float timestamp = 0.0f;

    private Status playerstatus;

    //Initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        direction = new Vector3(0.0f, 0.0f, boostAmount);
        boostCooldown = defaultCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //Get player inputs
        float boostInput = Input.GetAxisRaw("Boost");
        float rotateInput = Input.GetAxisRaw("Horizontal");

        //Is the player trying to boost?
        if (boostInput != 0)
        {
            if (timestamp <= Time.time)
            {
                rb.AddRelativeForce(direction);
                timestamp = Time.time + boostCooldown;
            }

        }


        //Is the player rotating?
        if (rotateInput != 0)
        {
            transform.Rotate(0.0f, rotateInput * rotateAmount, 0.0f, Space.Self);
        }

    }

    

    /*
    public void setBoosterModifier(Status stat)
    {
        float boostModifier = 1;
        float cooldownModifier = 1;

        switch (stat)
        {
            case Status.NORMAL:
                //Do Nothing
                break;
            case Status.FROZEN:
                boostModifier = .freezeEffect;
                cooldownModifier = playerstatus.freezeEffect;
                break;
            case Status.STUNNED:
                //Change me,
                //Should disable the addForce call rather than altering these
                boostModifier = playerstatus.stunnedEffect;
                cooldownModifier = playerstatus.stunnedEffect;
                break;
            case Status.HYPER:
                boostModifier = playerstatus.hyperEffect;
                cooldownModifier = playerstatus.hyperEffect;
                break;
            default:
                Debug.LogError("Invalid status effect on player");
                break;
        }

        direction = new Vector3(0.0f, 0.0f, boostAmount * boostModifier);
        boostCooldown = defaultCooldown * cooldownModifier;
    }*/
}
