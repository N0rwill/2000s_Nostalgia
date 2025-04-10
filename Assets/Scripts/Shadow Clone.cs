using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowClone : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    public float delayStart;
    private float actDelay;
    private List<PositionInfo> playerPositions = new List<PositionInfo>();

    public GameObject shadow;
    //public Animator shadowAnim;
    public float distanceToPlayer;

    public GameObject start;
    private bool created;
    //public GameObject creationFx;
    //private string savedAnim;

    void FixedUpdate()
    {
        //array set for shadow position
        PositionInfo posNew = new PositionInfo
        {
            position = player.transform.position,
            scale = player.transform.localScale,
            //anim = player.anim.GetCurrentAnimatorClipInfo(0)[0].clip,
        };

        playerPositions.Add(posNew);

        //delay
        if(actDelay < delayStart)
        {
            actDelay += Time.deltaTime;
            //start shadow
            if (actDelay > delayStart)
            { 
                shadow.SetActive(true);
                //place shadow
                SetShadowPos(playerPositions[0]);
                //create visual fx
                /*if (true)
                {
                    GameObject fx = Instantiate(start, playerPositions[0].position, Quaternion.identity);
                    fx.SetActive(true);
                }*/
            }
            return;
        }
        
        //set info
        PositionInfo setInfo = playerPositions[0];
        SetShadowPos(setInfo);
        //SetShadowAnimation(setInfo);

        
        distanceToPlayer = Vector3.Distance(player.transform.position, shadow.transform.position);
        Caught(player.transform.position);

        playerPositions.RemoveAt(0);


    }

    void SetShadowPos(PositionInfo setInfo)
    {
        //setting shadow position
        shadow.transform.position = setInfo.position;
        shadow.transform.localScale = setInfo.scale;

        //check distance
        Vector3 direction2Player = (player.transform.position - shadow.transform.position).normalized;
        shadow.transform.rotation = Quaternion.LookRotation(direction2Player);
        
    }


    void Caught(Vector3 playerPosition)
    {
        if (distanceToPlayer < 1)
        {
            
        }

    }

    
}

[System.Serializable]
public class PositionInfo
{
    //position info
    public Vector3 position;
    public Vector3 scale;
    //public AnimationClip anim;
}
