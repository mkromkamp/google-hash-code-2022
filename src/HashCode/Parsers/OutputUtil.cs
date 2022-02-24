namespace HashCode.Parsers
{
    public static class OutputUtil
    {
        public static string[] OutputToListOfLines(Dictionary<string, List<string>> toOutput)
        {          
            List<string> linesToOutput = new List<string>();
            var arrayOfAllKeys = toOutput.Keys.ToArray();
            linesToOutput.Add(arrayOfAllKeys.Count().ToString());
            foreach (string key in arrayOfAllKeys)
            {
                linesToOutput.Add(key);

                string listOfNames = "";
                foreach(var contributor in toOutput[key])
                {
                    listOfNames += contributor + " ";
                }                

                linesToOutput.Add(listOfNames.TrimEnd());
            }
            return linesToOutput.ToArray();
        }
    }
}
