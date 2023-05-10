using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public int maxHP;
    public int HP;
    [SerializeField] public ParticleSystem ps;
    [SerializeField] public GameObject HPBar;

    private void Start()
    {
        maxHP = 10;
        HP = maxHP;
    }

    public void Damaged(int _dmg)
    {
        HP -= _dmg;
        ps.transform.position = this.gameObject.transform.position;
        ps.Play();
        if (HP <= 0) CrystalMineComplete(); 
    }

    public void CrystalMineComplete()
    {
        Goblin.instance.target = null;
        Goblin.instance.isMining = false;
        //Goblin.instance.agent.SetDestination(Goblin.instance.HomePos.transform.position);
        this.gameObject.SetActive(false);
        HPBar.SetActive(false);
        QuestManager.instance.CrystalMineOnComplete();
    }
}
