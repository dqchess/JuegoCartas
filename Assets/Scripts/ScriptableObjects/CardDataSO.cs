using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardData", order = 1)]
public class CardDataSO : ScriptableObject
{
    public AudioClip Entrada;
    public AudioClip Activacion;
    public AudioClip Muerte;

    public Sprite ImagenMano;
    public Sprite ImagenTablero;

    public Sprite ImagenFondoMano;
    public Sprite ImagenFondoTablero;
    public Color TinteFondo = Color.white;

}