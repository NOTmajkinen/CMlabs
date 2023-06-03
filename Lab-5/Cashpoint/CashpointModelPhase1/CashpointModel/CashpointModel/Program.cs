namespace CashpointModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class Program
    {
        public static void Main(string[] args)
        {
            Cashpoint cashpoint = new Cashpoint();
            cashpoint.AddBanknote(3, 3);
            cashpoint.AddBanknote(5);
            cashpoint.RemoveBanknote(3);
        }
    }
}
