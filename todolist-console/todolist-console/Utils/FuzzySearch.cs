namespace todolist_console.Utils
{
    public class FuzzySearch
    {
        public static int CalculateLevenshteinDistance(string s, string t)
        {
            int m = s.Length;
            int n = t.Length;

            int[,] d = new int[m + 1, n + 1];

            for (int i = 0; i <= m; i++)
            {
                d[i, 0] = i;
            }

            for (int j = 0; j <= n; j++)
            {
                d[0, j] = j;
            }

            for (int j = 1; j <= n; j++)
            {
                for (int i = 1; i <= m; i++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    int insertion = d[i, j - 1] + 1;
                    int deletion = d[i - 1, j] + 1;
                    int substitution = d[i - 1, j - 1] + cost;

                    d[i, j] = Math.Min(insertion, Math.Min(deletion, substitution));
                }
            }

            return d[m, n];
        }
    }
}
