using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Header("Parameters")]
    public WeaponController weaponController;
    public float ammoAmount;

    Pickup m_Pickup;

    void Start()
    {
        m_Pickup = GetComponent<Pickup>();
        DebugUtility.HandleErrorIfNullGetComponent<Pickup, AmmoPickup>(m_Pickup, this, gameObject);

        // Subscribe to pickup action
        m_Pickup.onPick += OnPicked;
    }

    void OnPicked(PlayerCharacterController player)
    {
        var weaponManager = player.WeaponsManager;
        if (weaponManager)
        {
            weaponManager.GetWeapon(weaponController).PickupAmmo(ammoAmount);
            
            m_Pickup.PlayPickupFeedback();

            Destroy(gameObject);
        }
    }
}