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
        int OtherRandomCardIndex; // Index de l'autre carte de la paire qui recevra la meme couleur.
        List<int> removedCards = new List<int>();
        List<int> removedColors = new List<int>();
 
        for (int i = 0; i < deck.Count; i++) // Attention, si on n'avait pas eu recours à .Count et qu'on avait directement mis les instances d'objets contenus dans liste, il aurait fait des instanciations automatiques supplémentaires !!! Unity...
        {
            if (!removedCards.Contains(i))
            {
                // On tient une liste d'index des cartes qui ont déjà été sélectionnées:
                removedCards.Add(i);
 
                // On choisit une carte au hasard:
                OtherRandomCardIndex = Random.Range(1, deck.Count);
 
                // On vérifie que la carte random n'a pas déjà été sélectionnée :
                while (removedCards.Contains(OtherRandomCardIndex)) // Si l'index random est une carte qui a déjà reçu une couleur, on relance le random.
                {
                    OtherRandomCardIndex = Random.Range(1, deck.Count);
                }
                removedCards.Add(OtherRandomCardIndex);
 
                // On attribue une couleur au hasard et on vérifie ensuite qu'elle n'a pas déjà été attribuée:
                randomColorInt = Random.Range(0, colors.Length);
                while (removedColors.Contains(randomColorInt))
                {
                    randomColorInt = Random.Range(0, colors.Length);
                }
                removedColors.Add(randomColorInt);
 
                // On donne cette couleur aux deux cartes sélectionnées:
                deck[i].Initialize(colors[randomColorInt], randomColorInt, this);
                deck[OtherRandomCardIndex].Initialize(colors[randomColorInt], randomColorInt, this);
            }
        }
    }
}
 