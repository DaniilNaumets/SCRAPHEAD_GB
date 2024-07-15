using UnityEngine;

namespace Resources
{
    public class ScrapTurnOff : MonoBehaviour
    {
        public void TurnOff()
        {
            //transform.parent.gameObject.SetActive(false);//pool
            Destroy(transform.parent.gameObject);
        }
    }
}

