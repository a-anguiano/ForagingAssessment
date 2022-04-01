namespace SustainableForaging.Core.Models
{
    public class Forager
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }

        public Forager() { }

        public Forager(string id, string firstName, string lastName, string state)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            State = state;
        }

        public override bool Equals(object obj)
        {
            return obj is Forager forager &&
                   Id == forager.Id &&
                   FirstName == forager.FirstName &&
                   LastName == forager.LastName &&
                   State == forager.State;
        }

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(Id, FirstName, LastName, State);
        //}
    }
}
