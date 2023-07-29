using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    [SerializeField] GameObject shield;
    [SerializeField] GameObject[] stones;
    GameObject currentShield;
    Animator anim;
    List<GameObject> instantiatedStones;


    bool canShield = true;
    bool canStoneShower= true;


    private void Start()
    {
        instantiatedStones = new List<GameObject>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        Shield();
        stoneShower();
    }
    void Shield()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && canShield)
        {
            StartCoroutine(shieldCoolDown());
            Vector3 shieldPos = new Vector3(transform.position.x, 0, transform.position.z + 2.5f);
            currentShield = Instantiate(shield, shieldPos, Quaternion.identity);
            anim.SetTrigger("RockShield");
            Invoke("destroyShield", 5f);
        }
    }
    void stoneShower()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && canStoneShower)
        {
            StartCoroutine(stoneShowerCoolDown());

            for (int i = 0; i < 10; i++)
            {
                Vector3 stonesPos = new Vector3(transform.position.x+Random.Range(-3,3), transform.position.y+ Random.Range(8,10), transform.position.z);
                instantiatedStones.Add(Instantiate(stones[Random.Range(0, stones.Length)], stonesPos, Quaternion.identity));
            }
            
            anim.SetTrigger("StoneShower");
            Invoke("destroyStoneShower", 5f);
            
        }
    }
    void destroyShield()
    {
        Destroy(currentShield);
    }
    void destroyStoneShower()
    {
        foreach( GameObject stones in instantiatedStones) 
        {
            Destroy(stones);
        }
        instantiatedStones.Clear();
        
    }
    IEnumerator shieldCoolDown()
    {
        canShield = false;
        yield return new WaitForSeconds(7f);
        canShield = true;
    }
    IEnumerator stoneShowerCoolDown()
    {
        canStoneShower = false;
        yield return new WaitForSeconds(7f);
        canStoneShower = true;
    }
}
