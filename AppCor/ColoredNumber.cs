namespace AppCor
{
    public class ColoredNumber
    {
        public string color;
        public int value;

        public string midColor = "FF0000";
        public string lessColor = "00FF00";

        public ColoredNumber() 
        {
        }
        public ColoredNumber(int value)
        {
            color = "97DBEB";
            this.value = value;
        }

        public ColoredNumber(int value, string color):this(value)
        {
            this.color = color;

        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
