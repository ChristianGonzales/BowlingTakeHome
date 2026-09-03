namespace WeInfuseTakeHome
{
    public class FrameScorer
    {
        public int?[] GetFrameScores(string[] input)
        {
            var frames = new List<int?>();
            var rolls = new Queue<string>(input);
            var curFrame = 0;

            if (rolls.Count == 0)
                return frames.ToArray();

            do
            {
                if (rolls.TryDequeue(out var firstRoll))
                {
                    if (firstRoll == "X")
                    {
                        var bonusRolls = rolls.Take(2);

                        if (bonusRolls.Count() != 2)
                        {
                            // Incomplete Frame - No bonus rolls
                            frames.Add(null);
                        }
                        else
                        {
                            var scrapBonus = 0;
                            var score = bonusRolls.Aggregate(10, (total, next) =>
                            {
                                if (int.TryParse(next, out var bonus))
                                {
                                    scrapBonus = bonus;
                                    return total + bonus;
                                }
                                else
                                {
                                    return total + 10 - scrapBonus;
                                }
                            });
                            frames.Add(score);
                        }

                        curFrame++;

                        if (curFrame > 9)
                            break;
                        else
                            continue;
                    }
                }

                if (rolls.TryDequeue(out var secondRoll))
                {
                    if (secondRoll == "/")
                    {
                        if (!rolls.TryPeek(out var bonusRoll))
                        {
                            // Incomplete Frame - No bonus roll
                            frames.Add(null);
                        }
                        else
                        {
                            if (int.TryParse(bonusRoll, out var bonus))
                            {
                                frames.Add(10 + bonus);
                            }
                            else
                            {
                                frames.Add(20);
                            }
                        }

                        curFrame++;

                        if (curFrame > 9)
                            break;
                        else
                            continue;
                    }
                }
                else
                {
                    // Incomplete Frame - No second roll
                    frames.Add(null);
                    curFrame++;
                    break;
                }

                var first = int.Parse(firstRoll);
                var second = int.Parse(secondRoll);

                frames.Add(first + second);
                curFrame++;
            } while (rolls.Count > 0);

            return frames.ToArray();
        }
    }
}
