using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))] // Ceci permet de vérifier qu'il y a bien un Renderer sur l'objet sur lequel on a mis notre cardBehaviour. Plus besoin de vérifier par la suite. 

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 scaleOnFocus = Vector3.one * 1.5f; // Quand la souris passe dessus, change le scale. Vector3.one = 1-1-1
    [SerializeField] private float changeColorTime = 1f;
    private Vector3 memoScale;

    private Color color;
    [SerializeField] private Color baseColor = Color.gray;
    public int IndexColor { get; private set; } 

    public bool IsFaceUp { get; private set; } = false;

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

    private void OnMouseDown()
    {
        manager.CardIsClicked(this); // 
    }

    public void Initialize(Color color, int indexColor, CardsManager manager)
    {
        this.color = color;
        IndexColor = indexColor;
        this.manager = manager;

        // // Temporaire. Sera supprimé.
        // ChangeColor(color); // Ceci faisait en sorte que la couleur soit apparente dès l'initialisation. 

        ChangeColor(baseColor);
        IsFaceUp = false; // Attention ! Comme on a une coroutine juste au-dessus, IsFaceUp se fait tout de suite alors que la coroutine n'est pas encore finie. 
    }

    private void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    public void FaceUp()
    {
        StartCoroutine(ChangeColorWithLerp(color));
        // ChangeColor(color);
        IsFaceUp = true;
    }

    public void FaceDown(float delay = 0f)
    {
        StartCoroutine(ChangeColorWithLerp(baseColor, delay));
        // ChangeColor(baseColor);
        IsFaceUp = false;
    }

    private IEnumerator ChangeColorWithLerp(Color color, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        float chrono = 0f;
        Color startColor = GetComponent<Renderer>().material.color;

        while (chrono < changeColorTime)
        {
            chrono += Time.deltaTime;

            ChangeColor(Color.Lerp(startColor, color, chrono / changeColorTime));

            yield return new WaitForEndOfFrame(); // Signifie "attends jusqu'à la fin de la frame". On aurait pu aussi écrire yield return null.
        }

        ChangeColor(color);
    }
}
