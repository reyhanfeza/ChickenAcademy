using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ObjectPooling;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using Base.Runtime.Misc;
using TMPro;

namespace Worm
{
    public class WormSpawner : Singleton<WormSpawner>
    {
        [SerializeField] private float spawnInterval = 1f;
        [SerializeField] private float spawnDistance;
        [SerializeField] private Vector3 spawnerSize;
        [SerializeField] private TMP_Text[] wormLevelTexts;
        [SerializeField] private List<WormSlot> emptySlots;
        public List<WormSlot> fullSlots;

        private Transform _tr;

        protected override void Awake()
        {
            base.Awake();
            _tr = transform;
            for (var i = 0; i < emptySlots.Count; i++)
            {
                var wormSlot = emptySlots[i];
                wormSlot.Initialize(this, wormLevelTexts[i]);
            }

            StartCoroutine(SpawnOverTime());
        }

        private IEnumerator SpawnOverTime()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(spawnInterval);
            yield return waitForSeconds;
            while (true)
            {
                if (emptySlots.Count > 0)
                {
                    var worm = ObjectPooler.instance.SpawnFromPool("Worm", Vector3.zero, quaternion.identity);

                    var slot = emptySlots[0];
                    emptySlots.RemoveAt(0);
                    SetRandomSlotPositionInArea(slot);
                    slot.AssignWorm(worm.GetComponent<WormController>());
                    fullSlots.Add(slot);

                }
                yield return waitForSeconds;
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private void SetRandomSlotPositionInArea(WormSlot slot)
        {
            Vector3 randomPos;
            int safe = 0;
            do
            {
                safe++;
                randomPos.x = Random.Range(-spawnerSize.x / 2, spawnerSize.x / 2);
                randomPos.z = Random.Range(-spawnerSize.z / 2, spawnerSize.z / 2);
                randomPos.y = _tr.position.y;
            } while (fullSlots.Any(x => Vector3.Distance(x.tr.position, _tr.position + randomPos) <= spawnDistance) && safe < 9);

            slot.tr.position = _tr.position + randomPos;
        }

        public void EmptySlot(WormSlot wormSlot)
        {
            fullSlots.Remove(wormSlot);

            emptySlots.Add(wormSlot);

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 0.8f, .5f, 0.5f);
            Gizmos.DrawCube(transform.position, spawnerSize);
        }
    }
}
