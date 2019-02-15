namespace OrderEntryEngine
{
    public class EntityEventArgs<T>
    {
        public EntityEventArgs(T item)
        {
            this.Item = item;
        }

        public T Item { get; private set; }
    }
}