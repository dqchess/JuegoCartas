public class ArtefactCardView : CardView
{
    public override bool CanBePlayed(DemoHumanPlayer owner)
    {
        return base.CanBePlayed(owner) && owner.playerInfo.namedZones["Artefactos"].cards.Count < owner.playerInfo.namedZones["Artefactos"].maxCards;
    }
}