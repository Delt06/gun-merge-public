using Combat.Teams;
using DELTation.DIFramework;
using DELTation.DIFramework.Containers;

namespace Dependencies
{
	public class CompositionRoot : DependencyContainerBase
	{
		protected override void ComposeDependencies(ContainerBuilder builder)
		{
			builder.Register(new RivalToAllTeamFactory());
		}
	}
}