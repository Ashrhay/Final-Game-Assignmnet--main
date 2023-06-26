using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public List<SpecialAbility> specialAbilities;
    public Button shoreUpButton;
    public Button moveOtherPlayersButton;

    // Add any additional character-specific properties or methods here

    public enum SpecialAbility
    {
        Engineer,
        Pilot,
        Navigator,
        Messenger,
        Diver,
        Explorer,
    }

    public void PerformAbility(int abilityIndex)
    {
        if (abilityIndex >= 0 && abilityIndex < specialAbilities.Count)
        {
            SpecialAbility ability = specialAbilities[abilityIndex];
            // Perform logic based on the selected ability
            switch (ability)
            {
                case SpecialAbility.Engineer:
                    //shoreUpButton.gameObject.SetActive(true);
                    //moveOtherPlayersButton.gameObject.SetActive(false);
                    break;
                case SpecialAbility.Explorer:
                    //shoreUpButton.gameObject.SetActive(false);
                    //moveOtherPlayersButton.gameObject.SetActive(false);
                    break;
                case SpecialAbility.Navigator:
                    //shoreUpButton.gameObject.SetActive(false);
                    //moveOtherPlayersButton.gameObject.SetActive(true);
                    break;
                case SpecialAbility.Pilot:
                    //shoreUpButton.gameObject.SetActive(false);
                    //moveOtherPlayersButton.gameObject.SetActive(false);
                    break;
                case SpecialAbility.Diver:
                    //shoreUpButton.gameObject.SetActive(false);
                    //moveOtherPlayersButton.gameObject.SetActive(false);
                    break;
                case SpecialAbility.Messenger:
                    //shoreUpButton.gameObject.SetActive(false);
                    //moveOtherPlayersButton.gameObject.SetActive(false);
                    break;
                default:
                    Debug.LogWarning("Unknown ability selected");
                    break;
            }
        }
        else
        {
            Debug.LogError("Invalid ability index");
        }
    }


}
