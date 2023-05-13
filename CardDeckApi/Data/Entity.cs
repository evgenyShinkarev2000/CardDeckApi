namespace CardDeckApi.Data
{
    public class Entity<T> where T : notnull
    {
        public T Id { get; set; } = default!;
        public override bool Equals(object? obj)
        {
            if (obj is Entity<T> otherObj)
            {
                return Id.Equals(otherObj.Id);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
