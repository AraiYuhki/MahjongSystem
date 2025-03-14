namespace Xeon.MahjongSystem
{
    public class ScoreData
    {
        private int? score;
        public int Tier { get; private set; }
        public int Point { get; private set; }
        public int PlayerFinishScore { get; private set; }
        public (int fromPlayer, int fromHost) PlayerPickScore { get; private set; }
        public int HostFinishScore { get; private set; }
        public int HostPickScore { get; private set; }
        public bool IsHost { get; private set; }

        public ScoreData(int tier, int point, int playerFinishScore, (int fromPlayer, int fromHost) playerPickScore, int hostFinishScore, int hostPickScore)
        {
            Tier = tier;
            Point = point;
            PlayerFinishScore = playerFinishScore;
            PlayerPickScore = playerPickScore;
            HostFinishScore = hostFinishScore;
            HostPickScore = hostPickScore;
        }

        public override string ToString()
        {
            return $"{GetTierName()} {Point}符";
        }

        public string GetTierName()
        {
            if ((Tier == 3 && Point >= 7) || (Tier == 4 && Point >= 4))
                return "満貫";
            if (Tier <= 4)
                return $"{Tier}翻";
            if (Tier == 5)
                return "満貫";
            if (Tier == 6 || Tier == 7)
                return "跳満";
            if (8 <= Tier && Tier <= 10)
                return "倍満";
            if (Tier == 11 || Tier == 12)
                return "三倍満";
            if (13 <= Tier && Tier < 26)
                return "役満";
            if (26 <= Tier && Tier < 39)
                return "ダブル役満";
            return "トリプル役満";

        }
    }
}
