using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField] Animator playerAnims;
    //public Tiki _tiki;
    [SerializeField] GameObject wandVisuals = null;
    [SerializeField] AudioClip wandSound = null;
   

    public CameraShake cameraShake;
    public Transform _initalStartAttackPoint;
    public Transform _spinAttackPoint;
    public GameObject _spinAxis;
    public float _spinAttackRange = 0.4f;
    public LayerMask _enemyLayers;
    bool isAttacking = false;
    bool canSpin = true;
     
    void Start()
    {
        //_initalStartAttackPoint = _spinAttackPoint.transform;
        
    }
   
    void Update()
    {
        SpinAttack();
        //Debug.Log(_initalStartAttackPoint);
    }

    void SpinAttack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //to ensure we don't spin again in the middle of an attack
            if (canSpin)
            {
                isAttacking = true;
                //graphic visuals that play when SpinAttack() activates
                playerAnims.Play("SpinAttack");
                OneShotSoundManager.PlayClip2D(wandSound, 1);
                enableWandVisuals();
                StartCoroutine(disableWandVisuals());
                canSpin = false;
                StartCoroutine(resetSpin());
                StartCoroutine(stopSpinAttack());
            }
        }

        if (isAttacking == true)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(_spinAttackPoint.position, _spinAttackRange, _enemyLayers);
            //_spinAttackPoint.transform.RotateAround(_spinAxis.transform.position, Vector3.up, 510 * Time.deltaTime);
      
            //damage enemies
            foreach (Collider enemy in hitEnemies)
            {
                Tiki _tiki = enemy.GetComponent<Tiki>();
                if(_tiki != null)
                {
                    _tiki.destroyTiki();
                    StartCoroutine(cameraShake.Shake(.05f, .2f));
                }
            }
        }

    }

    void enableWandVisuals()
    {
        wandVisuals.SetActive(true);
    }

    IEnumerator disableWandVisuals()
    {
        yield return new WaitForSeconds(0.75f);
        wandVisuals.SetActive(false);
    }

    IEnumerator resetSpin()
    {
        yield return new WaitForSeconds(0.85f);
        canSpin = true;
        
    }

    IEnumerator stopSpinAttack()
    {
        yield return new WaitForSeconds(0.75f);
        isAttacking = false;
      //  _spinAttackPoint = _initalStartAttackPoint.transform;
    }




    private void OnDrawGizmos()
    {
        if (_spinAttackPoint == null)
            return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_spinAttackPoint.transform.position, _spinAttackRange);
        
    }
}
