using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardsManager : MonoBehaviour
{
    [SerializeField] private float delayBeforeFaceDown = 1f;
    private VictoryManager victoryManager;
    private List<CardBehaviour> deck;
    private Color[] colors;
    private CardBehaviour memoCard = null; // On précise que c'est null pour être explicite.
    public void Initialize(List<CardBehaviour> deck, Color[] colors, VictoryManager victoryManager) // /!\ deck n'est en fait pas celui de GameInitializer, mais ça aide à la compréhension il parait.
    {
        this.colors = colors;
        this.deck = deck;
        this.victoryManager = victoryManager;

        int colorIndex;
        int cardIndex;

        List<int> colorsAlreadyInGame = new();
        List<CardBehaviour> cards = new(deck); // Ceci crée un clône de deck. Petit désavantage, ça consomme plus d'espace. Comme c'est en initialisation, ça passe. Contrairement à ma méthode. Le défaut de la mienne, ci-dessus, est l'utilisation du random, qui va demander probablement beaucoup d'itérations pour obtenir un tirage différent à chaque fois via le random.

        memoCard = null;

        foundPairs = 0;

        // // Ma proposition:

        // int OtherRandomCardIndex; // Index de l'autre carte de la paire qui recevra la meme couleur.
        // List<int> removedCards = new List<int>();
        // List<int> removedColors = new List<int>();
        // int randomColorInt;

        // for (int i = 0; i < deck.Count; i++) // Attention, si on n'avait pas eu recours à .Count et qu'on avait directement mis les instances d'objets contenus dans liste, il aurait fait des instanciations automatiques supplémentaires !!! Unity...
        // {
        //     if (!removedCards.Contains(i))
        //     {
        //         // On tient une liste d'index des cartes qui ont déjà été sélectionnées:
        //         removedCards.Add(i);

        //         // On choisit une carte au hasard:
        //         OtherRandomCardIndex = Random.Range(1, deck.Count);

        //         // On vérifie que la carte random n'a pas déjà été sélectionnée :
        //         while (removedCards.Contains(OtherRandomCardIndex)) // Si l'index random est une carte qui a déjà reçu une couleur, on relance le random.
        //         {
        //             OtherRandomCardIndex = Random.Range(1, deck.Count);
        //         }
        //         removedCards.Add(OtherRandomCardIndex);

        //         // On attribue une couleur au hasard et on vérifie ensuite qu'elle n'a pas déjà été attribuée:
        //         randomColorInt = Random.Range(0, colors.Length);
        //         while (removedColors.Contains(randomColorInt))
        //         {
        //             randomColorInt = Random.Range(0, colors.Length);
        //         }
        //         removedColors.Add(randomColorInt);

        //         // On donne cette couleur aux deux cartes sélectionnées:
        //         deck[i].Initialize(colors[randomColorInt], randomColorInt, this);
        //         deck[OtherRandomCardIndex].Initialize(colors[randomColorInt], randomColorInt, this);
        //     }
        // }

        for (int _ = 0; _ < deck.Count / 2; _++) // Le _ signifie qu'on ne se sert de la variable compteur que pour compter, on ne la modifiera pas.
        {
            colorIndex = Random.Range(0, colors.Length);

            while (colorsAlreadyInGame.Contains(colorIndex))
            {
                colorIndex = Random.Range(0, colors.Length);
            }
            colorsAlreadyInGame.Add(colorIndex);

            cardIndex = Random.Range(0, cards.Count);
            cards[cardIndex].Initialize(colors[colorIndex], colorIndex, this);
            cards.RemoveAt(cardIndex);

            cardIndex = Random.Range(0, cards.Count);
            cards[cardIndex].Initialize(colors[colorIndex], colorIndex, this);
            cards.RemoveAt(cardIndex);
        }
    }

    private int foundPairs = 0;
    public void CardIsClicked(CardBehaviour card)
    {
        if (card.IsFaceUp) return;
        card.FaceUp();

        if (memoCard != null)
        {
            if (card.IndexColor != memoCard.IndexColor)
            {
                // Debug.Log("Sorry, elles sont différentes.");
                memoCard.FaceDown(delayBeforeFaceDown); // delayBeforeFaceDown a été SerializeFieldé pour pouvoir être accesible dans l'inspecteur.
                card.FaceDown(delayBeforeFaceDown);
            }
            else
            {
                foundPairs++;
                if (foundPairs == deck.Count / 2)
                {
                    // Debug.Log("VictoryScene");
                    victoryManager.LaunchVictory();
                }
            }
            memoCard = null;
        }
        else
        {
            memoCard = card;
        }
    }
}
