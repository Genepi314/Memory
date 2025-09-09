using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    const float CARD_SIZE = 1.0f; // Ou 1-1-1 en Vector3 en réalité
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 2;
    [SerializeField] private float gap = 0.5f;
    [SerializeField] private CardBehaviour cardPrefab; // Et plus de type GameObject ! Ca évite d'avoir recours au GetComponent par la suite. Et aussi, on s'assure que notre cardPrefab a bien un script CardBehaviour. 
    [SerializeField] private Color[] colors;

    private List<CardBehaviour> deck = new(); // Comme la liste n'est pas serialized, il faut l'instancier. 
    [SerializeField] private CardsManager cardsManager;

    private int i;
    private int j;


    private void Start()
    {
        if ((rows * columns) % 2 != 0)
        {
            Debug.LogError("Uneven number of cards.");
            return;
        }

        if (colors.Length < rows * columns / 2)
        {
            Debug.LogError("There aren't enough different colors for the number of cards you've got.");
        }
        ObjectsCreation(); // Crée tous les objets sans qu'ils aient encore de lien entre eux. 
        ObjectInitialization(); // Initialise le CardsManager, qui va s'occuper de la distribution des couleurs. 
    }
    private void ObjectsCreation()
    {
        Vector3 position;
        for (float x = 0f; x < columns * (CARD_SIZE + gap); x += CARD_SIZE + gap)
        {
            for (float z = 0f; z < rows * (CARD_SIZE + gap); z += CARD_SIZE + gap)
            {
                position = transform.position + Vector3.right * x + Vector3.forward * z;
                deck.Add(Instantiate(cardPrefab, position, Quaternion.identity)); // Chaque card instanciée est ajoutée à la liste deck.
            }
        }

        //// Ma proposition, à terminer pour voir.
        // for (i = 0; i < rows; i++)
        // {
        //     for (j = 0; j < columns; j++)
        //     {

        //         Instantiate(cardPrefab, transform.position + new Vector3(i, j + j * gap , 0), Quaternion.identity); //// Par défaut, l'objet instancié est positionné en 0-0-0. Quaternion est une façon plus précise que Euler d'exprimer un angle. 
        //     }
        // }

        cardsManager = Instantiate(cardsManager); // Instatiate crée un clone de cardManager. On dit donc que cardsManager devient son propre clone DANS LA SCENE. Tout ça permet d'avoir une variable, qui contient d'abord le préfab, et qui après cette ligne, contient le clone instancié dans la scène. 
    }

    private void ObjectInitialization()
    {
        cardsManager.Initialize(deck, colors);
    }
}

