namespace cw8_2.Entities
{
    public class Person : BaseEntity
    {
        public string FullName { get; set; }
        public Role Role { get; set; }
        public string Password { get; set; }
        public List<Order> orders { get; set; }
        public int Age { get; set; }
    }

}
