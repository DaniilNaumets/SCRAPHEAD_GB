using ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    public class ScrapCrumble : MonoBehaviour
    {
        private List<GameObject> currentFragments;
        private bool shouldCrumble;
        private ObjectsPoolManager objectsPoolManager;

        public void InitialCrumble(List<GameObject> fragments, bool isCrumble)
        {
            currentFragments = new List<GameObject>(fragments);
            objectsPoolManager = FindObjectOfType<ObjectsPoolManager>();
            shouldCrumble = isCrumble;
        }

        public void SeparateScrap()
        {
            if (shouldCrumble)
            {
                foreach (GameObject fragment in currentFragments)
                {
                    objectsPoolManager.GetFromPool(fragment);
                }
            }
        }
    }
}


