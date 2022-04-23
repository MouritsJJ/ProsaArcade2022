using System.Text.Json;

namespace NipNap.Core
{
    public class NipNapper
    {
        private Dictionary<string, List<string>> Questions = new Dictionary<string, List<string>>();
        private List<string> Results = new List<string>();
        private Random Rng = new Random();
        private int Chance = 0;
        public int Counter { get; private set; } = 0;

        public NipNapper()
        {
            LoadResults();
        }

        public bool AskQuestion()
        {
            // Find question
            int i = 0;
            int q = Rng.Next(0, Questions.Keys.Count);
            Console.WriteLine("\n" + Questions.ElementAt(q).Key);
            foreach (string choice in Questions.ElementAt(q).Value)
                Console.WriteLine("\t" + ++i + ". " + choice);
            
            // Ask question
            int answer = 0;
            while (answer < 1 || answer > Questions.ElementAt(q).Value.Count)
            {
                Console.Write("Choice: ");
                bool correct = int.TryParse(Console.ReadLine(), out answer);
                if (!correct) answer = 0;
            }

            // Convert answer to number
            foreach (char c in Questions.ElementAt(q).Value[answer - 1]) Counter += (int)c;

            // Move question to asked list
            Questions.Remove(Questions.ElementAt(q).Key);

            // Check if we are done
            if (Rng.Next(0, 100) < Chance) return true;
            Chance += 10;
            return false;
        }

        public void Reset()
        {
            Questions = new Dictionary<string, List<string>>();
            LoadQuestions();
            Chance = 0;
        }
        public void GetResult()
        {
            string res = Results[Counter % Results.Count];
            foreach (char c in res) Console.Write("-");
            Console.Write("\n\n");
            Console.Write(res);
            Console.Write("\n\n");
            foreach (char c in res) Console.Write("-");
        }

        private void LoadQuestions()
        {
            using (StreamReader sr = new StreamReader("res/Questions.json"))
            {
                var json = JsonSerializer.Deserialize<JsonElement>(sr.ReadToEnd());
                var questions = json.GetProperty("Questions");
                var iter = questions.EnumerateObject();
                while (iter.MoveNext())
                {
                    Questions.Add(iter.Current.Name, JsonSerializer.Deserialize<List<string>>(iter.Current.Value));
                }
            }
        }

        private void LoadResults()
        {
            using (StreamReader sr = new StreamReader("res/Results.txt"))
            {
                Results = sr.ReadToEnd().Split("\n").ToList();
            }
        }
    
    }
}