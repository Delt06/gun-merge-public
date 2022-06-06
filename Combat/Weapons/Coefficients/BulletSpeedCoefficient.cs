namespace Combat.Weapons.Coefficients
{
	public sealed class BulletSpeedCoefficient : NonNegativeCoefficient, IBulletSpeedModifier
	{
		public float Affect(float bulletSpeed) => bulletSpeed * Value;
	}
}