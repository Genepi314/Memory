using UnityEngine;

[RequireComponent(typeof(Renderer))] // Ceci permet de vérifier qu'il y a bien un Renderer sur l'objet sur lequel on a mis notre cardBehaviour. Plus besoin de vérifier par la suite. 

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 scaleOnFocus = Vector3.one * 1.5f; // Quand la souris passe dessus, change le scale. Vector3.one = 1-1-1
    private Vector3 memoScale;

    private Color color;
    private int indexColor;
    private CardsManager manager;
    private void OnMouseEnter()
    {
        // Debug.Log("Hum...");
        memoScale = transform.localScale; // Afin de préserver le scale, on mémorise son état avant OnMouseEnter.
        transform.localScale = scaleOnFocus;
    }

    private void OnMouseExit()
    {
        transform.localScale = memoScale;
    }

    public void Initialize(Color color, int indexColor, CardsManager manager)
    {
        this.color = color;
        this.indexColor = indexColor;
        this.manager = manager;
        // Temporaire. Sera supprimé.
        ChangeColor(color);
    }

    public void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}
