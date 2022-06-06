namespace Combat.Weapons.Coefficients
{
	public sealed class DamageCoefficient : NonNegativeCoefficient, IDamageModifier
	{
		public float Affect(float damage) => damage * Value;
	}
}