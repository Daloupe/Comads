namespace Comads
{
    public class Digit
    {
        int value = 7;
        public static implicit operator int(Digit source) => source.value;
    }
    
    public class newclass
    {
            
        public static void asndo()
        {

            var a  = new Digit();
            var b  = new Digit();
            
            int c = a;
        }
    }
}