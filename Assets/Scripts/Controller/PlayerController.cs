using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

class PlayerController : Singleton<PlayerController>
{
    public List<CharacterController> PartyMembers = new();
    public List<MaskController> UnassignedMasks = new();
    public List<CandyController> Candies = new();

    public void AddPartyMember(CharacterController character)
    {
        if (!PartyMembers.Contains(character))
        {
            PartyMembers.Add(character);
        }
    }

    public void AddMask(MaskController mask)
    {
        if (!UnassignedMasks.Contains(mask))
        {
            UnassignedMasks.Add(mask);
        }
    }

    public void FeedPartMemberCandy(CharacterController character, CandyController candy)
    {
        if (PartyMembers.Contains(character) && Candies.Contains(candy))
        {
            character.CurrentMotivation += candy.Data.MotivationGain;
            if (character.CurrentMotivation > character.GetCurrentMaxMotivation())
            {
                character.CurrentMotivation = character.GetCurrentMaxMotivation();
            }
            Candies.Remove(candy);
            Destroy(candy.gameObject);
        }
    }

    public void AttachMaskToCharacter(MaskController mask, CharacterController character)
    {
        if (UnassignedMasks.Contains(mask) && PartyMembers.Contains(character))
        {
            character.Mask = mask;
            UnassignedMasks.Remove(mask);
        }
    }

    public void RemoveMaskFromCharacter(CharacterController character)
    {
        if (PartyMembers.Contains(character))
        {
            UnassignedMasks.Add(character.Mask);
            character.Mask = null;
        }
    }


    public List<MaskController> GetActiveMasks() => PartyMembers.Select(m => m.Mask).NotNull().ToList();

    public List<(FactionData, int)> GetActiveFactionsAndCount()
    {
        var factionCount = new Dictionary<FactionData, int>();
        foreach (var mask in GetActiveMasks())
        {
            var faction = mask.Data.Faction;
            if (factionCount.ContainsKey(faction))
            {
                factionCount[faction]++;
            }
            else
            {
                factionCount[faction] = 1;
            }
        }

        return factionCount.Where(e => e.Value >= 2).Select(kv => (kv.Key, kv.Value)).ToList();       
    }

    public List<(TraitData, int)> GetActiveTraitsAndCount()
    {
        var traitCount = new Dictionary<TraitData, int>();
        foreach (var mask in GetActiveMasks())
        {
            foreach (var trait in mask.Data.Traits)
            {
                if (traitCount.ContainsKey(trait))
                {
                    traitCount[trait]++;
                }
                else
                {
                    traitCount[trait] = 1;
                }
            }
        }

        return traitCount.Where(e => e.Value >= 2).Select(kv => (kv.Key, kv.Value)).ToList();
    }

    public BuffData GetPossibleBuffFromFactionsFor(CharacterController character)
    {
        if (character.Mask == null)
            return null;

        var activeFactions = GetActiveFactionsAndCount();

        var result = activeFactions.FirstOrDefault(f => f.Item1 == character.Mask.Data.Faction);

        if (result.Item1 != null)
        {
            return result.Item1.GetBuffDataForUnitCount(result.Item2);
        }

        return null;
    }

    public BuffData GetPossibleBuffFromTraitsFor(CharacterController character)
    {
        if (character.Mask == null)
            return null;

        var activeTraits = GetActiveTraitsAndCount();

        foreach (var trait in character.Mask.Data.Traits)
        {
            var result = activeTraits.FirstOrDefault(t => t.Item1 == trait);

            if (result.Item1 != null)
            {
                return result.Item1.GetBuffDataForUnitCount(result.Item2);
            }
        }

        return null;
    }

    public int GetUnitCountForFaction(FactionData faction)
    {
        return GetActiveMasks().Count(m => m.Data.Faction == faction);
    }
}
