using System.Collections.Generic;
using System.Linq;
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
        int randomOtherCardI;
        List<int> removedCards = new List<int>();
        
        for (int i = 0; i < deck.Count; i++) // Attention, si on n'avait pas eu recours à .Count et qu'on avait directement mis les instances d'objets contenus dans liste, il aurait fait des instanciations automatiques supplémentaires !!! Unity...
        {
            if (!removedCards.Contains(i))
            {
                // On vérifie que la carte random n'a pas déjà été sélectionnée :
                randomOtherCardI = Random.Range(0, deck.Count);
                while (removedCards.Contains(randomOtherCardI))
                {
                    randomOtherCardI = Random.Range(0, deck.Count);
                }

                randomColorInt = Random.Range(0, colors.Length);
                deck[i].Initialize(colors[randomColorInt], randomColorInt, this);
                removedCards.Add(i);
                deck[randomOtherCardI].Initialize(colors[randomColorInt], randomColorInt, this);
                removedCards.Add(randomOtherCardI);

            }


        }
    }
}
