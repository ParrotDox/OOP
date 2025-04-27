using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModel
{
    public class AddQuantityWindowVM
    {
        public string Input { get; set; }
        public int PackedValue { get; set; }
        public AddQuantityWindowVM()
        {
            Input = "";
            PackedValue = 0;
        }
        public bool CheckIsInputCorrect() 
        {
            int value = -1;
            if (int.TryParse(Input, out value) && value > 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        public void PackValue() 
        {
            PackedValue = int.Parse(Input);
        }
    }
}
