using Events;
using UnityEngine;

namespace Combat.Characters.Events
{
	[CreateAssetMenu(menuName = GlobalEvent.AssetPath + "Character Death")]
	public sealed class CharacterDeathGlobalEvent : GlobalEvent<CharacterDeathArgs> { }
}