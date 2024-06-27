using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    public class ScrapCrumble : MonoBehaviour
    {
        private List<GameObject> currentFragments;
        private bool shouldCrumble;

        public void InitialCrumble(List<GameObject> fragments, bool isCrumble)
        {
            currentFragments = new List<GameObject>(fragments);
            shouldCrumble = isCrumble;
        }

        public void SeparateScrap()
        {
            if (shouldCrumble)
            {
                foreach (GameObject fragment in currentFragments)
                {
                    Instantiate(fragment, transform.position, transform.rotation);
                }
            }
        }
    }
}


