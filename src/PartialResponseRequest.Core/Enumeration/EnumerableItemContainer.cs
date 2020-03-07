namespace PartialResponseRequest.Core.Enumeration
{
    public static class EnumerableItemContainer
    {
        public static EnumerableItemContainer<T> Empty<T>() => new EnumerableItemContainer<T>(true, default);
        public static EnumerableItemContainer<T> Create<T>(T value) => new EnumerableItemContainer<T>(false, value);
    }

    public class EnumerableItemContainer<T>
    {
        public bool Empty { get; private set; }
        public T Value { get; private set; }

        public EnumerableItemContainer(bool empty, T value)
        {
            Empty = empty;
            Value = value;
        }
    }
}
