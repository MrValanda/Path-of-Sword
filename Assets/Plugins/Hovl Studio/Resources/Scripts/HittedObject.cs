using UnityEngine;
using UnityEngine.UI;

namespace Hovl_Studio.Resources.Scripts
{
	public class HittedObject : MonoBehaviour {

		public float startHealth = 100;
		private float health;
		public Image healthBar;
		// Use this for initialization
		void Start () {
			health = startHealth;
		}
	
		// Update is called once per frame
		void Update () {
		
		}

		public void TakeDamage(float amount)
		{
			health -= amount;
			healthBar.fillAmount = health / startHealth;
			if(health <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
