namespace DotnetAngularMiniEcommerce_API.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
        {
            var result = name.Replace("\"", "");

            result = result.Replace("!", "");
            result = result.Replace("'", "");
            result = result.Replace("^", "");
            result = result.Replace("+", "");
            result = result.Replace("%", "");
            result = result.Replace("&", "");
            result = result.Replace("/", "");
            result = result.Replace("(", "");
            result = result.Replace(")", "");
            result = result.Replace("=", "");
            result = result.Replace("?", "");
            result = result.Replace("_", "");
            result = result.Replace("@", "");
            result = result.Replace("€", "");
            result = result.Replace("¨", "");
            result = result.Replace("~", "");
            result = result.Replace(",", "");
            result = result.Replace(";", "");
            result = result.Replace(":", "");
            result = result.Replace(".", "-");
            result = result.Replace("Ö", "o");
            result = result.Replace("ö", "o");
            result = result.Replace("Ü", "u");
            result = result.Replace("ü", "u");
            result = result.Replace("ı", "i");
            result = result.Replace("İ", "i");
            result = result.Replace("ğ", "g");
            result = result.Replace("Ğ", "g");
            result = result.Replace("æ", "");
            result = result.Replace("ß", "");
            result = result.Replace("â", "a");
            result = result.Replace("î", "i");
            result = result.Replace("ş", "s");
            result = result.Replace("Ş", "s");
            result = result.Replace("Ç", "c");
            result = result.Replace("ç", "c");
            result = result.Replace("<", "");
            result = result.Replace(">", "");
            result = result.Replace("|", "");

            return result;
        }
    }
}
