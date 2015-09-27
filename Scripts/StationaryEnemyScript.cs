using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class StationaryEnemyScript : MonoBehaviour
{
	private WeaponScript[] weapons;
	
	void Awake()
	{
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<WeaponScript>();
	}
	
	void Update()
	{
		// Auto-fire
		foreach (WeaponScript weapon in weapons)
		{
			if (weapon != null && weapon.enabled && weapon.CanAttack)
			{
				weapon.Attack(true);
			}
		}
	}
}