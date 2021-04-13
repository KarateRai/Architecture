using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pickup : MonoBehaviour
{
    public TMP_Text pickup_Text;
    public int current_Pickup_Amount = 0;

    public void SetStartingPickupValue(int pickup_Amount)
    {
        current_Pickup_Amount = pickup_Amount;
    }

    public void AddPickups()
    {
        current_Pickup_Amount++;
        pickup_Text.text = current_Pickup_Amount.ToString();
    }
}
