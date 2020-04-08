namespace PlayersAndMonsters
{
   public class Hero
    {
        public Hero(string useName, int level)
        {
            this.UserName = useName;
            this.Level = level;
        }

        public string UserName { get; set; }
        public int Level { get; set; }

        public override string ToString()
        {
            return $"Type: {this.GetType().Name} Username: {this.UserName} Level: {this.Level}";
        }
    }
}
