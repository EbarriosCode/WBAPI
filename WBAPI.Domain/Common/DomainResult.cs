namespace WBAPI.Domain.Common
{
    public class DomainResult
    {
        public bool IsSuccess { get; }
        public IReadOnlyList<string> Errors { get; }

        protected DomainResult(bool isSuccess, IReadOnlyList<string> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static DomainResult Success() => new(true, []);
        public static DomainResult Failure(params string[] errors) => new(false, errors);
    }

    public class DomainResult<T> : DomainResult
    {
        public T? Value { get; }

        private DomainResult(bool isSuccess, T? value, IReadOnlyList<string> errors) : base(isSuccess, errors) => Value = value;

        public static DomainResult<T> Success(T value) => new(true, value, []);
        public static DomainResult<T> Failure(params string[] errors) => new(false, default, errors);
    }
}
