using System.Collections.Generic;
using System.Linq;
using CCGKit;
using UnityEngine;
using UnityEngine.Rendering;

public class ArtefactZoneController : MonoBehaviour
{
    [SerializeField] private Transform[] cardHolders;
    [SerializeField] private GameObject artefactoBoardPrefab;

    private Dictionary<Transform, BoardArtefact> holdersAndCards = new Dictionary<Transform, BoardArtefact>();

    private void Awake()
    {
        holdersAndCards = cardHolders.ToDictionary(x => x, x => (BoardArtefact)null);
    }

    public BoardArtefact AddCard(RuntimeCard card)
    {
        if (card.cardType.name != "Artefacto") return null;

        Transform spot = FindSpot();
        if (spot == null) return null;

        GameObject go = Instantiate(artefactoBoardPrefab, spot.transform.position, Quaternion.identity);

        var boardArtefact = go.GetComponent<BoardArtefact>();
        boardArtefact.PopulateWithInfo(card);
        holdersAndCards[spot] = boardArtefact;
        go.GetComponent<SortingGroup>().sortingOrder = holdersAndCards.Keys.ToList().IndexOf(spot);
        return boardArtefact;
    }

    public void RemoveCard(RuntimeCard card)
    {
        var empties = holdersAndCards.Where(x => x.Value != null && x.Value.card.instanceId == card.instanceId).ToArray();
        if (empties.Length == 0)
            return;

        Destroy(holdersAndCards[empties.First().Key].gameObject);
        holdersAndCards[empties.First().Key] = null;
    }

    private Transform FindSpot()
    {
        var empties = holdersAndCards.Where(x => x.Value == null).ToArray();
        return empties.Length == 0 ? null : empties.First().Key;
    }
}