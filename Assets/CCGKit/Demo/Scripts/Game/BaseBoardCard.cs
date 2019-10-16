using CCGKit;
using TMPro;
using UnityEngine;

public abstract class BaseBoardCard : MonoBehaviour
{
    public RuntimeCard card { get; protected set; }
    protected CardDataSO cardData;

    public GameObject fightTargetingArrowPrefab;

    [SerializeField]
    protected SpriteRenderer glowSprite;

    [SerializeField]
    protected SpriteRenderer shadowSprite;

    [SerializeField]
    protected SpriteRenderer shieldGlowSprite;

    [SerializeField]
    protected SpriteRenderer shieldShadowSprite;

    [SerializeField]
    protected SpriteRenderer shieldSprite;

    [SerializeField]
    protected SpriteRenderer pictureSprite;

    [SerializeField]
    protected SpriteRenderer backgorundSprite;
    [SerializeField]
    protected TextMeshPro nameText;

    [HideInInspector]
    public DemoHumanPlayer ownerPlayer;

    public abstract void PopulateWithInfo(RuntimeCard card);
}