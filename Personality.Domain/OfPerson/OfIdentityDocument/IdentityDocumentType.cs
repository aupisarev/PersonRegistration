using Personality.Domain.Base;

namespace Personality.Domain.OfPerson.OfIdentityDocument
{
    /// <summary>
    /// Тип документа, удостоверяющего личность
    /// </summary>
    public class IdentityDocumentType : InternalReference<IdentityDocumentType, int>
    {
        /// <summary>
        /// Инициализирует новый экземпляр типа документа, удостоверяющего личность
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="name">Наименование</param>
        /// <param name="serialPattern">Шаблон серии документа в формате RegExp</param>
        /// <param name="serialRule">Текстовое правило заполнения серии документа</param>
        /// <param name="numberPattern">Шаблон номера документа в формате RegExp</param>
        /// <param name="numberRule">Текстовое правило заполнения номера документа</param>
        private IdentityDocumentType(int value, string name, string serialPattern, string serialRule, string numberPattern, string numberRule)
        {
            Value = value;
            Name = name;
            SerialPattern = serialPattern;
            SerialRule = serialRule;
            NumberPattern = numberPattern;
            NumberRule = numberRule;
        }

        /// <summary>
        /// Шаблон серии документа в формате RegExp
        /// </summary>
        public string SerialPattern { get; }

        /// <summary>
        /// Текстовое правило заполнения серии документа
        /// </summary>
        public string SerialRule { get; }

        /// <summary>
        /// Шаблон номера документа в формате RegExp
        /// </summary>
        public string NumberPattern { get; }

        /// <summary>
        /// Текстовое правило заполнения номера документа
        /// </summary>
        public string NumberRule { get; }


        /// <summary>
        /// Значение отсутствует
        /// </summary>
        public static IdentityDocumentType Empty => new(0, string.Empty, "^$", string.Empty, "^$", string.Empty);

        /// <summary>
        /// Паспорт гражданина Российской Федерации
        /// </summary>
        public static IdentityDocumentType RusPassport => new(14, "Паспорт гражданина Российской Федерации",
            @"^[0-9]{2}[ ]{1}[0-9]{2}$", "Серия должна состоять из 2 цифр, знака пробел и 2 цифр",
            @"^[0-9]{6,7}$", "Номер должен содержать от 6 до 7 цифр");

        /// <summary>
        /// Заграничный паспорт гражданина Российской Федерации
        /// </summary>
        public static IdentityDocumentType RusForeignPassport => new(15, "Заграничный паспорт гражданина Российской Федерации",
            @"^[0-9]{2}$", "Серия должна состоять из 2 цифр",
            @"^[0-9]{7}$", "Номер должен состоять из 7 цифр");

        /// <summary>
        /// Свидетельство о рождении, выданное в Российской Федерации
        /// </summary>
        public static IdentityDocumentType RusBirthCertificate => new(3, "Свидетельство о рождении, выданное в Российской Федерации",
            @"^[XIVLM]+[\-]{1}[А-ЯЁ]{2}$", "Серия должна состоять из римской цифры, знака '-' и двух букв",
            @"^[0-9]{6}$", "Номер должен состоять из 6 цифр");
    }
}
