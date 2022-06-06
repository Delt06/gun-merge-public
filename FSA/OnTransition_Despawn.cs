using Plugins.FSA;
using Plugins.FSA.Components.Easy;
using Spawning;

namespace FSA
{
	public sealed class OnTransition_Despawn : EasyTransitionListener
	{
		protected override void TransitionAction(IFsa<EasyTrigger> fsa, IState<EasyTrigger> oldState,
			IState<EasyTrigger> newState)
		{
			_despawnable.Despawn();
		}

		public void Construct(IDespawnable despawnable) => _despawnable = despawnable;

		private IDespawnable _despawnable;
	}
}