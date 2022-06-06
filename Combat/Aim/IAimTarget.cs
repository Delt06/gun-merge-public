using Components;

namespace Combat.Aim
{
	public interface IAimTarget
	{
		IComponentProvider Target { get; }
	}
}