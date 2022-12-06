using Advent_of_Code_2022;

namespace Day2
{
    internal class Program
    {
        private const int _WIN = 6, _LOSE = 0, _DRAW = 3;
        public enum Outcome
        {
            Rock = 1,
            Paper = 2,
            Scissor = 3,
        }

        public static Dictionary<string, Outcome> OutcomeDict = new Dictionary<string, Outcome>()
        {
            {"X", Outcome.Rock}, {"A", Outcome.Rock},
            {"Y", Outcome.Paper}, {"B", Outcome.Paper},
            {"Z", Outcome.Scissor}, {"C", Outcome.Scissor}
        };
        static void Main(string[] args)
        {
            string input = Input.Get();
            string[] rounds = input.Split("\r\n");
            int totalScore = 0;

            foreach (var round in rounds)
            {
                string opponent = round.Split(' ')[0];
                string me = round.Split(' ')[1];

                totalScore += Result(OutcomeDict[opponent], OutcomeDict[me]);
            }

            Console.WriteLine($"Total score:\n" +
                $"{totalScore}");

            int realTotalScore = 0;
            foreach (var round in rounds)
            {
                string opponent = round.Split(' ')[0];
                string me = round.Split(' ')[1];

                if (OutcomeDict[me] == Outcome.Rock)
                    realTotalScore += Result(OutcomeDict[opponent], GetAdvantage(OutcomeDict[opponent], false));
                else if (OutcomeDict[me] == Outcome.Paper)
                    realTotalScore += Result(OutcomeDict[opponent], OutcomeDict[opponent]);
                else if (OutcomeDict[me] == Outcome.Scissor)
                    realTotalScore += Result(OutcomeDict[opponent], GetAdvantage(OutcomeDict[opponent], true));
            }

            Console.WriteLine($"Using the correct descryption, the total score:\n" +
                $"{realTotalScore}");
        }

        public static int Result(Outcome opponent, Outcome me)
        {
            if (opponent == me)
                return _DRAW + (int)me;

            if (opponent == Outcome.Rock)
                return me == Outcome.Paper ? _WIN + (int)me : _LOSE + (int)me;

            if (opponent == Outcome.Paper)
                return me == Outcome.Scissor ? _WIN + (int)me : _LOSE + (int)me;

            if (opponent == Outcome.Scissor)
                return me == Outcome.Rock ? _WIN + (int)me : _LOSE + (int)me;

            throw new Exception();
        }

        public static Outcome GetAdvantage(Outcome outcome, bool winning)
        {
            if (outcome == Outcome.Rock)
                return winning ? Outcome.Paper : Outcome.Scissor;
            else if (outcome == Outcome.Paper)
                return winning ? Outcome.Scissor : Outcome.Rock;
            else
                return winning ? Outcome.Rock : Outcome.Paper;
        }
    }
}