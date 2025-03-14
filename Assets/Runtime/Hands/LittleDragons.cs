using System.Linq;

namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 小三元
    /// </summary>
    public class LittleDragons : IHands
    {
        public int Tier => 2;
        public string Name => "小三元";
        public HandType Type => HandType.LittleDragons;
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;
            // 雀頭は三元牌
            if (!data.HeadTile.IsDragon) return false;
            // 成立している面子のうち二つが三元牌で構成されている
            return data.Elements.Count(element => element.IsPairs && element.TileType.IsDragon()) == 2;
        }
    }
}
