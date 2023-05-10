using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    static public QuestManager instance;

    [SerializeField] public GameObject Crystals;
    [SerializeField] public TextMeshProUGUI progress;

   int MaxCrystalCnt;
    int CrystalCnt;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        MaxCrystalCnt = Crystals.transform.childCount;
        CrystalCnt = 0;
        progress.text = "진행도 : " + CrystalCnt + " / " + MaxCrystalCnt;
    }

    public void CrystalMineOnComplete()
    {
        CrystalCnt++;
        progress.text = "진행도 : " + CrystalCnt + " / " + MaxCrystalCnt;
        if (CrystalCnt == MaxCrystalCnt) Goblin.instance.GotoHome();
    }
}
