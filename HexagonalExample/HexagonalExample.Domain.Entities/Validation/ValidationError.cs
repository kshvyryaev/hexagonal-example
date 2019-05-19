namespace HexagonalExample.Domain.Entities.Validation
{
    public class ValidationError
    {
        public string EntityName { get; set; }

        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }

        public object AttemptedValue { get; set; }
    }
}
