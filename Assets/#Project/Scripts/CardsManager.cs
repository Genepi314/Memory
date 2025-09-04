using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    private List<CardBehaviour> deck;
    private Color[] colors;
    public void Initialize(List<CardBehaviour> deck, Color[] colors) // /!\ deck n'est en fait pas celui de GameInitializer, mais ça aide à la compréhension il parait. 
    {
        this.colors = colors;
        this.deck = deck;

        int randomColorInt;
        for (int i = 0; i < deck.Count; i++) // Attention, si on n'avait pas eu recours à .Count et qu'on avait directement mis les instances d'objets contenus dans liste, il aurait fait des instanciations automatiques supplémentaires !!! Unity...
        {
            randomColorInt = Random.Range(0, colors.Length);
            deck[i].Initialize(colors[randomColorInt], randomColorInt, this);
        }
    }
}
