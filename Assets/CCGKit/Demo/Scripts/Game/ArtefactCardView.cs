using CCGKit;
using TMPro;
using UnityEngine;

public class ArtefactCardView : CardView
{
    [SerializeField]
    protected TextMeshPro defenseText;

    public Stat defenseStat { get; protected set; }
    
    public override bool CanBePlayed(DemoHumanPlayer owner)
    {
        return base.CanBePlayed(owner) && owner.playerInfo.namedZones["Artefactos"].cards.Count < owner.playerInfo.namedZones["Artefactos"].maxCards;
    }

    public override void PopulateWithInfo(RuntimeCard card)
    {
        base.PopulateWithInfo(card);
        defenseStat = card.namedStats["Life"];
        defenseText.text = defenseStat.effectiveValue.ToString();

        defenseStat.onValueChanged += (oldValue, newValue) => { defenseText.text = defenseStat.effectiveValue.ToString(); };
    }
    
    public override void PopulateWithLibraryInfo(Card card)
    {
        base.PopulateWithLibraryInfo(card);
        defenseText.text = card.stats[1].effectiveValue.ToString();
    }
}