using System;
using System.Collections;
using UnityEngine;
using AI;
using System.Collections.Generic;
using Worm;
using Random = UnityEngine.Random;

namespace WorkerChickenAI
{
    public class WorkerChickenAI : BaseAI
    {
        private bool isStart = true;
        public int currentStackCount;
        public bool isAttack = false;
        private List<GameObject> stackWorms = new List<GameObject>();
        [SerializeField] private WorkerAnimations workerAnimation;

        protected override void Awake()
        {
            base.Awake();
            baseTarget = GameObject.FindGameObjectWithTag("MotherChicken").transform;
        }

        void Start()
        {
            speed = WorkerChickenProperties.Instance.speed;
            SetSpeed();
            workerAnimation.RunAanimation();
        }

        private void Update()
        {
            if (WormSpawner.Instance.fullSlots.Count > 0 && isStart)
            {
                int randomWorm = Random.Range(0, WormSpawner.Instance.fullSlots.Count);
                GoTarget(WormSpawner.Instance.fullSlots[randomWorm].transform);
                WormSpawner.Instance.fullSlots.Remove(WormSpawner.Instance.fullSlots[randomWorm]);
                isStart = false;
            }
            if (aIPath.reachedEndOfPath)
            {
                
                if (currentStackCount < WorkerChickenProperties.Instance.wormStackCount)
                {
                    StartCoroutine(AttackDelay());
                }
                else
                {
                    ReturnBase();
                    if (Vector3.Distance(transform.position, baseTarget.position) < 0.5f)
                    {
                        workerAnimation.IdleAnimation();
                    }
                }

            }


        }

        IEnumerator AttackDelay()
        {
            if (!isAttack)
            {
                isAttack = true;
                Attack();
                workerAnimation.AttackAnimation();
                yield return new WaitForSeconds(1.5f);
                isAttack = false;
                currentStackCount++;
                FindWorm();

            }

        }

        void FindWorm()
        {
            if (currentStackCount < WorkerChickenProperties.Instance.wormStackCount)
            {
                int randomWorm = Random.Range(0, WormSpawner.Instance.fullSlots.Count);
                ChangeTarget(WormSpawner.Instance.fullSlots[randomWorm].transform);
                aIPath.ReachedEndOfPathBoolFalse();
                workerAnimation.RunAanimation();
            }
        }
        public void PickingWorm(GameObject worm)
        {
            if (currentStackCount < WorkerChickenProperties.Instance.wormStackCount)
            {
                stackWorms.Add(worm);
            }
        }
        void GiveWorms()
        {
            workerAnimation.IdleAnimation();
            for (int i = 0; i < stackWorms.Count; i++)
            {
                stackWorms.Remove(stackWorms[0]);
            }
            currentStackCount = 0;
        }
    }

}
