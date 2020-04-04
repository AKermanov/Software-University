namespace ValidationAttributes
{
   public class Person
    {
        public Person(string firstName, int age)
        {
            this.FullName = firstName;
            this.Age = age;
        }
        [MyRequired]
        public string FullName { get; set; }

        [MyRange(18, 65)] //конструктура на MyRangeAttribute
        public int Age { get; set; }
    }
}
