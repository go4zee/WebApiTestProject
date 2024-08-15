using System.Text;

namespace WebApiTestProject.Core
{
    public class StringGenerator : IStringGenerator
    {
        const string alphabets = "abcdefghijklmnopqrstuvwxyz";
        readonly List<string> _list = new List<string>();

        public StringGenerator()
        {

        }

        public async Task<List<string>> GetStringCombination(int stringMaxLength, int count)
        {
            StringCombinations(stringMaxLength, count, new StringBuilder());

            return _list;
        }

        private void StringCombinations(int stringMaxLength, int count, StringBuilder currSb)
        {
            if (_list.Count == count)
            {
                return;
            }

            if (currSb.Length == stringMaxLength)
            {
                _list.Add(currSb.ToString());
            }
            else
            {
                for (int i = 0; i < alphabets.Length; i++)
                {
                    var prevCombination = new StringBuilder(currSb.ToString());
                    currSb.Append(alphabets[i]);
                    StringCombinations(stringMaxLength, count, currSb);
                    currSb = prevCombination;
                }
            }
        }
    }
}
