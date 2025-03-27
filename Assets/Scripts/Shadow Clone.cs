using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShadowClone : MonoBehaviour
{
    private PlayerMovement player;
    public float delayStart;
    private float actDelay;
    private List<PositionInfo> playerPositions = new List<PositionInfo>();

    public GameObject shadow;
    //public Animator shadowAnim;

    private bool created;
    public GameObject creationFx;
    //private string savedAnim;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        PositionInfo posNew = new PositionInfo
        {
            position = player.transform.position,
            rotation = player.transform.rotation,
            scale = player.transform.localScale,
            //anim = player.anim.GetCurrentAnimatorClipInfo(0)[0].clip,
        };

        playerPositions.Add(posNew);

        if(actDelay < delayStart)
        {
            actDelay += Time.deltaTime;
            if (actDelay > delayStart)
            { 
                shadow.SetActive(true);
                //SetShadowPos(playerPositions[0]);
                if (creationFx)
                {
                    GameObject fx = Instantiate(creationFx, playerPositions[0].position, Quaternion.identity);
                    fx.SetActive(true);
                }
            }
            return;
        }
        PositionInfo setInfo = playerPositions[0];


    }

    void Update()
    {
        
    }

    
}

[System.Serializable]
public class PositionInfo
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    //public AnimationClip anim;
}
