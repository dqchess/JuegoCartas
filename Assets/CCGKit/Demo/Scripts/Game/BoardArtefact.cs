using System;
using CCGKit;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class BoardArtefact : BaseBoardCard
{
    [SerializeField] protected TextMeshPro healthText;
    public Stat healthStat { get; protected set; }
    
    protected Action<int, int> onHealthStatChangedDelegate;

    protected virtual void OnDestroy()
    {
        healthStat.onValueChanged -= onHealthStatChangedDelegate;
    }
    
    public override void PopulateWithInfo(RuntimeCard card)
    {
        this.card = card;

        var gameConfig = GameManager.Instance.config;
        var libraryCard = gameConfig.GetCard(card.cardId);
        cardData = libraryCard.cardData;
        Assert.IsNotNull(libraryCard);
        nameText.text = libraryCard.name;

        healthStat = card.namedStats["Life"];
        healthText.text = healthStat.effectiveValue.ToString();

        onHealthStatChangedDelegate = (oldValue, newValue) =>
        {
            UpdateStatText(healthText, healthStat);
        };
        healthStat.onValueChanged += onHealthStatChangedDelegate;
        
        gameObject.name = $"{nameText.text} {card.instanceId}";

        backgorundSprite.sprite = libraryCard.cardData.ImagenFondoTablero;
        backgorundSprite.color = libraryCard.cardData.TinteFondo;
        pictureSprite.sprite = libraryCard.cardData.ImagenTablero;
        var material = libraryCard.GetStringProperty("Material");
        if (!string.IsNullOrEmpty(material))
        {
            pictureSprite.material = Resources.Load<Material>(string.Format("Materials/{0}", material));
        }
    }
    public override void SetHighlightingEnabled(bool enabled)
    {
        glowSprite.enabled = enabled;
        shadowSprite.enabled = !enabled;
    }

    private void UpdateStatText(TextMeshPro text, Stat stat)
    {
        text.text = stat.effectiveValue.ToString();
        if (stat.effectiveValue > stat.originalValue)
        {
            text.color = Color.green;
        }
        else if (stat.effectiveValue < stat.originalValue)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
        var sequence = DOTween.Sequence();
        sequence.Append(text.transform.DOScale(new Vector3(1.4f, 1.4f, 1.0f), 0.4f));
        sequence.Append(text.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.2f));
        sequence.Play();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.parent != null)
        {
            var targetingArrow = collider.transform.parent.GetComponent<TargetingArrow>();
            if (targetingArrow != null)
            {
                targetingArrow.OnCardSelected(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.parent != null)
        {
            var targetingArrow = collider.transform.parent.GetComponent<TargetingArrow>();
            if (targetingArrow != null)
            {
                targetingArrow.OnCardUnselected(this);
            }
        }
    }
}