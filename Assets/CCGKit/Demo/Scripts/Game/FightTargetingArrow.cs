// Copyright (C) 2016-2019 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using CCGKit;

public class FightTargetingArrow : TargetingArrow
{
    public RuntimeZone opponentBoardZone;

    public void End(BoardCreature creature)
    {
        if (!startedDrag)
        {
            return;
        }

        startedDrag = false;

        creature.ResolveCombat();
        Destroy(gameObject);
    }

    public override void OnCardSelected(BaseBoardCard boardCard)
    {
        if (targetType == EffectTarget.AnyPlayerOrCreature ||
            targetType == EffectTarget.TargetCard ||
            (targetType == EffectTarget.PlayerOrPlayerCreature && boardCard.tag == "PlayerOwned") ||
            (targetType == EffectTarget.OpponentOrOpponentCreature && boardCard.tag == "OpponentOwned") ||
            (targetType == EffectTarget.PlayerCard && boardCard.tag == "PlayerOwned") ||
            (targetType == EffectTarget.OpponentCard && boardCard.tag == "OpponentOwned"))
        {
            var opponentHasProvoke = OpponentBoardContainsProvokingCreatures();
            if (!opponentHasProvoke || (opponentHasProvoke && boardCard.card.HasKeyword("Provoke")))
            {
                selectedCard = boardCard;
                selectedPlayer = null;
                CreateTarget(boardCard.transform.position);
            }
        }
    }

    public override void OnCardUnselected(BaseBoardCard boardCard)
    {
        if (selectedCard == boardCard)
        {
            Destroy(target);
            selectedCard = null;
        }
    }

    public override void OnPlayerSelected(PlayerAvatar player)
    {
        if (targetType == EffectTarget.AnyPlayerOrCreature ||
            targetType == EffectTarget.TargetPlayer ||
            (targetType == EffectTarget.PlayerOrPlayerCreature && player.tag == "PlayerOwned") ||
            (targetType == EffectTarget.OpponentOrOpponentCreature && player.tag == "OpponentOwned") ||
            (targetType == EffectTarget.Player && player.tag == "PlayerOwned") ||
            (targetType == EffectTarget.Opponent && player.tag == "OpponentOwned"))
        {
            var opponentHasProvoke = OpponentBoardContainsProvokingCreatures();
            if (!opponentHasProvoke)
            {
                selectedPlayer = player;
                selectedCard = null;
                CreateTarget(player.transform.position);
            }
        }
    }

    public override void OnPlayerUnselected(PlayerAvatar player)
    {
        if (selectedPlayer == player)
        {
            Destroy(target);
            selectedPlayer = null;
        }
    }

    protected bool OpponentBoardContainsProvokingCreatures()
    {
        var provokeCards = opponentBoardZone.cards.FindAll(x => x.HasKeyword("Provoke"));
        return provokeCards.Count > 0;
    }
}