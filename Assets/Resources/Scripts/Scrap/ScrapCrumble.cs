using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    public class ScrapCrumble : MonoBehaviour
    {
        private List<GameObject> currentFragments;
        //private bool shouldCrumble;

        public void InitialCrumble(List<GameObject> fragments)
        {
            currentFragments = new List<GameObject>(fragments);
            //shouldCrumble = isCrumble;
        }

        public void SeparateScrap()// objectPool
        {
            Debug.Log("+");
            if (currentFragments != null)
            {      
                foreach (GameObject fragment in currentFragments)
                {
                    Instantiate(fragment, transform.position, transform.rotation);
                }
            }
            
        }
    }
}

