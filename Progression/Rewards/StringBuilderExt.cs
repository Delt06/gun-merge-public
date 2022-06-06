using System.Text;
using UnityEngine;

namespace Progression.Rewards
{
	public static class StringBuilderExt
	{
		public static StringBuilder AppendRatioAsPercentage(this StringBuilder stringBuilder, float ratio)
		{
			var percentage = Mathf.RoundToInt(ratio * 100);
			stringBuilder.Append(percentage).Append("%");
			return stringBuilder;
		}
	}
}