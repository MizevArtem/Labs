using System;
using System.Text.RegularExpressions;

namespace WindowsFormsApp4
{
    /// <summary>
    /// Класс для проверки формат строк
    /// </summary>
    public static class CorrectValue
    {
        //TODO: дубль | +
        /// <summary>
        /// Регулярка для проверки правильности введеных параметров
        /// </summary>
        private const string _сorrectParametersValue =
                    @"(^(-)?([0-9]+)(,|.)?([0-9])+$)|(^(-)?([0-9])+$)";

        /// <summary>
        /// Проверка корректности введеной строки со значением параметра
        /// </summary>
        /// <param name="parametrs">Строка со значением параметра</param>
        /// <returns>Результат проверки на соответствие формата (True/False)</returns>
        public static bool CheckParameterString(string parametrs)
        {
            Regex regex = new Regex(_сorrectParametersValue);
            if (string.IsNullOrEmpty(parametrs))
            {
                throw new Exception("Пустая строка");
            }
            else
            {
                return regex.IsMatch(parametrs);
            }
        }
    }
}
