using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShinyObject : MonoBehaviour
{
    [SerializeField] AudioClip shinyObjectPickup;
    public GameObject player;
    public float speed = 2f;
    public float attractRange = 2f;
    public LayerMask playerLayer;
    public GameState ShinyObjectIncrease;

    int sCount;

    void Start()
    {
        player = GameObject.Find("Player");
        ShinyObjectIncrease = FindObjectOfType<GameState>();
    }

    public enum SHINYOBJECTTYPE
    {
        red,
        yellow,
        green,
        blue,
        purple
    }

    public enum ATTRACTIONTYPE
    {
        staticShinyObject,
        magneticShinyObject
    }

    public SHINYOBJECTTYPE shinyObjectType;
    public ATTRACTIONTYPE AttractionType;


    // Update is called once per frame
    void Update()
    {
        ShinyObjectState();
        ShinyObjectColorType();
        Debug.Log(sCount);
    }

    public void ShinyObjectState()
    {
        switch (AttractionType)
        {
            case ATTRACTIONTYPE.staticShinyObject:
                magnetRange();
                break;
            case ATTRACTIONTYPE.magneticShinyObject:
                playerMagnet();
                break;
        }
    }

    public void ShinyObjectColorType()
    {
        switch (shinyObjectType)
        {
            case SHINYOBJECTTYPE.red:
                sCount = 1;
                break;
            case SHINYOBJECTTYPE.yellow:
                sCount = 2;
                break;
            case SHINYOBJECTTYPE.green:
                sCount = 5;
                break;
            case SHINYOBJECTTYPE.blue:
                sCount = 10;
                break;
            case SHINYOBJECTTYPE.purple:
                sCount = 50;
                break;
        }
    }

    void playerMagnet()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void magnetRange()
    {
        Collider[] playerHit = Physics.OverlapSphere(transform.position, attractRange, playerLayer);

        foreach (Collider player in playerHit)
        {
            PlayerCombat _player = player.GetComponent<PlayerCombat>();
            if (_player != null)
            {
                playerMagnet();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCombat playerCombat = other.gameObject.GetComponent<PlayerCombat>();
        if (playerCombat != null)
        {
            OneShotSoundManager.PlayClip2D(shinyObjectPickup, 1);
            ShinyObjectIncrease.ShinyObjectScore(sCount);
            Destroy(gameObject);
            
        }
    }

    private void OnDrawGizmos()
    {
 

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, attractRange);

    }
}
