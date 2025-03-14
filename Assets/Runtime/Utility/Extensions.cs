using System.Collections.Generic;
using System.Linq;

namespace Xeon.MahjongSystem
{
    public static class Extensions
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> self)
            => self.OrderBy(_ => UnityEngine.Random.Range(0, float.MaxValue));

        public static T Random<T>(this IEnumerable<T> self)
            => self.Randomize().First();
    }
}
