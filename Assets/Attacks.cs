using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    [SerializeField] GameObject shield;
    bool canShield = true;
    GameObject currentShield;
    Animator anim;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        Shield();
    }
    void Shield()
    {
        if (Input.GetKeyDown(KeyCode.E) && canShield)
        {
            StartCoroutine(shieldCoolDown());
            Vector3 shieldPos = new Vector3(transform.position.x, 0, transform.position.z + 2.5f);
            currentShield = Instantiate(shield, shieldPos, Quaternion.identity);
            anim.SetTrigger("RockShield");
            Invoke("destroy", 5f);
        }
    }
    void destroy()
    {
        Destroy(currentShield);
    }
    IEnumerator shieldCoolDown()
    {
        canShield = false;
        yield return new WaitForSeconds(7f);
        canShield = true;
    }
}
