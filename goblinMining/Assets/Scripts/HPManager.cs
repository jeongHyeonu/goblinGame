using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public static HPManager instance;

    [SerializeField] public GameObject Goblin;
    [SerializeField] public List<GameObject> Cristal;
    private Vector3 goblinPos;
    private Vector3[] cristalPos = new Vector3[100];

    [SerializeField] public GameObject GoblinHPBar;
    [SerializeField] public GameObject CristalHPBar;
    private GameObject[] CristalHPBarLIst = new GameObject[100];

    enum _type
    {
        goblin,
        cristal
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        for(int i = 0; i < Cristal.Count; i++)
        {
            CristalHPBarLIst[i] = Instantiate(CristalHPBar,this.transform);
            Cristal[i].GetComponent<Crystal>().HPBar = CristalHPBarLIst[i];
            CristalHPBarLIst[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        goblinPos = Goblin.gameObject.transform.position;
        goblinPos = new Vector3(goblinPos.x, goblinPos.y + 2f, goblinPos.z);
        GoblinHPBar.transform.position = goblinPos;

        for (int i = 0; i < Cristal.Count; i++)
        {
            CristalHPBarLIst[i].GetComponent<Slider>().value = (float)Cristal[i].GetComponent<Crystal>().HP / (float)Cristal[i].GetComponent<Crystal>().maxHP;
            cristalPos[i] = Cristal[i].transform.position;
            cristalPos[i] = new Vector3(cristalPos[i].x, cristalPos[i].y + 2f, cristalPos[i].z);
            CristalHPBarLIst[i].transform.position = cristalPos[i];
        }
    }

    //public void MakeNewHPBar(_type hpType)
    //{
    //    switch (hpType)
    //    {
    //        case _type.goblin:
    //            GameObject newHPBar = Instantiate(GoblinHPBar,this.gameObject.transform);
    //            break;
    //        case _type.cristal:
    //            newHPBar = Instantiate(CristalHPBar, this.gameObject.transform);
    //            break;
    //    }

    //}
}
