using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goblin : MonoBehaviour
{
    static public Goblin instance;

    Camera cam;
    Animator animator;
    public NavMeshAgent agent;
    public LayerMask moveMask;
    public LayerMask interactiveMask;
    public GameObject target=null;
    Vector3 offset;

    // ��� ��
    [SerializeField] public GameObject HomePos;

    // ä�� ���� ����
    public bool isMining = false;
    private float miningRange = 2f;
    private float miningCooltime = 0f;
    private float maxMiningCooltime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cam = Camera.main;
        animator = this.GetComponent<Animator>();
        offset = cam.transform.position - this.transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, moveMask))
            {
                agent.SetDestination(hit.point);
                animator.SetTrigger("Walk");
                target = null;
                isMining = false;
            }

            if (Physics.Raycast(ray, out hit, 100, interactiveMask))
            {
                agent.SetDestination(hit.point);
                animator.SetTrigger("Walk");

                // ũ����Ż Ŭ����
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Crystal"))
                {
                    target = hit.collider.gameObject;
                    transform.LookAt(target.transform);
                }
                

            }

        }
        if (!agent.pathPending) // ������Ʈ�� �������� ��Ҵ���
        {
            if (agent.remainingDistance <= agent.stoppingDistance )
            {
                animator.SetTrigger("Idle");
            }
        }
        if (target != null) // Ŭ���� Ÿ���� null �� �ƴ϶��
        {
            if(target.layer == LayerMask.NameToLayer("Crystal"))
            {
                if (agent.remainingDistance <= miningRange)
                {
                    miningCooltime -= Time.deltaTime;
                    if (miningCooltime <= 0)
                    {
                        transform.LookAt(target.transform);
                        animator.SetTrigger("Attack");
                        miningCooltime = maxMiningCooltime;
                        isMining = true;
                        animator.SetTrigger("Idle");
                    }
                }
            }

        }
        cam.transform.position = transform.position + offset;
    }

    public void attk()
    {
        if(target!=null)
        target.GetComponent<Crystal>().Damaged(2);
    }
}
