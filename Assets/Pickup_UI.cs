using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pickup_UI : MonoBehaviour
{
    public TMP_Text pickup_Text;

    private void Update()
    {
        pickup_Text.text = Player.pickup_amount.ToString();
    }
    
}
